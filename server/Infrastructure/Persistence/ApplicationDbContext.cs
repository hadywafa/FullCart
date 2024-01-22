using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Domain.Common;
using Domain.EFModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        #region Inject Dependencies

        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,//what is this?
            IDomainEventService domainEventService,//what is this?
            IDateTime dateTime //what is this?
            ) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        #endregion

        public DbSet<Address> Addresses => Set<Address>();

        public DbSet<Card> Cards => Set<Card>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();

        public DbSet<Image> Images => Set<Image>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Admin> Admins => Set<Admin>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Seller> Sellers => Set<Seller>();

        public DbSet<Shipper> Shippers => Set<Shipper>();

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<ProductSpecifications> ProductSpecifications => Set<ProductSpecifications>();

        public DbSet<ProductHighlights> ProductHighlights => Set<ProductHighlights>();

        public DbSet<CustProWishlist> CustProWishlists => Set<CustProWishlist>();

        public DbSet<CustProCart> CustProCarts => Set<CustProCart>();

        public DbSet<CustProSellReviews> Reviews => Set<CustProSellReviews>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            //Restricted on delete
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
                         .Where(x => x.IsRequired && x.IsUnique))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


        //What is this?
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            //var events = ChangeTracker.Entries<IHasDomainEvent>()
            //    .Select(x => x.Entity.DomainEvents)
            //    .SelectMany(x => x)
            //    .Where(domainEvent => !domainEvent.IsPublished)
            //    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            //await DispatchEvents(events);

            return result;
        }

        //What is this?
        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
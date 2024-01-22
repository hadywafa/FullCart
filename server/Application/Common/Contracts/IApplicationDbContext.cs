using System.Threading;
using System.Threading.Tasks;
using Domain.EFModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUser { get; }
        DbSet<Address> Addresses { get; }
        DbSet<Card> Cards { get; }
        DbSet<Admin> Admins { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Seller> Sellers { get; }
        DbSet<Shipper> Shippers { get; }
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductSpecifications> ProductSpecifications { get; }
        DbSet<ProductHighlights> ProductHighlights { get; }
        DbSet<Image> Images { get; }
        DbSet<CustProWishlist> CustProWishlists { get; }
        DbSet<CustProCart> CustProCarts { get; }
        DbSet<CustProSellReviews> Reviews { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
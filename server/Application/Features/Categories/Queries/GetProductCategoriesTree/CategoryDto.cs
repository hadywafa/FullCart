using System.Collections.Generic;
using Application.Features.Brands.Queries.GetAllBrands;

namespace Application.Features.Categories.Queries.GetProductCategoriesTree
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int? ParentId { get; set; }
        //public ICollection<BrandDto> Brands { get; set; }
        public ICollection<CategoryDto> Children { get; set; }
    }
}

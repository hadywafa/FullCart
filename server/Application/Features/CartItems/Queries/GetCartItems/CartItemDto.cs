using Application.Features.Products.Queries.GetProductsBref;

namespace Application.Features.CartItems.Queries.GetCartItems
{
    internal class CartItemDto
    {

        public int Quantity { get; set; }
        public ProductBrefDto ProductBref { get; set; }
    }
}

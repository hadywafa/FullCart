using System.Threading.Tasks;
using Application.Common.ExtensionMethods;
using Application.Features.Categories.Queries.GetCategoriesTree;
using Application.Features.Categories.Queries.GetProductCategoriesTree;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.FilterProductsByCatCode;
using Application.Features.Products.Queries.GetProductDetailsById;
using Application.Features.Products.Queries.GetProductsBref;
using Application.Features.Reviews.Queries.GetReviewsByProductId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Inject MediatR

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Product End Points

        [HttpGet("GetProductsBref")]
        public async Task<IActionResult> GetProductsBref()
        {
            var products = await _mediator.Send(new GetProductsBrefQuery());
            return StatusCode(200, products);
        }

        [HttpGet("GetProductDetailsById/{id}")]
        public async Task<IActionResult> GetProductDetailsById([FromRoute] int id)
        {
            var product = await _mediator.Send(new GetProductDetailsByIdQuery() { Id = id });
            return StatusCode(200, product);
        }

        //Don't use this End point because it's hell
        [HttpGet("FilterProductsByCatCode/{catCode}")]
        public async Task<IActionResult> FilterProductsByCatCode([FromRoute] string catCode)
        {
            var products = await _mediator.Send(
                new FilterProductsByCatCodeQuery() { CatCode = catCode }
            );
            return StatusCode(200, products);
        }

        //Don't use this End point because it's hell
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductBrefDto productBrefDto)
        {
            var result = await _mediator.Send(
                new CreateProductCommand() { ProductBrefDto = productBrefDto }
            );
            return StatusCode(200, result);
        }

        #endregion

        #region Reviews Endpoints

        [HttpGet("GetProductReviews/{id}")]
        public async Task<IActionResult> GetProductReviews([FromRoute] int id)
        {
            var reviews = await _mediator.Send(new GetReviewsByProductIdQuery() { Id = id });
            return StatusCode(200, reviews);
        }

        #endregion

        #region Category End Points

        [HttpGet("GetCategoriesJson")]
        public async Task<IActionResult> GetCategoriesTree()
        {
            var result = await _mediator.Send(new GetCategoriesTreeQuery());
            return StatusCode(200, result.ToString());
        }

        [HttpGet("GetProductCategories")]
        public async Task<IActionResult> GetProductCategoriesTree([FromQuery] int parentCatId)
        {
            var cats = await _mediator.Send(
                new GetProductCategoriesTreeQuery() { ParentCatId = parentCatId }
            );
            return StatusCode(200, cats);
        }

        #endregion
    }
}

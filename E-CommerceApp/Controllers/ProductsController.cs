using Application.Abstractions;
using Application.Products.Commands;
using Application.Products.Queries;
using E_CommerceApp.Contracts.Product.Request;
using E_CommerceApp.Contracts.Product.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductsController : BaseController
{
    //TODO: Add GetProductsByCategoryId
    private readonly ICurrentUser _currentUser;

    public ProductsController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    [HttpGet]
    [Route(ApiRoutes.Product.Get)]
    public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery { ProductId = productId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.Product.GetProductsByCategoryName)]
    public async Task<IActionResult> GetAllProducts([FromBody] string categoryName, CancellationToken cancellationToken)
    {
        var query = new GetProductsByCategoryNameQuery { CategoryName = categoryName };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<ProductResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.Product.Create)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreate productCreate,
        CancellationToken cancellationToken)
    {
        var product = _mapper.Map<CreateProductCommand>(productCreate);
        var result = await _mediator.Send(product, cancellationToken);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPatch]
    [Route(ApiRoutes.Product.Update)]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdate productUpdate, Guid productId,
        CancellationToken cancellationToken)
    {
        var product = _mapper.Map<UpdateProductCommand>(productUpdate);
        product.Id = productId;
        var result = await _mediator.Send(product, cancellationToken);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpDelete]
    [Route(ApiRoutes.Product.Delete)]
    public async Task<IActionResult> DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        var product = new DeleteProductCommand { Id = productId };
        var result = await _mediator.Send(product, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }

    [HttpPost]
    [Route(ApiRoutes.Product.CreateComment)]
    public async Task<IActionResult> CreateComment([FromBody] CommentCreate commentCreate, Guid productId,
        CancellationToken cancellationToken)
    {
        var comment = new AddProductCommentCommand
        {
            ProductId = productId,
            Text = commentCreate.Text,
            CustumUserId = _currentUser.UserId
        };
        var result = await _mediator.Send(comment, cancellationToken);
        var mapped = _mapper.Map<CommentResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }

    [HttpDelete]
    [Route(ApiRoutes.Product.DeleteComment)]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid productId, CancellationToken cancellationToken)
    {
        var comment = new DeleteProductCommentCommand
        {
            ProductId = productId,
            UserId = _currentUser.UserId,
            ProductCommentId = commentId
        };
        var result = await _mediator.Send(comment, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }

    [HttpPost]
    [Route(ApiRoutes.Product.CreateRate)]
    public async Task<IActionResult> CreateRate([FromBody] RateCreate rateCreate, Guid productId,
        CancellationToken cancellationToken)
    {
        var comment = new AddProductRateCommand
        {
            ProductId = productId,
            RateValue = rateCreate.RateValue,
            UserId = _currentUser.UserId
        };
        var result = await _mediator.Send(comment, cancellationToken);
        var mapped = _mapper.Map<RateResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpDelete]
    [Route(ApiRoutes.Product.DeleteRate)]
    public async Task<IActionResult> DeleteRate(Guid rateId, Guid productId, CancellationToken cancellationToken)
    {
        var comment = new DeleteProductRateCommand
        {
            RateId = rateId,
            ProductId = productId,
            UserId = _currentUser.UserId,
        };
        var result = await _mediator.Send(comment, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }
}
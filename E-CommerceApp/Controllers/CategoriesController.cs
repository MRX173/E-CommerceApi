using Application.Models;
using Application.ProductCategories.Commands;
using Application.ProductCategories.Queries;
using Domain.ProductAggregate;
using E_CommerceApp.Contracts.Product.Request;
using E_CommerceApp.Contracts.Product.Response;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

public class CategoriesController : BaseController
{
    [HttpGet]
    [Route(ApiRoutes.Category.GetAll)]
    public async Task<IActionResult> GetCategories( CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        var mappedResult = _mapper.Map<List<CategoryResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mappedResult);
    }

    [HttpPost]
    [Route(ApiRoutes.Category.Create)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreate categoryCreate
        , CancellationToken cancellationToken)
    {
        var category = new CreateProductCategoryCommand()
        {
            CategoryName = categoryCreate.CategoryName,
            CategoryDescription = categoryCreate.Description,
            CategoryImageUrl = categoryCreate.ImageUrl
        };
        var result = await _mediator.Send(category, cancellationToken);
        var mappedResult = _mapper.Map<CategoryResponse>(result.Payload);
        return Ok(mappedResult);
    }

    [HttpPatch]
    [Route(ApiRoutes.Category.Update)]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryCreate categoryUpdate, Guid categoryId,
        CancellationToken cancellationToken)
    {
        var category = new UpdateProductCategoryCommand
        {
            ProductCategoryId = categoryId,
            CategoryName = categoryUpdate.CategoryName,
            CategoryDescription = categoryUpdate.Description,
            CategoryImageUrl = categoryUpdate.ImageUrl
        };
        var result = await _mediator.Send(category, cancellationToken);
        var mappedResult = _mapper.Map<CategoryResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }

    [HttpDelete]
    [Route(ApiRoutes.Category.Delete)]
    public async Task<IActionResult> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = new DeleteProductCategoryCommand
        {
            ProductCategoryId = categoryId
        };
        var result = await _mediator.Send(category, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : NoContent();
    }
    
}
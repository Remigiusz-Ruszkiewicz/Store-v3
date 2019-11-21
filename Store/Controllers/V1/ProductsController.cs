using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = (await productsService.GetAllAsync()).Select(x => new ProductResponse { Id = x.Id, Name = x.Name });

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var product = await productsService.GetAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Products.Add)]
        public async Task<IActionResult> Add([FromBody]ProductRequest productRequest)
        {
            var product = await productsService.AddAsync(productRequest.Name);

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]ProductRequest productRequest)
        {
            var product = await productsService.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productRequest.Name;

            var updatedProduct = await productsService.UpdateAsync(product);

            var response = new ProductResponse { Id = updatedProduct.Id, Name = updatedProduct.Name };

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await productsService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
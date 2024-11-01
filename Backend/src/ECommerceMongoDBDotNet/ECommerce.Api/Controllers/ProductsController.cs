﻿using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Infra.Repositories.CacheRepositories;
using ECommerce.Api.InputModels.Queries;
using ECommerce.Api.Services.Contract;
using ECommerce.Api.ViewModels.ProductViewModels;
using ECommerce.Service.InputModels.ProductInputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Fields
        private readonly IProductService _productService;
        #endregion Fields

        #region Ctor
        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        #endregion Ctor

        /// <summary>
        /// Get Product List
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var productVMs = await _productService.GetProductsAsync().ConfigureAwait(false);

            return Ok(productVMs);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("/api/products")]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel inputModel)
        {
            int productId = await _productService.CreateProductAsync(inputModel).ConfigureAwait(false);

            return CreatedAtRoute(nameof(GetProduct), new { productId = productId }, new { ProductId = productId });
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("/api/products/{productId:int}", Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _productService.GetProductAsync(productId).ConfigureAwait(false);

            return Ok(product);
        }

        /// <summary>
        /// Get Product By Filter
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("/api/products/get-product-by-filter")]
        public async Task<IActionResult> GetProductByFilterAsync([FromQuery] GetProductByFilterQuery query)
        {
            var product = await _productService.GetProductByFilterAsync(query.Id, query.ProductName, query.ProductTitle).ConfigureAwait(false);

            return Ok(product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("/api/products/{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, UpdateProductInputModel inputModel)
        {
            await _productService.UpdateProductAsync(inputModel).ConfigureAwait(false);

            return NoContent();
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("/api/products/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId).ConfigureAwait(false);

            return NoContent();
        }

    }

}

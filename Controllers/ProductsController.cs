using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;
using System.Collections.Generic;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //GET /products?name={name}` - finds all products matching the specified name.
        //pass name via querystring [api/products/getproductsbyname?name=
        [HttpGet("GetProductsByName")]
        public IActionResult GetProductsByName(string name)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            List<Product> products = null;
            if(name == null)
            {
                return BadRequest();
            }

            try
            {
                products = Products.GetProducts(name);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }

            return Ok(products);
        }

        //GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
        //getproductbyid/DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(Guid? Id) 
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            List<Product> products = null;
            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                //products = Products.GetProducts(new Guid(BitConverter.GetBytes(id)));
                products = Products.GetProducts((Guid)Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }

            return Ok(products);
        } //consider using [FormBody] so not visable in querystring

        //POST /products` - creates a new product.
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                Product.CreateProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok();
        }

        //PUT /products/{id}` - updates a product.
        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(Product product)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                Product.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok();
        }

        //DELETE /products/{id}` - deletes a product and its options.
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(Guid? Id) 
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }
            if (Id == null)
            {
                return BadRequest();
            }


            try
            {
                Product.DeleteProduct((Guid)Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok();
        } //consider using [FormBody] so not visible in querystring

        //GET /products/{id}/options` - finds all options for a specified product.
        [HttpGet("GetProductOptionById/{productId}")]
        public IActionResult GetProductOptionById(Guid Id)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            ProductOption productOption;
            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                productOption = ProductOption.GetProductOption((Guid)Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok(productOption);
        }  //consider using [FormBody] so not visable in querystring

        //POST /products/{id}/options` - adds a new product option to the specified product.
        [HttpPost("CreateProductOption/{productId}")]
        public IActionResult CreateProductOption(ProductOption productOption)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            if (productOption == null)
            {
                return BadRequest();
            }

            try
            {
                ProductOption.CreateProductOption(productOption);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok();
        }

        //DELETE /products/{id}/options/{optionId}` - deletes the specified product option.
        [HttpDelete("DeleteOption/{productId}")]
        public IActionResult DeleteOption(Guid? Id)
        {
            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            if (!AuthenticateUser.IsAuth(HttpContext.Request.Headers["APIToken"].ToString()))
            {
                return StatusCode(401, "APIToken not found.");
            }

            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                ProductOption.DeleteProductOption((Guid)Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }
            return Ok();
        }
    }
}
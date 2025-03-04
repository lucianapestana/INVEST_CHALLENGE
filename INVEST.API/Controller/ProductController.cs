using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.API.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductOutput _output;
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _output = new ProductOutput();
            _productService = productService;
        }

        /// <summary>
        /// Listar todos os produtos.
        /// </summary>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult> GetProducts
            (
                [FromQuery(Name = "i")] int? idProduct
            
            )
        {
            try
            {
                var result = await _productService.GetProducts(idProduct);

                if (result.Products != null)
                {
                    _output.Status = "success";
                    _output.Code = "200";
                    _output.MessageCode = "Produtos obtidos com sucesso.";
                    _output.Products = result.Products;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "error";
                    _output.Code = "400";
                    _output.MessageCode = "Nemhum produto encontrado.";
                    return await Task.FromResult(StatusCode(400, _output));
                }
            }
            catch (Exception)
            {
                _output.Status = "error";
                _output.Code = "500";
                _output.MessageCode = "Ocorreu um erro inesperado ao processar a requisição.";
                return await Task.FromResult(StatusCode(500, _output));
            }
        }
    }
}

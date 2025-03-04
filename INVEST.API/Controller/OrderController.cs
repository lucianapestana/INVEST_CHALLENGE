using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.API.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderOutput _output;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _output = new OrderOutput();
            _orderService = orderService;
        }

        /// <summary>
        /// Atualiza o saldo do cliente e o lastro do produto
        /// </summary>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> OrderClient([FromBody] OrderInput input)
        {
            try
            {
                var result = await _orderService.OrderClient(input);

                if (result != null && result.Order != null)
                {
                    _output.Status = "success";
                    _output.Code = "200";
                    _output.MessageCode = "Pedido realizado com sucesso.";
                    _output.Order = result.Order;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "error";
                    _output.Code = "400";
                    _output.MessageCode = "Falha ao realizar o pedido.";
                    _output.Errors = result.Errors.Any() ? result.Errors : new List<Error>();
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

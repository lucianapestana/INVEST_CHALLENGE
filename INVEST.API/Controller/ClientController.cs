using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.API.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly ClientOutput _output;
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _output = new ClientOutput();
            _clientService = clientService;
        }

        /// <summary>
        /// Faz login do cliente.
        /// </summary>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> ClientLogin([FromBody] LoginClient input)
        {
            try
            {
                var result = await _clientService.GetLoginClient(input);

                if (result.LoginClient != null)
                {
                    _output.Status = "success";
                    _output.Code = "200";
                    _output.MessageCode = "Login efetuado com sucesso.";
                    _output.LoginClient = result.LoginClient;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "error";
                    _output.Code = "400";
                    _output.MessageCode = "Falha ao efetuar o login.";
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

        /// <summary>
        /// Obtem os dados do cliente.
        /// </summary>
        /// <response code="200">Retorno de processamento executado com sucesso.</response>
        /// <response code="400">Retorno de erro de processamento.</response>
        /// <response code="500">Retorno de servidor indisponível.</response>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult> GetClients
            (
                [FromQuery(Name = "i")] int? idClient,
                [FromQuery(Name = "n")] string? nameClient
            )
        {
            try
            {
                var result = await _clientService.GetClients(idClient, nameClient);

                if (result.Clients != null)
                {
                    _output.Status = "success";
                    _output.Code = "200";
                    _output.MessageCode = "Login efetuado com sucesso.";
                    _output.Clients = result.Clients;
                    return await Task.FromResult(StatusCode(200, _output));
                }
                else
                {
                    _output.Status = "error";
                    _output.Code = "400";
                    _output.MessageCode = "Falha ao efetuar o login.";
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

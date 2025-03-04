using System.Text;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.SITE.Factory.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.SITE.Controllers.Json
{
    [Route("json/client")]
    public class JsonClientController : Controller
    {
        private readonly IClientFactory _clientFactory;

        public JsonClientController(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResult> LoginClient([FromBody] LoginClient payload)
        {
            try
            {
                payload.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.Password));
                var data = await _clientFactory.ClientLogin(payload);
                 
                return Json(
                    new
                    {
                        result = data
                    });
            }
            catch (Exception ex)
            {
                return Json(
                   new
                   {
                       sucesso = false,
                       erros = new List<Error>()
                       {
                           new Error()
                           {
                               Message = ex.Message
                           }
                       }
                   });
            }
        }

        [HttpGet]
        [Route("{idClient}")]
        public async Task<JsonResult> GetClientById([FromRoute] string idClient)
        {
            try
            {
                var data = await _clientFactory.GetClientById(idClient);

                return Json(
                    new
                    {
                        result = data
                    });
            }
            catch (Exception ex)
            {
                return Json(
                   new
                   {
                       sucesso = false,
                       erros = new List<Error>()
                       {
                           new Error()
                           {
                               Message = ex.Message
                           }
                       }
                   });
            }
        }

    }
}

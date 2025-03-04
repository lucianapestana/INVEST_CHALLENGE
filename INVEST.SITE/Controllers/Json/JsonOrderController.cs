using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.SITE.Factory.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.SITE.Controllers.Json
{
    [Route("json/order")]
    public class JsonOrderController : Controller
    {
        private readonly IOrderFactory _orderFactory;

        public JsonOrderController(IOrderFactory orderFactory)
        {
            _orderFactory = orderFactory;
        }

        [HttpPost]
        [Route("client")]
        public async Task<JsonResult> OrderClient([FromBody] OrderInput payload)
        {
            try
            {
                var data = await _orderFactory.OrderClient(payload);

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

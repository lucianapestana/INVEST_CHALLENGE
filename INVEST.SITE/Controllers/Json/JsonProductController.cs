using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.SITE.Factory.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INVEST.SITE.Controllers.Json
{
    [Route("json/product")]
    public class JsonProductController : Controller
    {
        private readonly IProductFactory _productFactory;

        public JsonProductController(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }
       
        [HttpGet]
        [Route("all")]
        public async Task<JsonResult> GetAllProducts()
        {
            try
            {
                var data = await _productFactory.GetAllProducts();
                 
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

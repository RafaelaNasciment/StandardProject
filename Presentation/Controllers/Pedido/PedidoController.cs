using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace api_project_presentation.Controllers.Pedido
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        [Route("v1")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> ListAsync()
        {
            bool teste = false;
            if (teste == true)
                return BadRequest(teste);

            return Ok(true);
        }
    }
}

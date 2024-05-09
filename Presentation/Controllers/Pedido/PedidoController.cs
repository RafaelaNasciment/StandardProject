using api_project_domain.Dto;
using api_project_domain.Dto.Product;
using api_project_service.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace api_project_presentation.Controllers.Pedido
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly CreateProductService _createProductService;

        public PedidoController(
            CreateProductService createProductService)
        {
            _createProductService = createProductService;
        }

        [HttpGet]
        [Route("v1")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseModelDto<CreateProductDto>>> ListAsync()
        {
            var teste = _createProductService.Handle();

            if (!teste.Sucess)
                return BadRequest(teste);

            return Ok(teste);
        }
    }
}

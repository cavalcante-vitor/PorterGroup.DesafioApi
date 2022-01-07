using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PorterGroup.Desafio.Api.Constants;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Models.Responses;
using PorterGroup.Desafio.Business.Services;

namespace PorterGroup.Desafio.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces(HttpHeaders.JsonApiContentType)]
    [Consumes(HttpHeaders.JsonApiContentType)]
    [ApiController]
    public class CondominiosController : ControllerBase
    {
        private readonly ICondominioService _condominioService;

        public CondominiosController(
            ICondominioService condominioService) =>
            _condominioService = condominioService;

        [HttpGet("{id}", Name = nameof(GetCondominioByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCondominioByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return await Task.FromResult(BadRequest("Bad Id."));
            }

            var condominioModel = await _condominioService.GetByIdAsync(id);
            if (condominioModel == null)
            {
                return await Task.FromResult(NotFound());
            }

            return await Task.FromResult(Ok(condominioModel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateCondominioAsync([FromBody] Condominio condominio)
        {
            var id = await _condominioService.CreateAsync(condominio);
            return await Task.FromResult(CreatedAtRoute(
             routeName: nameof(GetCondominioByIdAsync),
             routeValues: new { id },
             value: condominio));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutCondominioAsync([FromBody] Condominio condominio)
        {
            await _condominioService.PutAsync(condominio);
            return await Task.FromResult(new NoContentResult());
        }
    }
}

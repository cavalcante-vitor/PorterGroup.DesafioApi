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
    public class ApartamentosController : ControllerBase
    {
        private readonly IApartamentoService _apartamentoService;

        public ApartamentosController(
            IApartamentoService apartamentoService) =>
            _apartamentoService = apartamentoService;

        [HttpGet("{id}", Name = nameof(GetApartamentoByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetApartamentoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return await Task.FromResult(BadRequest("Bad Id."));
            }

            var apartamentoModel = await _apartamentoService.GetByIdAsync(id);
            if (apartamentoModel == null)
            {
                return await Task.FromResult(NotFound());
            }

            return await Task.FromResult(Ok(apartamentoModel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateApartamentoAsync([FromBody] Apartamento apartamento)
        {
            var id = await _apartamentoService.CreateAsync(apartamento);
            return await Task.FromResult(CreatedAtRoute(
             routeName: nameof(GetApartamentoByIdAsync),
             routeValues: new { id },
             value: apartamento));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutApartamentoAsync([FromBody] Apartamento apartamento)
        {
            await _apartamentoService.PutAsync(apartamento);
            return await Task.FromResult(new NoContentResult());
        }
    }
}

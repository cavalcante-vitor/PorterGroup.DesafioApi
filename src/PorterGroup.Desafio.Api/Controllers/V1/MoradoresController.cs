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
    public class MoradoresController : ControllerBase
    {
        private readonly IMoradorService _moradorService;

        public MoradoresController(
            IMoradorService moradorService) =>
            _moradorService = moradorService;

        [HttpGet("{id}", Name = nameof(GetMoradorByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetMoradorByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return await Task.FromResult(BadRequest("Bad Id."));
            }

            var moradorModel = await _moradorService.GetByIdAsync(id);
            if (moradorModel == null)
            {
                return await Task.FromResult(NotFound());
            }

            return await Task.FromResult(Ok(moradorModel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateMoradorAsync([FromBody] Morador morador)
        {
            var id = await _moradorService.CreateAsync(morador);
            return await Task.FromResult(CreatedAtRoute(
             routeName: nameof(GetMoradorByIdAsync),
             routeValues: new { id },
             value: morador));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutMoradorAsync([FromBody] Morador morador)
        {
            await _moradorService.PutAsync(morador);
            return await Task.FromResult(new NoContentResult());
        }
    }
}

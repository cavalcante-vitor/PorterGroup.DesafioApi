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
    public class BlocosController : ControllerBase
    {
        private readonly IBlocoService _blocoService;

        public BlocosController(
            IBlocoService blocoService) =>
            _blocoService = blocoService;

        [HttpGet("{id}", Name = nameof(GetBlocoByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBlocoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                return await Task.FromResult(BadRequest("Bad Id."));
            }

            var blocoModel = await _blocoService.GetByIdAsync(id);
            if (blocoModel == null)
            {
                return await Task.FromResult(NotFound());
            }

            return await Task.FromResult(Ok(blocoModel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateBlocoAsync([FromBody] Bloco bloco)
        {
            var id = await _blocoService.CreateAsync(bloco);
            return await Task.FromResult(CreatedAtRoute(
             routeName: nameof(GetBlocoByIdAsync),
             routeValues: new { id },
             value: bloco));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutBlocoAsync([FromBody] Bloco bloco)
        {
            await _blocoService.PutAsync(bloco);
            return await Task.FromResult(new NoContentResult());
        }
    }
}

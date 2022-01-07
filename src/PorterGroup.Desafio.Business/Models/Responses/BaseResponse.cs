using System;

namespace PorterGroup.Desafio.Business.Models.Responses
{
    public abstract record BaseResponse
    {
        public Guid Id { get; init; }

        public DateTime ResponseDate { get; init; }

        public int ResponseCode { get; init; }
    }
}

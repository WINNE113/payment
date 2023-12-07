using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.Base.Models;
using Payment.Application.Features.Commands;
using Payment.Application.Features.Dtos;
using Payment.Application.Features.Payment.Commands;
using System.Net;

namespace Payment.Api.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Created payment to get link
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDtos>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePayment request)
        {
            var response = new BaseResultWithData<PaymentLinkDtos>();
            response = await mediator.Send(request);
            return Ok(response);
        }

    }
}

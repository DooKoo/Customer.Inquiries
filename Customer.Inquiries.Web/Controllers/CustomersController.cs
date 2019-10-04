using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Inquiries.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Inquiries.Web.Controllers
{
    [Route("api/[controller]/inquiry")]
    [ApiController]
    [Authorize("ApiPolicy")]
    public class CustomersController : Controller
    {
        private readonly IMediator mediator;
        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetInquiryCommand request)
        {
            return Json(await mediator.Send(request));
        }
    }
}

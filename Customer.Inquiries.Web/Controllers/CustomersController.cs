using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Inquiries.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Inquiries.Web.Controllers
{
    [Route("api/customers/inquiry")]
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
        public async Task<IActionResult> Get([FromQuery]GetCustomerQuery request)
        {
			var customerProfile = await mediator.Send(request);

            if (customerProfile == null) return NotFound();

            return Json(customerProfile);
        }
    }
}

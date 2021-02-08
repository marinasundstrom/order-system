using Commerce.Application.Common;
using Commerce.Application.Common.Models;
using Commerce.Application.Orders;
using Commerce.Application.Orders.Queries;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<PagedResult<OrderDto>> Get(int page = 1, int pageSize = 100)
        {
            return await mediator.Send(new GetOrdersQuery(page, pageSize));
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Task<OrderDto> Get(int id)
        {
            return mediator.Send(new GetOrderQuery(id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody][JsonSchemaType(typeof(IEnumerable<PatchOperation>))] JsonPatchDocument<OrderDto> patchDocument)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

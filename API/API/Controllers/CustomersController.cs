using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Models.Context;
using API.Repositories;
using API.ReturnModels;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRestaurantRepository _context;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CustomersController(IRestaurantRepository context, IMapper mapper, LinkGenerator linkGenerator)
        {
            _context = context;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Customers
        public async Task<ActionResult<Customer[]>> GetCustomers()
        {
            try
            {
                var results = await _context.GetAllCustomersAsync();

                return results;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }

        }

        //GET: api/Customers/5
        //We should use the ActionResult if we would like to expose API/document it well. With this we will be showing what we are actually returning.
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.GetCustomerById(id);

                if (customer == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There's no customer with the Id: {id}");
                }

                return customer;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }
        }

        //PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerModel>> PutCustomer(int id, CustomerModel customer)
        {
            try
            {
                var oldCustomer = await _context.GetCustomerById(id);

                if (oldCustomer == null)
                {
                    return NotFound($"Could not find customer with the id: {id}");
                }

                _mapper.Map(customer, oldCustomer);

                if (await _context.SaveChangesAsync())
                {
                    return _mapper.Map<CustomerModel>(oldCustomer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }

            return BadRequest();
        }

        //POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);

                _context.Add(customer);

                var location =
                    _linkGenerator.GetPathByAction("GetCustomer", 
                        "Customers", 
                        new {id = customer.CustomerId});

                if (await _context.SaveChangesAsync())
                {
                    return Created(location, customer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }

            return BadRequest();
        }

        // DELETE: api/Customers/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _context.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Delete(customer);

                if (await _context.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }

            return BadRequest($"Failed to delete customer with id {id}");
        }

        #region SPROC Calls

        //GET Customers using Stored Procedure - check how to manage the routing!
        [HttpGet, Route("/sproc/all")]
        public async Task<ActionResult<CustomerModel[]>> GetCustomersSproc()
        {
            try
            {
                var result = await _context.GetAllCustomersSprocAsync();

                return _mapper.Map<CustomerModel[]>(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }
        }

        //GET Customers using Stored Procedure - check how to manage the routing!
        [HttpGet, Route("/sproc/{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(int id)
        {
            try
            {
                var result = await _context.GetCustomerByIdSprocAsync(id);

                return _mapper.Map<CustomerModel>(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while executing request: {ex}");
            }

        }

        #endregion

    }
}

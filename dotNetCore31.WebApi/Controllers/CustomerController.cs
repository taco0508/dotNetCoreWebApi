using AutoMapper;
using dotNetCore31.Business.Dtos;
using dotNetCore31.Business.IServices;
using dotNetCore31.WebApi.Infrastructure.ActionFilters;
using dotNetCore31.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNetCore31.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CoreProfilerActionFilter]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(
            IMapper mapper,
            ICustomerService customerService)
        {
            this._mapper = mapper;
            this._customerService = customerService;
        }

        /// <summary>
        /// 取得客戶清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomerList")]
        public async Task<IActionResult> GetCustomerListAsync()
        {
            var customerIds = new string[] { "ALFKI", "ANATR", "ANTON" };
            var data = await this._customerService.GetCustomerListAsync(customerIds);
            var result = this._mapper.Map<IEnumerable<CustomersDto>, IEnumerable<CustomersViewModel>>(data);
            return Ok(result);
        }
    }
}
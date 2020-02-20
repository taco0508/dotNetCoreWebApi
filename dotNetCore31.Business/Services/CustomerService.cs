using AutoMapper;
using dotNetCore31.Business.Dtos;
using dotNetCore31.Business.IServices;
using dotNetCore31.DataAccess.IRepositories;
using dotNetCore31.DataAccess.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNetCore31.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IMapper mapper,
            ICustomerRepository customerRepository)
        {
            this._mapper = mapper;
            this._customerRepository = customerRepository;
        }

        /// <summary>
        /// 取得客戶清單
        /// </summary>
        /// <param name="customerIds">客戶編號(多筆)</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomersDto>> GetCustomerListAsync(IEnumerable<string> customerIds)
        {
            var data = await this._customerRepository.GetCustomerListAsync(customerIds);
            var result = this._mapper.Map<IEnumerable<CustomersDataModel>, IEnumerable<CustomersDto>>(data);
            return result;
        }
    }
}
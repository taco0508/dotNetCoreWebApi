using AutoMapper;
using dotNetCore31.Business.Dtos;
using dotNetCore31.DataAccess.Models.DataModels;

namespace dotNetCore31.Business.Infrastructure.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<CustomersDataModel, CustomersDto>();
        }
    }
}
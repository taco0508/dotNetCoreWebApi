using AutoMapper;
using dotNetCore31.Business.Dtos;
using dotNetCore31.WebApi.ViewModels;

namespace dotNetCore31.WebApi.Infrastructure.Mappings
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<CustomersDto, CustomersViewModel>();
        }
    }
}
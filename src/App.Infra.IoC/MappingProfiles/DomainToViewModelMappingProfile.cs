using App.Application.ViewModels;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infra.IoC.MappingProfiles
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}

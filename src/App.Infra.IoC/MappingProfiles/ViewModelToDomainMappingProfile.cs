using App.Application.ViewModels;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infra.IoC.MappingProfiles
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<RegisterViewModel, Customer>();
        }
    }
}

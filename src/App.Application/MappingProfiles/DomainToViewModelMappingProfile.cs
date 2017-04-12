using App.Application.ViewModels;
using App.Domain.Models;
using AutoMapper;

namespace App.Application.MappingProfiles
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}

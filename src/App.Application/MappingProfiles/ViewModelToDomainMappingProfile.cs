using App.Application.ViewModels;
using App.Domain.Models;
using AutoMapper;

namespace App.Application.MappingProfiles
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}

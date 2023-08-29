using AutoMapper;
using DotnetApiTemplate.Data.Models;
using Application.Dtos;

namespace DotnetApiTemplate.Api.AutoMapperProfiles
{
    public class EmployeeAutoMapperProfile : Profile
    {
        public EmployeeAutoMapperProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

            //Below Comment is for example only

            //CreateMap<EmployeeCreateDto, Employee>()
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}

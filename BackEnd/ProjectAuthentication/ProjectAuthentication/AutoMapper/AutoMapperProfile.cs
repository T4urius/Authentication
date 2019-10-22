using AutoMapper;
using ProjectAuthentication.Dtos;
using ProjectAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TblBook, BookListDto>()
                .ForMember(c => c.Title, s => s.MapFrom(b => b.Title))
                .ForMember(c => c.Description, s => s.MapFrom(b => b.Description)).ReverseMap();

            CreateMap<LoginDto, TblUser>()
                .ForMember(c => c.Email, s => s.MapFrom(b => b.Email))
                .ForMember(c => c.Password, s => s.MapFrom(b => b.Password)).ReverseMap();

            CreateMap<RegisterDto, TblUser>()
                .ForMember(c => c.Email, s => s.MapFrom(b => b.Email))
                .ForMember(c => c.Password, s => s.MapFrom(b => b.Password))
                .ForMember(c => c.FullName, s => s.MapFrom(b => b.FullName)).ReverseMap();
        }
    }
}

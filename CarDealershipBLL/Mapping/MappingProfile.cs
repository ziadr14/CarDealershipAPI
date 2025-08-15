using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();

            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));

            CreateMap<CarDto, Car>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore());
            CreateMap<Car, CreatCarDto>().ReverseMap();


            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.CarId,
                    opt => opt.MapFrom(src => src.Car != null ? src.Car.Model : null))
                .ForMember(dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FullName : null))
                .ReverseMap()
                .ForMember(dest => dest.Car, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
        }
    }
}

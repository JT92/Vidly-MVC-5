using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        // DTO (Data Transfer Object) Mapping for API
        public MappingProfile()
        {

            // CUSTOMERS 
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>().
                ForMember(c => c.Id, opt => opt.Ignore());

            // CUSTOMERS - MEMBERSHIP TYPE
            CreateMap<MembershipType, MembershipTypeDto>();

            // MOVIES 
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>().
                ForMember(c => c.Id, opt => opt.Ignore());

            // MOVIES - GENRE
            CreateMap<Genre, GenreDto>();

        }
    }
}
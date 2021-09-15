using AutoMapper;
using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Mapping
{
    public class ApplicationUserMapping : Profile
    {
        public ApplicationUserMapping()
        {
            CreateMap<ApplicationUser, SignupDto>().ReverseMap();
        }
    }
}

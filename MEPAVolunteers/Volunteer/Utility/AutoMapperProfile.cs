using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Model
{
    internal class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VolunteerModel, VolunteerDTO>();
            CreateMap<VolunteerDTO, VolunteerModel>();
        }
    }
}

using Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Model;
using Volunteer.Utility;

namespace Volunteer.Interface
{
    public interface IVolunteerService
    {
        Task<List<VolunteerDTO>> GetVolunteerAync();
        Task<VolunteerDTO> GetVolunteerByIDAync(string VolunteerID);
        Task<bool> RegisterVolunteer(RegisterModel item, Role rolw);
        Task<VolunteerModel> CreateVolunteer(VolunteerDTO item);
        Task<VolunteerDTO> ValidateVolunteer(LoginModel item);
    }
}

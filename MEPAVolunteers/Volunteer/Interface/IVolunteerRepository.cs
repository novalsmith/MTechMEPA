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
    public interface IVolunteerRepository
    {
        Task<List<VolunteerModel>> GetVolunteerAync();
        Task<VolunteerModel> GetVolunteerByIDAync(string VolunteerID);

        Task<bool> RegisterVolunteer(RegisterModel item, Role role);
        Task<VolunteerModel> CreateVolunteer(VolunteerDTO item);

    }
}

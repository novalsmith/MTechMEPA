using AutoMapper;
using Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Interface;
using Volunteer.Model;
using Volunteer.Utility;

namespace Volunteer.Service
{
    public class VolunteerService : IVolunteerService
    {
        private IVolunteerRepository _repository;
        private readonly IMapper _mapper;
        public VolunteerService(IVolunteerRepository category, IMapper mapper)
        {
            _repository = category;
            _mapper = mapper;
        }
        public Task<VolunteerModel> CreateVolunteer(VolunteerDTO item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VolunteerDTO>> GetVolunteerAync()
        {
            var result = await _repository.GetVolunteerAync();
            var list = _mapper.Map<List<VolunteerDTO>>(result);
            return list;
        }

        public async Task<VolunteerDTO> GetVolunteerByIDAync(string VolunteerID)
        {
            var result = await _repository.GetVolunteerByIDAync(VolunteerID);
            var list = _mapper.Map<VolunteerDTO>(result);
            return list;
        }

        public async Task<bool> RegisterVolunteer(RegisterModel item, Role role)
        {
            var result = await _repository.RegisterVolunteer(item, role);
            return result;
        }
        public async Task<VolunteerDTO> ValidateVolunteer(LoginModel item) {
            var passwordCorrect = false;
            var result = await _repository.GetVolunteerByIDAync(item.Username);
            if (result != null && result.Password != null) {
                var validate = BCrypt.Net.BCrypt.Verify(item.Password, result.Password);
                if (validate) {
                    passwordCorrect = true; 
                } 
            }

            var list = _mapper.Map<VolunteerDTO>(result);
            if (!passwordCorrect) {
                list = null;
            }
            return list;
        }
    }
}

using BoilerPlate.Service.DTO.Request;
using BoilerPlate.Service.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAll();
        Task<UserResponseDTO> GetUser(Guid id);
        Task<List<UserContactResponseDto>> GetContacts(Guid id);
        Task<SuccessDTO> Save(UserRequestDTO user);
        Task<SuccessDTO> Update(Guid id, UserRequestDTO user);
        Task<SuccessDTO> SaveContact(Guid id, UserContactRequesttDTO contact);
        Task<SuccessDTO> UpdateContact(Guid id, UserContactRequesttDTO contact);
    }
}

using BoilerPlate.Domain.Exceptions;
using BoilerPlate.Domain.Interfaces.Repositories;
using BoilerPlate.Domain.Models;
using BoilerPlate.Domain.Util;
using BoilerPlate.Service.DTO.Request;
using BoilerPlate.Service.DTO.Response;
using BoilerPlate.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDTO>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.ToDTO()).ToList();
        }

        public async Task<List<UserContactResponseDto>> GetContacts(Guid id)
        {
            var usersContacts = await _userRepository.GetContactListByIdAsync(id);
            if (usersContacts is null)
            {
                throw new NotFoundException(Constant.UserNotFound);
            }
            return usersContacts.Select(c => c.ToDTO()).ToList();
        }

        public async Task<UserResponseDTO> GetUser(Guid id)
        {
            var userObj = await _userRepository.GetByIdAsync(id);
            if (userObj is null)
            {
                throw new NotFoundException(Constant.UserNotFound);
            }

            return userObj.ToDTO();
        }

        public async Task<SuccessDTO> Save(UserRequestDTO user)
        {
            var preFix = Enumeration.FromDisplayName<UserPrefix>(user.Prefix);
            var userObj = new User(preFix, user.Name, user.Address, user.Job);

            foreach (var c in user.Contacts)
            {
                var cType = Enum.Parse<ContactType>(c.Type);
                userObj.AddOrUpdateContact(cType, c.Number, c.Id);
            }

            _userRepository.Add(userObj);
            await _userRepository.SaveChangesAsync();

            return new SuccessDTO(Constant.RecordCreated);
        }

        public async Task<SuccessDTO> SaveContact(Guid id, UserContactRequesttDTO contact)
        {
            var userObj = await _userRepository.GetByIdAsync(id);
            if (userObj is null)
            {
                throw new NotFoundException(Constant.UserNotFound);
            }

            var cType = Enum.Parse<ContactType>(contact.Type);
            userObj.AddOrUpdateContact(cType, contact.Number);

            _userRepository.Update(userObj);
            await _userRepository.SaveChangesAsync();

            return new SuccessDTO(Constant.RecordCreated);
        }

        public async Task<SuccessDTO> Update(Guid id, UserRequestDTO user)
        {
            var userObj = await _userRepository.GetByIdAsync(id);
            if (userObj is null)
            {
                throw new NotFoundException(Constant.UserNotFound);
            }

            var preFix = Enumeration.FromDisplayName<UserPrefix>(user.Prefix);
            userObj.Name = user.Name;
            userObj.Address = user.Address;
            userObj.Job = user.Job;
            userObj.Prefix = preFix;

            foreach (var c in user.Contacts)
            {
                var cType = Enum.Parse<ContactType>(c.Type);
                userObj.AddOrUpdateContact(cType, c.Number, c.Id);
            }

            _userRepository.Add(userObj);
            await _userRepository.SaveChangesAsync();

            return new SuccessDTO(Constant.RecordUpdated);
        }

        public async Task<SuccessDTO> UpdateContact(Guid id, UserContactRequesttDTO contact)
        {
            var userObj = await _userRepository.GetByIdAsync(id);
            if (userObj is null)
            {
                throw new NotFoundException(Constant.UserNotFound);
            }

            var cType = Enum.Parse<ContactType>(contact.Type);
            userObj.AddOrUpdateContact(cType, contact.Number, contact.Id);

            _userRepository.Update(userObj);
            await _userRepository.SaveChangesAsync();

            return new SuccessDTO(Constant.RecordUpdated);
        }
    }
}

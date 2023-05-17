using BoilerPlate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoilerPlate.Service.DTO.Response
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public List<UserContactResponseDto> Contacts { get; set; }
    }

    public static class UserResponseDTOExtension
    {
        public static UserResponseDTO ToDTO(this User model)
        {
            return new UserResponseDTO()
            {
                Id = model.Id,
                Name = $"{model.Prefix} {model.Name}",
                Address = model.Address,
                Job = model.Job,
                Contacts = model.Contacts.Select(x => x.ToDTO()).ToList()
            };
        }
    }
}

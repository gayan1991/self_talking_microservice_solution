using BoilerPlate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Service.DTO.Response
{
    public class UserContactResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

    }

    public static class UserContactResponseDtoExtension
    { 
        public static UserContactResponseDto ToDTO(this UserContact model)
        {
            return new UserContactResponseDto()
            {
                Id = model.Id,
                Type = model.Type.ToString(),
                Number = model.Number
            };
        }
    }
}

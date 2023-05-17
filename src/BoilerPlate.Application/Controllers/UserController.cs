using BoilerPlate.Domain.Exceptions;
using BoilerPlate.Service.DTO.Request;
using BoilerPlate.Service.DTO.Response;
using BoilerPlate.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Controllers
{
    [Route("api/boilerplate/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorDTO(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _userService.GetUser(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorDTO(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserRequestDTO user)
        {
            try
            {
                return Ok(await _userService.Save(user));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorDTO(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequestDTO user)
        {
            try
            {
                return Ok(await _userService.Update(id, user));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorDTO(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/contact")]
        public async Task<IActionResult> UpdateUser(Guid id, UserContactRequesttDTO contact)
        {
            try
            {
                return Ok(await _userService.UpdateContact(id, contact));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorDTO(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

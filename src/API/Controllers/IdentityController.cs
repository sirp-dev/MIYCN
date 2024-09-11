using API.DTO;
using Application.Commands.DTO;
using Application.Commands.IdentityCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("emailupdatetoken")]
        public async Task<IActionResult> UpdateEmailRequestToken([FromBody] EmailUpdateRequestTokenDto requestDto)
        {
            var command = new EmailUpdateRequestTokenCommand
            {
                UserId = requestDto.UserId,
                NewEmail = requestDto.NewEmail
            };

            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("forgottenpassword")]
        public async Task<IActionResult> ForgottenPassword([FromBody] ForgottenPasswordDto requestDto)
        {
            var command = new ForgottenPasswordCommand
            {
                Email = requestDto.Email
            };

            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };

            LoginResponse response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registerDto)
        {
            var command = new RegisterCommand
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Password = registerDto.Password
            };

            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}

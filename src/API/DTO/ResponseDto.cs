namespace API.DTO
{
    public class ResponseDto
    {
    }
    public class EmailUpdateRequestTokenDto
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
    }

    public class ForgottenPasswordDto
    {
        public string Email { get; set; }
    }
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}

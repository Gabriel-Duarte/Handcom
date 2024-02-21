namespace Handcom.Domain.Dto.Responses
{
    public class RegisterUserResponseDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public RegisterUserResponseDto()
        {
        }

        public RegisterUserResponseDto(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

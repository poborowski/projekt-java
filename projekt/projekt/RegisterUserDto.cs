namespace projekt
{
    public class RegisterUserDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string ConfirmPassword { get; set; }
        public string Password { get; set; }

    }
}


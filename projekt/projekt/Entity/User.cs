namespace projekt.Entity
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email{ get; set; }

        public string PasswordHash  { get; set; }

        public bool IsAdmin { get; set; } = false;
        public int RoleId   { get; set; }

        public virtual Role Role { get; set; }
    }
}

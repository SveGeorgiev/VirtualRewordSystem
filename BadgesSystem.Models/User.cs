namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        //public Nullable<UserRole> UserRole { get; set; }
    }

    public enum UserRole
    {
        Administrator = 1,
        Manager = 2,
        Programmer = 3,
        Tester = 4
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_Repository.Modals
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public int? Role { get; set; }

        public bool? Isactive { get; set; }
    }
    public class Userlogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;

namespace UserApi.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
﻿using Sat.Recruitment.Core.Entities.Interfaces;

namespace Sat.Recruitment.Core.Entities
{
    public class User : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}

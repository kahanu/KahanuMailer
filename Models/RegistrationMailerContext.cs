﻿using KahanuMailer;

namespace Models
{
    public class RegistrationMailerContext : BaseContext
    {
        public string Now { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdminMessage { get; set; }
        public string RecipientMessage { get; set; }
    }
}

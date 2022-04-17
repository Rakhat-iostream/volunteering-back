﻿using System;
using Microsoft.AspNetCore.Identity;
using Vol.Infrastructure;

namespace Vol.Domain.Users
{
    public class User : IdentityUser<Guid>, IAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public Guid? PartnerId { get; set; }

        public string? PreferLanguage { get; set; }

        public bool IsExternal { get; set; }

        public DateTime CreationDate { get; set; }

        public void SetFullName(string name, string surname)
        {
            FullName = $"{name} {surname}";
        }

        public User()
        {
        }
    }
}

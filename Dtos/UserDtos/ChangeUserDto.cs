﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UserDtos
{
    public class ChangeUserDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? AboutMe { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
    }
}

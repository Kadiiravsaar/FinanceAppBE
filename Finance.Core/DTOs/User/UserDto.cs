﻿using Finance.Core.DTOs.Stock;

namespace Finance.Core.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
	
}

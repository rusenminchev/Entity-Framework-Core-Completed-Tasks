﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Dto
{
    public class UserInfoDto
    {
        public int UsersCount { get; set; }
        public UserDetailsDto[] Users { get; set; }
    }
}

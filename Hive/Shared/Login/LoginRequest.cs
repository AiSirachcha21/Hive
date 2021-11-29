﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shared.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

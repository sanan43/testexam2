﻿using System.ComponentModel.DataAnnotations;

namespace bilet2.ViewModel.Auth
{
    public class LoginVM
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Shared_Models
{
    public class UserRegisterDtoReq
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(250)]
        public string Password { get; set; }

        [Required, StringLength(50)]
        public List<string> Roles { get; set; }
    }
}

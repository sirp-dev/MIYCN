﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DTO
{
    public class ForgottenPasswordResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
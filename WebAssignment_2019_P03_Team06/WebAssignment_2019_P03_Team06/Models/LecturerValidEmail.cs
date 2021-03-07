﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAssignment_2019_P03_Team06.DAL;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class LecturerValidEmail : ValidationAttribute
    {
        private LecturerDAL lecturerContext = new LecturerDAL();

        public override bool IsValid(object value)
        {
            string email = Convert.ToString(value);
            if (lecturerContext.IsEmailExist(email))
            
                return false;
            // validation failed        
            else
                return true;
            // validation passed      
        }
    }
}

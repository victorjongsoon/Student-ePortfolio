using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAssignment_2019_P03_Team06.DAL;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class ValidateEmailExists : ValidationAttribute
    {
        private StudentDAL staffContext = new StudentDAL();
        

        public override bool IsValid(object value)
        {
            string email = Convert.ToString(value); if (staffContext.IsEmailExist(email)) return false;
            // validation failed        
            else
                return true;
            // validation passed      
        }


    }
}

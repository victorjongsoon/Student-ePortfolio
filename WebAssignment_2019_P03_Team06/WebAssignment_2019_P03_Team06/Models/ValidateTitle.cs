using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebAssignment_2019_P03_Team06.DAL;

namespace WebAssignment_2019_P03_Team06.Models
{
    public class ValidateTitle : ValidationAttribute
    {
        private ProjectDAL projectContext = new ProjectDAL();

        public override bool IsValid(object value)
        {
            string title = Convert.ToString(value);
            if (title.Length > 0 || projectContext.IsTitleExist(title))
                return true;
            else 
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface ITuliaRepository
    {
        // returns a string with information regarding the success or failure
        // of the method.
        public string CreateAccount();

        
    }
}

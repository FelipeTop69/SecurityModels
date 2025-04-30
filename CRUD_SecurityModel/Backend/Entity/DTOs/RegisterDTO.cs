using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RegisterDTO
    {
        // Datos Person
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DocumentType { get; set; }
        public string BloodType { get; set; }

        // Datos User
        public string Username { get; set; }
        public string Password { get; set; }

        public int RolId { get; set; }
    }
}

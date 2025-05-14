using DevIntel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.DTO
{
    public class PromoteUserDto
    {
        public string Email { get; set; }
        public Role NewRole { get; set; }
    }
}

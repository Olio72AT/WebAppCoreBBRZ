using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.Models
{
    public class Admin
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = null;
        public string Email { get; set; } = null;
        public string PW { get; set; } = null;
        public string SessionID { get; set; } = null;
        public bool Active { get; set; } = true;

        // Übung
        public RoleType Role { get; set; } = RoleType.LoggingOut;
    }

    public enum RoleType
    {
        LoggingOut, Admin,User
    }
}

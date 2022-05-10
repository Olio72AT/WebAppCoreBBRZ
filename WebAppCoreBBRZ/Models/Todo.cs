using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.Models
{
    [Serializable]
    public class Todo
    {
        public int Id { get; set; }
        public string Aufgabe { get; set; } = "";
        public string Beschreibung { get; set; } = null;
        public bool Done { get; set; } = false;
        
        [Display(Name = "User")]
        public int? FKUser { get; set; } = null;
        
        public bool Active { get; set; } = true;

    }
}

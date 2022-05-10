using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.Models
{
    public class Calculator
    {
        public int Id { get; set; }
        public List<double> Results { get; set; } = new List<double>() { 0.0, 0.0, 0.0, 0.0}; 
        public string Register1 { get; set; } = "0";
        public string SessionID { get; set; } = null;

    }

    public enum calctype 
    {
        plus,
        minus,
        multiply,
        divide,
        top3,
        average
    }

}

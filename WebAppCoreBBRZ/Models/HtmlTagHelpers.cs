using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.Models
{
    [Serializable]
    public class HtmlTagHelpers
    {
        public int id { get; set; }
        public string TextBox { get; set; }

        [DataType(DataType.MultilineText)]
        public string TextArea { get; set; }

        public bool CheckBox { get; set; }

        public bool CheckBox2 { get; set; }

        public radiotype RadioButton { get; set; }

        public dropdowntype DropDown1 { get; set; }

        public IEnumerable<string> DropDown2 { get; set; }

        public IEnumerable<string> ListBox { get; set; }

        public int Hidden { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.MultilineText)]
        public string Editor { get; set; }

        [MinLength(3, ErrorMessage="UserId zu wenig Zeichen(3)")]
        [MaxLength(25, ErrorMessage = "UserId zu viele Zeichen(25)")]
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        public string Pin { get; set; }

        [DataType(DataType.Password)]
        [Compare("Pin")]
        public string RepeatPin { get; set; }


    }

    public enum radiotype
    {
        radio1, radio2
    }

    public enum dropdowntype
    {
        FirstItem,
        SecondItem,
        ThirdItem
    }
}

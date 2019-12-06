using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PRD_High_School.Models
{
    public class StudentModel
    {
       

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Field Require")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "ID Field Require")]
        public string StudentId { get; set; }
        [Required(ErrorMessage = "Class Field Require")]
        public string Class { get; set; }
        [Required(ErrorMessage = "Section Field Require")]
        public string Section { get; set; }
        [Required(ErrorMessage = "Fixed Price Field Require")]
        public double FixedPrice { get; set; }
        [Required(ErrorMessage = "Ammount Field Require")]
        public double Ammount { get; set; }

        [Required(ErrorMessage = "Collection Date Field Require")]
       /* [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]*/
        public string CollectionDate { get; set; }

        [Required(ErrorMessage = "Type of Payment  Field Require")]

        public string PaymentType { get; set; }

        [Required(ErrorMessage = "Status  Field Require")]

        public string Status { get; set; }
        [Required(ErrorMessage = "Date of Birth Field Require")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd MM yyyy}")]
        public string DateofBirth { get; set; }

    }
}

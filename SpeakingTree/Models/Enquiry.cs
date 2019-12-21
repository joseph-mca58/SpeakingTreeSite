using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpeakingTree.Models
{
    public class Enquiry
    {
        [Required(ErrorMessage = "Please enter Name"), MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter School"), MaxLength(200)]
        public string School { get; set; }
        [Required(ErrorMessage = "Please enter EmailId"), MaxLength(200)]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter Contact number"), MaxLength(200)]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter Query"), MaxLength(200)]
        public string Query { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace SpeakingTree.Models
{
    public class User
    {
        //Id is actual user id to log into website
        [Required(ErrorMessage = "Please enter Username"), MaxLength(15)]
        public string Id { get; set; }
        //Name = name of School / Student / User
        [Required(ErrorMessage = "Please enter Name"), MaxLength(200)]
        public string Name { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please enter Password"), MaxLength(16)]

        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter City"), MaxLength(150)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please select State")]
        public int State { get; set; }
        [Required(ErrorMessage = "Please select District")]
        public int District { get; set; }
        [Required(ErrorMessage = "Please enter Area / Locality"), MaxLength(150)]
        public string Area { get; set; }
        [Required(ErrorMessage = "Please enter Pincode")]
        public int Pincode { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter EmailId"), MaxLength(150)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Id is not valid.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter Contact Number"),MinLength(10)]
        
        public int ContactNo { get; set; }

        public string UserType { get; set; }//Student / School / Other (admin)

        public SelectList lstRole { get; set; }

        public SelectList lstState { get; set; }

        public SelectList lstDistrict { get; set; }

        public Student student { get; set; }
        public School school { get; set; }

        [Required(ErrorMessage = "Please enter new password"), MaxLength(16)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter confirm password"), MaxLength(16)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

    }
}
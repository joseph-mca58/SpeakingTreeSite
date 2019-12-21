using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeakingTree.Data.DataContracts;
using webModel = SpeakingTree.Models;


namespace SpeakingTree.Extensions
{
    public static class ViewModelExtension
    {
        internal static List<SelectListItem> ToSelectListFromList(this IEnumerable<Role> Roles)
        {
            if (Roles == null || !Roles.Any())
                return new List<SelectListItem>();
            return Roles.Select(p => new SelectListItem
            {
                Text = p.Id.ToString(),
                Value = p.Name.Trim()
            }).ToList();
        }

        internal static Enquiry ToBusinessEnquiry(webModel.Enquiry ViewModel)
        {
            if (ViewModel == null)
                return new Enquiry();
            return new Enquiry
            {
                Name = ViewModel.Name,
                School = ViewModel.School,
                EmailId = ViewModel.EmailId,
                ContactNo = ViewModel.ContactNo,
                Query = ViewModel.Query
            
            };
        }

        internal static User ToBusinessUser(webModel.User ViewModel)
        {
            if (ViewModel == null)
                return new User();
            return new User
            {
                Id = ViewModel.Id,
                Name = ViewModel.Name,
                RoleId = ViewModel.RoleId,
                Password = ViewModel.Password,
                City = ViewModel.City,
                State = ViewModel.State,
                District = ViewModel.District,
                Area = ViewModel.Area,
                Pincode = ViewModel.Pincode,
                Address = ViewModel.Address,
                EmailId = ViewModel.EmailId,
                ContactNo = ViewModel.ContactNo,
                student = ToBusinessStudent(ViewModel.student),
                school = ToBusinessSchool(ViewModel.school)
            };
        }
        internal static School ToBusinessSchool(webModel.School ViewModel)
        {
            if (ViewModel == null)
                return new School();
            return new School
            {
                Id = ViewModel.Id,
                SchoolId = ViewModel.SchoolId,
                Branch = ViewModel.Branch,
                ExamOption = ViewModel.ExamOption,
                Strength = ViewModel.Strength,
                PersonContact = ViewModel.PersonContact

            };
        }
        internal static Student ToBusinessStudent(webModel.Student ViewModel)
        {
            if (ViewModel == null)
                return new Student();
            return new Student
            {
                Id = ViewModel.Id,
                StudetId = ViewModel.StudetId,
                SchoolId = ViewModel.SchoolId,
                StudentClass = ViewModel.StudentClass,
                ParentName = ViewModel.ParentName
            };
        }
    }
}
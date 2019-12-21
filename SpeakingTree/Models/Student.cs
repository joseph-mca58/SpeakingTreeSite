using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeakingTree.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int StudetId { get; set; }
        public int SchoolId { get; set; }
        public int StudentClass { get; set; }
        public int ParentName { get; set; }
    }
}
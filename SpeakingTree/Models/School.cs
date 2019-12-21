using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeakingTree.Models
{
    public class School
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Branch { get; set; }
        public string ExamOption { get; set; }
        public int Strength { get; set; }
        public string PersonContact { get; set; }
    }
}
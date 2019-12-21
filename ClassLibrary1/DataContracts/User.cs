using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeakingTree.Data.DataContracts
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public int District { get; set; }
        public string Area { get; set; }
        public int Pincode { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public int ContactNo { get; set; }

        //public char UserType { get; set; }//Student / School / Other (admin)

        //public List<Role> lstRole { get; set; }

        //public List<State> lstState { get; set; }

        //public List<District> lstDistrict { get; set; }


        public Student student { get; set; }
        public School school { get; set; }


    }
}
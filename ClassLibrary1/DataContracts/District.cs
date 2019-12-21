using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeakingTree.Data.DataContracts
{
    public class District
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }

    }
}
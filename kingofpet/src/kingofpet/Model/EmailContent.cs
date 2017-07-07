using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingOfPetsAPI.Model
{
    public class EmailContent
    {
        public string subject { get; set; }
        public string textBody { get; set; }
        public string[] attachements { get; set; }
    }
}

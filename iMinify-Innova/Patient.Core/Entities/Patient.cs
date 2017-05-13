using Patients.Core.Entities.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Core.Entities
{
    public class Patient:BaseEntity
    {
        public string FullName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CareSpec { get; set; }
        public string Photo { get; set; }
    }
}

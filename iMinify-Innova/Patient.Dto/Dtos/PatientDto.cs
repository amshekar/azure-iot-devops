using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dto.Dtos
{
    public class PatientDto:BaseDto
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

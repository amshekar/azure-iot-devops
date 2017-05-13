using Patients.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Data.Configurations
{
    public class PatientConfiguration : EntityTypeConfiguration<Patient>
    {
        public PatientConfiguration()
        {
            Property(c => c.FullName).IsRequired().HasMaxLength(100);
            Property(c => c.DOB).HasMaxLength(10);
            Property(c => c.PhoneNo).HasMaxLength(10);
            Property(c => c.Gender).HasMaxLength(10);
            Property(c => c.Address).IsRequired().HasMaxLength(300);
            Property(c => c.CareSpec).HasMaxLength(25);
            Property(c => c.Photo).HasMaxLength(100);
        }
    }
}

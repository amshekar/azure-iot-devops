using Patients.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Core.Data.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
    }
}

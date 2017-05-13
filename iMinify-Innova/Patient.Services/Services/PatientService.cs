using Patients.Core.Data;
using Patients.Core.Entities;
using Patients.Core.Services;

namespace Patients.Services.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        public PatientService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}

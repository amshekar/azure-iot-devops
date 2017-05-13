using System.Reflection;

namespace Patients.Web.References
{
    public static class ReferencedAssemblies
    {
        public static Assembly Services
        {
            get { return Assembly.Load("Patients.Services"); }
        }

        public static Assembly Repositories
        {
            get { return Assembly.Load("Patients.Data"); }
        }

        public static Assembly Dto
        {
            get
            {
                return Assembly.Load("Patients.Dto");
            }
        }

        public static Assembly Domain
        {
            get
            {
                return Assembly.Load("Patients.Core");
            }
        }
    }
}

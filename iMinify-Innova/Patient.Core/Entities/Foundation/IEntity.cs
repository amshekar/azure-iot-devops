using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Core.Entities.Foundation
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}

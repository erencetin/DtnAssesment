using DtnAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Core.Interfaces
{
    public interface ILightningRepository
    {
        IEnumerable<Lightning> GetLightnings();
    }
}

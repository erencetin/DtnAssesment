using DtnAssesment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Infrastructure.Services
{
    public class LogService : ILogService
    {
        public void Log(string message)
        {
            //ignored persisting logs on filesystem. Writing only on screen.
            Console.WriteLine(message);
        }
    }
}

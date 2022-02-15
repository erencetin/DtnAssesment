using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Core.Models
{
    public class Lightning
    {
        public int FlashType { get; set; }
        public long StrikeTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }
        public long ReceivedTime { get; set; }
        public int NumberOfSensors { get; set; }
        public int Multiplicity { get; set; }
    }
}

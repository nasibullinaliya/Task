using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    /// class for receivers
    /// </summary>
    class Receiver : Point
    {
        public Receiver()
        {
            S = new List<double>();
        }

        public override double X { get; set; }
        public override double Y { get; set; }
        /// <summary>
        /// Distance from transmitter to receiver
        /// </summary>
        public List<double> S { get; set; }
    }
}

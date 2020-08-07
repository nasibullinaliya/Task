using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    /// class for point
    /// </summary>
    public class Point
    {
        public Point()
        {
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public virtual double X { get; set; }
        public virtual double Y { get; set; }
    }
}

using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    /// class for transmitter
    /// </summary>
    class Transmitter
    {
        public List<Point> Track;

        public Transmitter()
        {
            Track = new List<Point>();
        }

        /// <summary>
        /// Get track coordinates for transmitter by formulas for distance between two points 
        /// where (x1, y1), (x2, y2), (x3, y3) - coordinates of receivers
        /// S1^2 = (x4-x1)^2 + (y4-y1)^2
        /// S2^2 = (x4-x2)^2 + (y4-y2)^2
        /// S3^2 = (x4-x3)^2 + (y4-y3)^2
        /// </summary>
        /// <param name="receiver1"></param>
        /// <param name="receiver2"></param>
        /// <param name="receiver3"></param>
        public void GetTransmitterTrack(Receiver receiver1, Receiver receiver2, Receiver receiver3)
        {
            var n = receiver3.S.Count;
            for (var i = 0; i < n; i++)
            {
                var a = -2 * receiver1.X;
                var b = -2 * receiver1.Y;
                var d = -2 * receiver2.X;
                var e = -2 * receiver2.Y;
                var h = -2 * receiver3.X;
                var g = -2 * receiver3.Y;

                var coef = new double[3];
                coef[0] = Math.Pow(receiver1.S[i], 2) - Math.Pow(receiver1.X, 2) - Math.Pow(receiver1.Y, 2);
                coef[1] = Math.Pow(receiver2.S[i], 2) - Math.Pow(receiver2.X, 2) - Math.Pow(receiver2.Y, 2);
                coef[2] = Math.Pow(receiver3.S[i], 2) - Math.Pow(receiver3.X, 2) - Math.Pow(receiver3.Y, 2);

                var matrix = Matrix<double>.Build.DenseOfArray(new double[,] { { a, b, 1 }, { d, e, 1 }, { h, g, 1 } });
                var coefs = Vector<double>.Build.Dense(coef);
                var x = matrix.Solve(coefs);
                this.Track.Add(new Point(x[0], x[1]));
            }
        }
    }
}

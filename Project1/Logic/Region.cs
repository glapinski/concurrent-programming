using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Region
    {
        public int size { get; set; }

        public List<Ball> _balls { get; set; }
        private Task changePosition;
    }
}

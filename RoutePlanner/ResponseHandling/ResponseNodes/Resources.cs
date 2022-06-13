using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class Resources
    {
        public Route route { get; set; }

        public override string ToString()
        {
            return $"Resources: {route}";
        }
    }
}

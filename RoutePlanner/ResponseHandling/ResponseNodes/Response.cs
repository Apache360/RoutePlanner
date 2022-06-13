using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class Response
    {
        public ResourceSets resourceSets { get; set; }

        public override string ToString()
        {
            return $"Response: {resourceSets}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ResourceSets
    {
        public ResourseSet resourseSet  { get; set; }

        public override string ToString()
        {
            return $"ResourceSets: {resourseSet}";
        }
    }
}

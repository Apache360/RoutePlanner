using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ResourseSet
    {
        public Resources resources { get; set; }

        public override string ToString()
        {
            return $"ResourseSet: {resources}";
        }
    }
}

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ResourceSets
    {
        public ResourseSet ResourseSet  { get; set; }

        public override string ToString()
        {
            return $"ResourceSets: {ResourseSet}";
        }
    }
}

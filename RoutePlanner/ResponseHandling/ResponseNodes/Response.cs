namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class Response
    {
        public ResourceSets ResourceSets { get; set; }

        public override string ToString()
        {
            return $"Response: {ResourceSets}";
        }
    }
}

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class Resources
    {
        public Route Route { get; set; }

        public override string ToString()
        {
            return $"Resources: {Route}";
        }
    }
}

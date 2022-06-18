namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ResourseSet
    {
        public Resources Resources { get; set; }

        public override string ToString()
        {
            return $"ResourseSet: {Resources}";
        }
    }
}

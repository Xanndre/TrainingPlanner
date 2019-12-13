using System;

namespace TrainingPlanner.Core.Helpers
{
    public class TrainingFilterData
    {
        public DateTime? DateLowerBound { get; set; }
        public DateTime? DateUpperBound { get; set; }
        public string Level { get; set; }
        public string Title { get; set; }
    }
}

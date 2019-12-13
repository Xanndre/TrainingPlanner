using System.Collections.Generic;

namespace TrainingPlanner.Core.Helpers
{
    public class TrainerFilterData
    {
        public string Location { get; set; }
        public string Keywords { get; set; }
        public List<int?> SportIds { get; set; }
    }
}

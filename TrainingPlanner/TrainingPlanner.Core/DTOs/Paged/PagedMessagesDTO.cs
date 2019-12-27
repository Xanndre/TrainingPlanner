using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.Chat;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedMessagesDTO : PagedDTO
    {
        public IEnumerable<MessageDTO> Messages { get; set; }
        public int CurrentPage { get; set; }
    }
}

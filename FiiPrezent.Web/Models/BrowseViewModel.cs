using System.Collections.Generic;

namespace FiiPrezent.Web.Models
{
    public class BrowseViewModel
    {
        public IEnumerable<BrowseEventViewModel> Events { get; set; }
        public FilterEventsViewModel Filter { get; set; }
    }
}
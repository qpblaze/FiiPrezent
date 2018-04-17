namespace FiiPrezent.Web.Models
{
    public class FilterEventsViewModel
    {
        private string _name;
        private string _location;
        private string _date;

        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToLower();
        }

        public string Location
        {
            get => _location;
            set => _location = value.Trim().ToLower();
        }

        public string Date
        {
            get => _date;
            set => _date = value.Trim().ToLower();
        }
    }
}
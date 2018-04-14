namespace FiiPrezent.Models
{
    public class FilterEventsViewModel
    {
        private string _name;
        private string _location;
        private string _date;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.Trim().ToLower();
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value.Trim().ToLower();
            }
        }

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value.Trim().ToLower();
            }
        }
    }
}
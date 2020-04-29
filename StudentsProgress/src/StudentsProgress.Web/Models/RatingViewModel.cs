using System.ComponentModel;

namespace StudentsProgress.Web.Models
{
    public class RatingViewModel
    {
        public string Subject { get; set; }

        [DisplayName("Semestr Points")]
        public int SemestrPoints { get; set; }

        [DisplayName("Sum Points")]
        public int SumPoints { get; set; }
    }
}

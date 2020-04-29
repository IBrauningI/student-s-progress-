using System.ComponentModel;

namespace StudentsProgress.Web.Models
{
    public class AttendanceViewModel
    {
        public string Subject { get; set; }

        [DisplayName("Count of Lectures")]
        public int LecturesCount { get; set; }

        [DisplayName("Count of Passes")]
        public int PassesCount { get; set; }
    }
}

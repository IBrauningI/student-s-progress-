// <copyright file="ErrorViewModel.cs" company="StudentPro">
//     Company copyright tag.
// </copyright>
namespace StudentPro.Models
{
    using System;

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public string GroupName { get; set; }
        public string FacultyName { get; set; }
    }
}

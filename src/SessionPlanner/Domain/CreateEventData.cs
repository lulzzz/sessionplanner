using System;
using System.ComponentModel.DataAnnotations;

namespace SessionPlanner.Domain
{
    public class CreateEventData
    {
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace event_page_web.Models
{
    public class Event
    {
       public int Id { get; set; }
        public string? CoverImagePath { get; set; }
        [Required]

        public string Title { get; set; }
       public string Description { get; set; }
       public DateTime Date { get; set; }

        public string? Place { get; set; }
    }


}

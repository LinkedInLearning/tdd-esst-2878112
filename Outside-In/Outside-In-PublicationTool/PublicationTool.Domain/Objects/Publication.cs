using System;
using System.ComponentModel.DataAnnotations;

namespace PublicationTool.Domain.Objects
{
    public class Publication
    {
        public Publication(string title, DateTime? date)
        {
            Title = title;
            Date = date;
        }

        public Publication()
        {
        }

        [Required]
        public string Title { get; set; }

        public DateTime? Date { get; set; }
    }
}

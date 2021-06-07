using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PublicationTool.Domain.Interfaces
{
    public class Result
    {
        public bool Success => !Errors.Any();

        public List<string> Errors { get; set; }

        public Result()
        {
            Errors = new List<string> { };
        }
    }
}
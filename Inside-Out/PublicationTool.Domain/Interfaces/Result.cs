using System.Collections.Generic;

namespace PublicationTool.Domain.Interfaces
{
    public class Result
    {
        public Result()
        {
            Errors = new List<string>();
        }

        public bool WasSuccessful => this.Errors.Count == 0;
        
        public List<string> Errors { get; private set; }
    }
}
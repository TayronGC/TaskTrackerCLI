using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerCLI.Models
{
    public enum Status { ToDo, InProgress, Done }
    internal class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Status Status { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }
    }
}

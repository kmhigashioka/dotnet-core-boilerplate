using System;

namespace Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool Done { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}

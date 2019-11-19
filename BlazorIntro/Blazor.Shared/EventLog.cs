using System;

namespace Blazor.Shared
{
    public class EventLog
    {
        public int Id { get; set; }
        public int? ProcessId { get; set; }
        public string LogType { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }
}
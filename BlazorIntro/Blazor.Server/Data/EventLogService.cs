using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Shared;

namespace Blazor.Server.Data
{
    public class EventLogService
    {
        private static int _id;
        private static readonly List<EventLog> Logs = new List<EventLog>();

        private static readonly string[] EventLogTypes = {
            "Trace", "Debug", "Information", "Warning", "Error"
        };

        public async Task<EventLog[]> GetEventLogsAsync(int cacheSize = 10)
        {
            var rng = new Random();

            var logs = await Task.FromResult(Enumerable.Range(1, 5)
                .Select(index => CreateLogEvent(
                    EventLogTypes[rng.Next(EventLogTypes.Length)],
                    rng.Next(1, 50)))
                    .ToArray());

            Logs.AddRange(logs);

            while (Logs.Count > cacheSize) 
                Logs.RemoveAt(0);

            return Logs.OrderByDescending(e => e.Id).TakeLast(cacheSize).ToArray();
        }
        private static EventLog CreateLogEvent(string eventType, int processId)
        {
            return new EventLog
            {
                Id = ++_id,
                LoggedAt = DateTime.UtcNow,
                LogType = eventType,
                ProcessId = processId,
                Message = CreateMessage(eventType)
            };
        }

        private static string CreateMessage(string eventType)
        {
            try
            {
                if (eventType == "ERR")
                {
                    throw new Exception($"A message that was logged as '{eventType}'.");
                }

                return $"A message that was logged as '{eventType}'.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.ToString();
            }
        }
    }
}

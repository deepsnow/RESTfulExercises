using System.Collections.Generic;
using System.Linq;
using TimeLogging.Models;

namespace TimeLogging.DataAccess
{
    public interface IIimeLogService
    {
        List<TimeLogViewModel> GetFiveLatestEntries();
        void SubmitTimeLog(TimeLogViewModel log);
    }

    public class TimeLogService : IIimeLogService
    {
        public List<TimeLogViewModel> GetFiveLatestEntries()
        {
            var timeLoggingContext = new TimeLoggingContext();

            var result = timeLoggingContext.Logs.Take(5).Select(l => new TimeLogViewModel()
            {
                UserId = l.UserId,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                Comment = l.Comment,
                Billable = l.Billable
            });
            return result.ToList();

        }

        public void SubmitTimeLog(TimeLogViewModel log)
        {
            var timeLoggingContext = new TimeLoggingContext();
            var newEntry = new Log()
            {
                Billable = log.Billable,
                Comment = log.Comment,
                EndTime = log.EndTime,
                StartTime = log.StartTime,
                UserId = "doug"
            };

            timeLoggingContext.Logs.Add(newEntry);
            timeLoggingContext.SaveChanges();
        }

    }
}
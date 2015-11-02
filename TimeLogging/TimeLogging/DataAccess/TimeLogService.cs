using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TimeLogging.Models;
using System;
using System.Data.Entity;

namespace TimeLogging.DataAccess
{
    public interface IIimeLogService
    {
        List<TimeLogViewModel> GetFiveLatestEntries();
        void SubmitTimeLog(TimeLogViewModel log);
        void AddLog(Log log);
        List<Log> GetEntriesByDate();
        Log FindLog(int? id);
    }

    public class TimeLogService : IIimeLogService
    {
        private ITimeLoggingContext _context;
        private IQueryableWrapper _query;

        public TimeLogService(ITimeLoggingContext context, IQueryableWrapper query)
        {
            _context = context;
            _query = query;
        }

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

        public void AddLog(Log log)
        {
            var timeLoggingContext = new TimeLoggingContext();

            timeLoggingContext.Logs.Add(log);
            timeLoggingContext.SaveChanges();
        }

        public List<Log> GetEntriesByDate()
        {
            // I don't know how to mock these first two forms:
            
            //var result = _context.Logs.Where(r => 
            //    (r.StartTime.Day == DateTime.UtcNow.Day)
            //    && (r.StartTime.Month == DateTime.UtcNow.Month)
            //    && (r.StartTime.Year == DateTime.UtcNow.Year));

            //var result = from r in _context.Logs
            //             where (r.StartTime.Day == DateTime.UtcNow.Day)
            //                && (r.StartTime.Month == DateTime.UtcNow.Month)
            //                && (r.StartTime.Year == DateTime.UtcNow.Year)
            //             select r;

            var result = _query.Where(_context.Logs, (r =>
                            (r.StartTime.Day == DateTime.UtcNow.Day)
                            && (r.StartTime.Month == DateTime.UtcNow.Month)
                            && (r.StartTime.Year == DateTime.UtcNow.Year)));

            return result.ToList();

        }

        public Log FindLog(int? id)
        {
            var timeLoggingContext = new TimeLoggingContext();

            return timeLoggingContext.Logs.Where(r => r.Id == id).SingleOrDefault<Log>();
        }
    }

    public interface IQueryableWrapper
    {
        IQueryable<TSource> Where<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
    }

    public class QueryableLogs : IQueryableWrapper
    {
        public IQueryable<TSource> Where<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.Where(predicate);
        }
    }
}
using WebApi.Models;

namespace WebApi.Repositories
{
    public static class LogsRepository
    {
        public static List<Log> LogsItems = new();


        static int _nextId = 1;

       public static void AddLogs(int level, string message)
        {

            var log = new Log();
            log.Id = _nextId;
            log.Level = level;
            log.Message = message;
            log.Timestamp = DateTime.Now;
            _nextId++;
            LogsItems.Add(log);
            




        }


    }
}

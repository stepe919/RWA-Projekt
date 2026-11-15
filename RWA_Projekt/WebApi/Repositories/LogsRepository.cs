using WebApi.Models;

namespace WebApi.Repositories
{
    public static class LogsRepository
    {
    

       public static void AddLogs(RwaLibraryLearnContext context,int level, string message)
        {

            var log = new Log();
            log.Level = level;
            log.Message = message;
            log.Timestamp = DateTime.Now;
            context.Logs.Add(log);
            context.SaveChanges();
            




        }


    }
}

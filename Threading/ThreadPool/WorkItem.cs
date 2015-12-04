using System.Threading;

namespace Threading.ThreadPool
{
   internal class WorkItem
   {
      internal WorkItem(WaitCallback task, object context)
      {
         Task = task;
         Context = context;
      }

      internal WaitCallback Task { get; set; }
      internal object Context { get; set; }
   }
}

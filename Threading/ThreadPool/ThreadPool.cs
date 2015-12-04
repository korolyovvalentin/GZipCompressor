using System.Collections.Generic;
using System.Threading;

namespace Threading.ThreadPool
{
   public class ThreadPool : IThreadPool
   {
      private readonly int concurrencyLevel;
      private readonly Queue<WorkItem> queue;
      private Thread[] threads;
      private bool shutdown;
      private int threadsWaiting;

      public ThreadPool(int concurrencyLevel)
      {
         queue = new Queue<WorkItem>();
         shutdown = false;
         threadsWaiting = 0;

         this.concurrencyLevel = concurrencyLevel;
      }

      public void QueueUserWorkItem(WaitCallback work, object obj)
      {
         var workItem = new WorkItem(work, obj);

         EnsureStarted();

         lock (queue)
         {
            queue.Enqueue(workItem);

            if (threadsWaiting > 0)
            {
               Monitor.Pulse(queue);
            }
         }
      }

      private void DoWork()
      {
         while (true)
         {
            WorkItem item;

            if (shutdown)
            {
               return;
            }

            lock (queue)
            {
               while (queue.Count == 0)
               {
                  threadsWaiting++;
                  try
                  {
                     Monitor.Wait(queue);
                  }
                  finally
                  {
                     threadsWaiting--;
                  }

                  if (shutdown)
                  {
                     return;
                  }
               }

               item = queue.Dequeue();
            }

            item.Task.Invoke(item.Context);
         }
      }

      private void EnsureStarted()
      {
         if (threads == null)
         {
            lock (queue)
            {
               if (threads == null)
               {
                  threads = new Thread[concurrencyLevel];
                  for (int i = 0; i < threads.Length; i++)
                  {
                     threads[i] = new Thread(DoWork);
                     threads[i].Start();
                  }
               }
            }
         }
      }

      public void Dispose()
      {
         shutdown = true;
      }
   }
}

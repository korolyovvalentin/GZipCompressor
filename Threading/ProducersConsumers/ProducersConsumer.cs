using System.Collections.Generic;
using Threading.ThreadPool;

namespace Threading.ProducersConsumers
{
   public class ProducersConsumer<T> where T : class
   {
      private readonly IThreadPool threadPool;
      private IConsumer<T> consumer;
      private List<IProducer<T>> producers;
      private IList<T> queue;

      public ProducersConsumer(IThreadPoolFactory factory)
      {
         threadPool = factory.Create();
      }

      public void Start()
      {
      }

      private void QueuePolling()
      {
         while (true)
         {
            T itemToConsume = null;

            lock (queue)
            {
               if (queue.Count > 0)
               {
                  itemToConsume = PollQueue();
               }
            }

            if (itemToConsume != null)
            {
               consumer.Consume(itemToConsume);
            }
         }
      }

      private T PollQueue()
      {
         var length = queue.Count;
         for (var i = length - 1; i >= 0; i--)
         {
            var item = queue[i];
            if (consumer.CanConsume(item))
            {
               queue.Remove(item);
               return item;
            }
         }
         return null;
      }
   }
}

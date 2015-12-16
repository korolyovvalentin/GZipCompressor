using System.Collections.Generic;
using System.Linq;
using Threading.ThreadPool;

namespace Threading.ProducersConsumers
{
   public class ProducersConsumer<T> where T : class
   {
      private readonly IThreadPool threadPool;
      private readonly IConsumer<T> consumer;
      private readonly List<IProducer<T>> producers;
      private readonly Queue<T> queue;
      private bool shutdown;

      public ProducersConsumer(IThreadPoolFactory factory, IConsumer<T> consumer, List<IProducer<T>> producers)
      {
         this.consumer = consumer;
         this.producers = producers;
         queue = new Queue<T>();
         threadPool = factory.Create();
         shutdown = false;
      }

      public void Start()
      {
         threadPool.QueueUserWorkItem(a => Consume(), new object());
         producers.ForEach(
            p => threadPool.QueueUserWorkItem(
               a => Produce(p), 
               new object()));
      }

      public void Stop()
      {
         shutdown = false;
         threadPool.Dispose();
      }

      private void Produce(IProducer<T> producer)
      {
         while (producer.CanContinue())
         {
            queue.Enqueue(producer.Produce());
         }
         shutdown = !producers.Any(p => p.CanContinue());
      }

      private void Consume()
      {
         while (!shutdown)
         {
            T itemToConsume = null;

            lock (queue)
            {
               if (queue.Count > 0)
               {
                  itemToConsume = queue.Dequeue();
               }
            }

            if (itemToConsume != null)
            {
               consumer.Consume(itemToConsume);
            }
         }
      }
   }
}

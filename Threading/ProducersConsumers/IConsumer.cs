namespace Threading.ProducersConsumers
{
   interface IConsumer<T>
   {
      bool CanConsume(T item);
      void Consume(T item);
   }
}

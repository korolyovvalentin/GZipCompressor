namespace Threading.ProducersConsumers
{
   public interface IProducer<T>
   {
      bool CanContinue();
      void Stop();
      T Produce();
   }
}

namespace Threading.ProducersConsumers
{
   public interface IProducer<T>
   {
      T Produce();
   }
}

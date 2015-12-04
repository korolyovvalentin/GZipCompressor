namespace Compression
{
   public class CompressionDispatcher
   {
      private ICompressorFactory compressorFactory;

      public CompressionDispatcher(ICompressorFactory compressorFactory)
      {
         this.compressorFactory = compressorFactory;
      }

      public void Compress()
      {
         
      }
   }
}

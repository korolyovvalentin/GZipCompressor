namespace Compression
{
   public interface ICompressor
   {
      byte[] Compress(byte[] input);
   }
}

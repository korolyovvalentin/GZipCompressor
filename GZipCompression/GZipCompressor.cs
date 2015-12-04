using System.IO;
using System.IO.Compression;
using Compression;

namespace GZipCompression
{
   public class GZipCompressor : ICompressor
   {
      private readonly Stream stream;

      public GZipCompressor(Stream stream)
      {
         this.stream = stream;
      }

      public byte[] Compress(byte[] input)
      {
         using (var gzStream = new GZipStream(stream, CompressionLevel.Optimal, true))
         {
            gzStream.Write(input, 0, input.Length);
         }
         return new byte[0];
      }
   }
}

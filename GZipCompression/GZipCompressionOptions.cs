using System.IO.Compression;

namespace GZipCompression
{
   public class GZipCompressionOptions
   {
      public CompressionLevel Level { get; set; }
      public CompressionMode Mode { get; set; }
   }
}

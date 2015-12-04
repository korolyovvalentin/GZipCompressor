using System;
using System.Threading;

namespace Threading.ThreadPool
{
   public interface IThreadPool : IDisposable
   {
      void QueueUserWorkItem(WaitCallback work, object obj);
   }
}

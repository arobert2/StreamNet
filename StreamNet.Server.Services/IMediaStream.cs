using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StreamNet.Server.Services
{
    public interface IMediaStream : IDisposable
    {
        string Path { get; set; }
        //new void Dispose();
    }
}

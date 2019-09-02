using System;
using System.Collections.Generic;
using System.Text;

namespace StreamNet.Server.Services
{
    public interface IMediaStream : IDisposable
    {
        string Path { get; set; }
        //new void Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamNet.Server.Services
{
    public class NetworkFileStream : FileStream
    {
        public override bool CanTimeout => true;
        public NetworkFileStream(string path, FileMode fm, FileAccess fa) : base(path, fm, fa)
        {
        }
    }
}

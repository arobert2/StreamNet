using StreamNetServer.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Services.Options
{
    public class FileStoreOptions
    {
        private string _videoPath { get; set; }
        private string _audioPath { get; set; }
        private string _dumpPath { get; set; }
        private string _rejectedPath { get; set; }
        private string _profileContent { get; set; }
        public string VideoPath { get => _videoPath; set { _videoPath = value.ReplaceUserProfileVariable(); } }
        public string AudioPath { get => _audioPath; set { _audioPath = value.ReplaceUserProfileVariable(); } }
        public string DumpPath { get => _dumpPath; set { _dumpPath = value.ReplaceUserProfileVariable(); } }
        public string RejectedPath { get => _rejectedPath; set { _rejectedPath = value.ReplaceUserProfileVariable(); } }
        public string ProfileContent { get => ProfileContent; set { _profileContent = value.ReplaceUserProfileVariable(); } }
    }
}

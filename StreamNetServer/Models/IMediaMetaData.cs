using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    interface IMediaMetaData
    {
        //Media Description Properties
        string Title { get; set; }
        string Subtitle { get; set; }
        int Rating { get; set; }
        string Comments { get; set; }

        //Media File Properties
        string Name { get; set; }
        string FolderPath { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        double Size { get; set; }
    }
}

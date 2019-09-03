using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamNet.Options
{
    public static class OptionsFactory
    {
        public static ConnectionString GetConnectionString() {
            string connectionStringData = string.Empty;
            using (StreamReader fs = new StreamReader(new FileStream("ConnectionString.json", FileMode.Open, FileAccess.Read)))
                connectionStringData = fs.ReadToEnd();
            dynamic connectionString = JsonConvert.DeserializeObject<ConnectionString>(connectionStringData);
            return connectionString;
        }

        public static FileStoreOptions GetFileStoreOptions()
        {          
            string fileStoreOptionsData = string.Empty;
            using (StreamReader fs = new StreamReader(new FileStream("FileStoreOptions.json", FileMode.Open, FileAccess.Read)))
                fileStoreOptionsData = fs.ReadToEnd();           
            dynamic fileStoreOptions = JsonConvert.DeserializeObject<FileStoreOptions>(fileStoreOptionsData);
            return fileStoreOptions;
        }
    }
}

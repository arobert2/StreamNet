using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamNet.Options
{
    public class OptionsFactory
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

        public static VideoRepositoryOptions GetVideoRepositoryOptions()
        {
            string videoRepoOptData = string.Empty;
            using (StreamReader fs = new StreamReader(new FileStream("VideoRepositoryOptions.json", FileMode.Open, FileAccess.Read)))
                videoRepoOptData = fs.ReadToEnd();
            dynamic videoRepoOpt = JsonConvert.DeserializeObject<VideoRepositoryOptions>(videoRepoOptData);
            return videoRepoOpt;
        }

        public T GetOptions<T>()
        {
            string options = string.Empty;
            string type = typeof(T).ToString();
            type = type.Substring(type.LastIndexOf(".") + 1);
            var fileName = type + ".json";
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                options = sr.ReadToEnd();
            dynamic optObj = JsonConvert.DeserializeObject<T>(options);
            return optObj;
        }
    }
}

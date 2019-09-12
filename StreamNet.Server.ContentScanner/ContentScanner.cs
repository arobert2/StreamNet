using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.ExtensionMethod;
using StreamNet.Options;
using System;
using System.IO;

namespace StreamNet.Server.ContentScanner
{
    public static class ContentScanner
    {
        private static ApplicationDbContext _dbContext;
        private static FileStoreOptions _fileStoreOptions;
        private static ConnectionString _connectionString;
        private static byte[] _defaultImagedata;

        public static void Main(string[] args)
        {
            
            _connectionString = OptionsFactory.GetConnectionString();                   //Get connection string options
            _fileStoreOptions = OptionsFactory.GetFileStoreOptions();                   //Get file store options
            _dbContext = new ApplicationDbContext(                                      //Create database context
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString.DefaultConnection).Options);

            //Fill default picture data
            using(FileStream fs = new FileStream("NoPicture.png", FileMode.Open, FileAccess.Read))
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    _defaultImagedata = ms.ToArray();
                }
            }

            bool error = false;                                                         //If an error was thrown.                     
            
            InitializeFileSystem();                                                     //Make folders if they don't exist.
            var filenames = Directory.GetFiles(_fileStoreOptions.DumpPath);
            if(filenames.Length == 0)
            {
                System.Diagnostics.Debug.WriteLine("No new files to add.");
                return;
            }
            foreach (var fp in filenames)          //Create DB entries and copy files to correct locations.
            {
                try
                {
                    CopyFileToMediaLibrary(fp);                                         
                }
                catch (UnsupportedFileException ex )                                    //If unsupported type move to rejected path.
                {
                    var adminMessage = CreateAdminMessage(string.Format("Failed to add {0} to database. File type {1} incompatible.", ex.FileName, ex.MediaType));
                    _dbContext.AdminMessages.Add(adminMessage);
                    //Rejected path
                    string rejectpath = Path.Combine(_fileStoreOptions.RejectedPath, Path.GetFileName(fp));
                    //Move rejected file
                    File.Move(fp, rejectpath);
                    error = true;
                    continue;
                }
                catch(Exception ex)                                                     //Halt program if other error.
                {
                    var adminMessage = CreateAdminMessage(string.Format("ContentScanner failed with the following error: {0}", ex.Message));
                    _dbContext.AdminMessages.Add(adminMessage);
                    error = true;
                    throw ex;
                }
            }
            if (error)
                System.Diagnostics.Debug.WriteLine("Process ended with errors.");       //Report program ended with errors.
            else
                System.Diagnostics.Debug.WriteLine("Process ended successfully.");      //Report program ended without errors.
        }

        private static void CopyFileToMediaLibrary(string path)
        {
            var movieEntity = CreateVideoMetaData(path);
            _dbContext.Videos.Add(movieEntity);

            var adminMessage = CreateAdminMessage(string.Format("A new video called {0} has been added to the data base.", movieEntity.Title));
            _dbContext.AdminMessages.Add(adminMessage);

            if (_dbContext.SaveChanges() < 0)
                throw new Exception("failed to save DB entry");
            string successdirectory = Path.Combine(_fileStoreOptions.VideoPath, movieEntity.Id.ToString());
            string successpath = Path.Combine(successdirectory, Path.GetFileName(path));
            if (!Directory.Exists(successdirectory))
                Directory.CreateDirectory(successdirectory);
            File.Move(path, successpath);
        }

        private static VideoMetaData CreateVideoMetaData(string path)
        {
            string mediaType = string.Empty;
            new FileExtensionContentTypeProvider()
                .TryGetContentType(Path.Combine(_fileStoreOptions.DumpPath + path), out mediaType);
            if (!mediaType.CompatibilityCheck())
                throw new UnsupportedFileException(Path.GetFileName(path), mediaType, "Incompatible media type");

            var movieEntity = new VideoMetaData()
            {
                Title = Path.GetFileNameWithoutExtension(path),
                FileName = Path.GetFileName(path),
                MediaType = mediaType,
                FileSize = new FileInfo(path).Length,
                MD5 = path.GetMd5(),
                CoverArt = _defaultImagedata,
                CoverArtContentType = "image/png"
            };
            return movieEntity;
        }

        private static AdminMessage CreateAdminMessage(string message)
        {
            var adminMessage = new AdminMessage()
            {
                Body = message,
                Read = false,
                TimeStamp = DateTime.Now
            };
            return adminMessage;
        }

        private static void InitializeFileSystem()
        {
            if (!Directory.Exists(_fileStoreOptions.AudioPath))
                Directory.CreateDirectory(_fileStoreOptions.AudioPath);
            if (!Directory.Exists(_fileStoreOptions.DumpPath))
                Directory.CreateDirectory(_fileStoreOptions.DumpPath);
            if (!Directory.Exists(_fileStoreOptions.ProfileContent))
                Directory.CreateDirectory(_fileStoreOptions.ProfileContent);
            if (!Directory.Exists(_fileStoreOptions.RejectedPath))
                Directory.CreateDirectory(_fileStoreOptions.RejectedPath);
            if (!Directory.Exists(_fileStoreOptions.VideoPath))
                Directory.CreateDirectory(_fileStoreOptions.VideoPath);
        }
    }
}

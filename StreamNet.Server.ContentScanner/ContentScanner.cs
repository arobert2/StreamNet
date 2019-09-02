using Microsoft.EntityFrameworkCore;
using StreamNet.Server.DomainEntities.Data;
using StreamNet.Server.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using StreamNet.Server.DomainEntities.Entities;
using Microsoft.AspNetCore.StaticFiles;
using System.Linq;
using StreamNet.Server.ExtensionMethod;
using System.Security.Cryptography;

namespace StreamNet.Server.ContentScanner
{
    public static class ContentScanner
    {
        private static ApplicationDbContext _dbContext;
        private static FileStoreOptions _fileStoreOptions;
        private static ConnectionString _connectionString;

        public static void Main(string[] args)
        {
            _connectionString = OptionsFactory.GetConnectionString();                   //Get connection string options
            _fileStoreOptions = OptionsFactory.GetFileStoreOptions();                   //Get file store options
            _dbContext = new ApplicationDbContext(                                      //Create database context
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString.DefaultConnection).Options);

            List<string> rejectedFiles = new List<string>();
            //Make folders if they don't exist.
            InitializeFileSystem();
            //Create DB entries and copy files to correct locations.
            foreach (var fp in Directory.GetFiles(_fileStoreOptions.DumpPath))
            {
                try
                {
                    CopyFileToMediaLibrary(fp);
                }
                catch (UnsupportedFileException ex )
                {
                    System.Diagnostics.Debug.WriteLine(ex.MediaType + " Is not a compatible media type.");
                    //Rejected path
                    string rejectpath = Path.Combine(_fileStoreOptions.RejectedPath, Path.GetFileName(fp));
                    //Move rejected file
                    File.Move(fp, rejectpath);
                    continue;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static void CopyFileToMediaLibrary(string path)
        {
            string mediaType = string.Empty;
            new FileExtensionContentTypeProvider()
                .TryGetContentType(Path.Combine(_fileStoreOptions.DumpPath + path), out mediaType);
            if (!mediaType.CompatibilityCheck())
                throw new UnsupportedFileException(mediaType, "Incompatible media type");

            var movieEntity = new VideoMetaData()
            {
                Title = Path.GetFileNameWithoutExtension(path),
                FileName = Path.GetFileName(path),
                MediaType = mediaType,
                FileSize = new FileInfo(path).Length,
                MD5 = path.GetMd5()
            };
            _dbContext.Videos.Add(movieEntity);
            if (_dbContext.SaveChanges() > 0)
                throw new Exception("failed to save DB entry");
            string successpath = Path.Combine(Path.Combine(_fileStoreOptions.VideoPath, movieEntity.Id.ToString()), Path.GetFileName(path));
            File.Move(path, successpath);
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

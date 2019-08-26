using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamNetServer.Data;
using StreamNetServer.Models;
using StreamNetServer.Services;

namespace StreamNetServer.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaStreamApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MediaStreamFactory _mediaReaderFactory;

        public MediaStreamApiController(
            ApplicationDbContext dbContext,
            MediaStreamFactory mediaReaderFactory)
        {
            _mediaReaderFactory = mediaReaderFactory;
            _dbContext = dbContext;
        }
        [HttpGet("Video/{id}")]
        public async Task<FileStreamResult> GetVideoStream(Guid id)
        {
            var videoinfo = _dbContext.Videos.FirstOrDefault(v => v.Id == id);
            var mediaReader = _mediaReaderFactory.CreateVideoReadStream(videoinfo.Id,videoinfo.FileName);
            await mediaReader.ReadMedia();
            var mediaStream = mediaReader.MemoryStream;
            return new FileStreamResult(mediaReader.MemoryStream, videoinfo.MediaType);
        }
        [HttpGet("Music/{id}")]
        public async Task<FileStreamResult> GetAudioStream(Guid id)
        {
            var audioinfo = _dbContext.Music.FirstOrDefault(v => v.Id == id);
            var mediaReader = _mediaReaderFactory.CreateAudioReadStream(audioinfo.Id, audioinfo.FileName);
            await mediaReader.ReadMedia();
            var mediaStream = mediaReader.MemoryStream;
            return new FileStreamResult(mediaReader.MemoryStream, audioinfo.MediaType);
        }
        [HttpGet("Videos/{genre}")]
        public IActionResult GetVideosByGenre(string genre)
        {
            var videos = _dbContext.Videos.Where(v => v.Genre.Contains(genre));
            if (videos == null)
                return NotFound();
            var videoViewModels = Mapper.Map<IEnumerable<MediaReadViewModel>>(videos);
            return Ok(videoViewModels);
        }
        [HttpGet("Music/{artist}")]
        public IActionResult GetMusicByArtist(string artist)
        {
            var music = _dbContext.Music.Where(a => a.Artists.Contains(artist));
            if (music == null)
                return NotFound();
            var musicViewModel = Mapper.Map<IEnumerable<MediaReadViewModel>>(music);
            return Ok(musicViewModel);
        }
        [HttpGet("Music/{album}")]
        public IActionResult GetMusicByAlbum(string album)
        {
            var music = _dbContext.Music.Where(a => a.Album == album);
            if (music == null)
                return NotFound();
            var musicViewModel = Mapper.Map<IEnumerable<MediaReadViewModel>>(music);
            return Ok(musicViewModel);
        }
        [HttpGet("Music/{genre}")]
        public IActionResult GetMusicByGenre(string genre)
        {
            var music = _dbContext.Music.Where(a => a.Genre.Contains(genre));
            if (music == null)
                return NotFound();
            var musicViewModel = Mapper.Map<IEnumerable<MediaReadViewModel>>(music);
            return Ok(musicViewModel);
        }
    }
}
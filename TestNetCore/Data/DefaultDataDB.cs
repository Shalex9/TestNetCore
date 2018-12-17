using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data;
using TestNetCore.Data.TableData;

namespace TestNetCore.Data
{
    public class DefaultDataDB
    {
        private readonly ApplicationDbContext _dbContext;

        public DefaultDataDB(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public void HotelRooms()
        {
            if (!_dbContext.HotelInformations.Any())
            {
                var filePath = "Files/hotel_rooms.txt";
                var data = File.ReadAllLines(filePath);

                foreach (var line in data)
                {
                    HotelInformation room = new HotelInformation();

                    dynamic[] lineData = line.Split(new char[] { '-' });

                    room.NumberOfRoom = Int32.Parse(lineData[0]);
                    room.PriceForRoom = Int32.Parse(lineData[1]);
                    room.ComfortableOfRoom = lineData[2];
                    room.HasToilet = lineData[3] == "True";
                    room.HasTV = lineData[4] == "True";
                    room.HasBigBed = lineData[5] == "True";
                    room.IsFreeNow = lineData[6] == "True";

                    _dbContext.HotelInformations.Add(room);
                }
                _dbContext.SaveChanges();
            }
        }

        public void Dictionary()
        {
            if (!_dbContext.RussianDictionaries.Any())
            {
                var filePath = "Files/russian_nouns_with_definition.txt";
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    RussianDictionary w = new RussianDictionary();

                    string[] words = line.Split(new char[] { ':' });

                    w.Word = words[0];
                    w.Definition = line;

                    _dbContext.RussianDictionaries.Add(w);
                }
                _dbContext.SaveChanges();
            }
        }


        public void SeedForbidden()
        {
            if (!_dbContext.ForbiddenWords.Any())
            {
                var filePath = "Files/russian_bad_words.txt";
                var words = File.ReadAllLines(filePath);

                foreach (var word in words)
                {
                    ForbiddenWord w = new ForbiddenWord();
                    w.Word = word;

                    _dbContext.ForbiddenWords.Add(w);
                }
                _dbContext.SaveChanges();
            }
        }


        public void GalleryImg()
        {
            if (!_dbContext.GalleryFilesImgs.Any())
            {
                var filePath = "Files/gallery_img.txt";
                var data = File.ReadAllLines(filePath);

                foreach (var line in data)
                {
                    GalleryFileImg w = new GalleryFileImg();

                    string[] lineData = line.Split(new char[] { ' ' });

                    w.FileName = lineData[0];
                    w.Size = lineData[1];

                    _dbContext.GalleryFilesImgs.Add(w);
                }
                _dbContext.SaveChanges();
            }
        }

        public void GallerySound()
        {
            if (!_dbContext.GalleryFilesSounds.Any())
            {
                var filePath = "Files/gallery_sound.txt";
                var data = File.ReadAllLines(filePath);

                foreach (var line in data)
                {
                    GalleryFileSound w = new GalleryFileSound();

                    string[] lineData = line.Split(new char[] { ' ' });

                    w.FileName = lineData[0];
                    w.Size = lineData[1];

                    _dbContext.GalleryFilesSounds.Add(w);
                }
                _dbContext.SaveChanges();
            }
        }
    }
}

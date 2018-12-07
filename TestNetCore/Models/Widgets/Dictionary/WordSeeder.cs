using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Widgets.Dictionary
{
    public class WordSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public WordSeeder(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public void SeedDictionary()
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

    }
}

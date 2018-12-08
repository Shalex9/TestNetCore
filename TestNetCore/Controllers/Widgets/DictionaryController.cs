using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using TestNetCore.Data.TableData;
using TestNetCore.Controllers;
using TestNetCore.Models.Widgets.Dictionary;

namespace TestNetCore.Widget.Controllers
{
    public class DictionaryController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;

        public DictionaryController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Route("dictionary")]
        public IActionResult Dictionary()
        {
            var viewModel = new DictionaryViewModel();
            viewModel = ModelForDictionary(viewModel);

            return View("~/Views/Widgets/Dictionary.cshtml", viewModel);
        }

        // Основной метод для Get и Post
        public DictionaryViewModel ModelForDictionary(DictionaryViewModel viewModel)
        {
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;
            viewModel.CountWord = _dbContext.RussianDictionaries.Count();

            // проверяю или у юзера уже есть что-то в БД, чтоб не выбросило ошибку
            var isAssociationUser = _dbContext.SavedUserAssociations.FirstOrDefault(a => a.UserId == UserID);

            //var userAssociations;
            if (isAssociationUser != null)
            {
                SavedUserAssociation listAssociation = new SavedUserAssociation();
                var userAssociations = _dbContext.SavedUserAssociations.Where(a => a.UserId == UserID);

                foreach (SavedUserAssociation ass in userAssociations)
                {
                    viewModel.ListAssociation.Add(ass);
                }                
            }
           
            return viewModel;
        }

        [HttpPost]
        public IActionResult Dictionary(DictionaryViewModel viewModel)
        {
            // FOR READ FROM TXT FILE AND WRITE TO DATABASE
            //WordSeeder seed = new WordSeeder(_dbContext);
            //seed.SeedWord();
            //seed.SeedDictionary();
            //seed.SeedForbidden();

            var vm = ModelForDictionary(viewModel);
            return View("~/Views/Widgets/Dictionary.cshtml", vm);
        }

        // Получение искомого слова
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchWord(string word)
        {
            try
            {
                RussianDictionary w = _dbContext.RussianDictionaries.FirstOrDefault(a => a.Word == word);
                return Json(w.Definition);
            }
            catch (Exception)
            {
                return Json("Неккоректно введено слово, или по введенному слову данных нет.");
            }
        }

        // Получение ассоциации
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchAssotiation(string word, bool full, bool f3, bool f3len, bool f3cons, bool f3consLen)
        {
            try
            {
                AssociatedWords listAssociation = new AssociatedWords();

                int wLen = word.Length;

                // если слово 3 символа или короче
                if (wLen <= 3)
                {
                    // полное совпадение слова + еще символы
                    IQueryable<RussianDictionary> listCategory1 = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, word + "%"));
                    listAssociation.FullAndSymbols = listCategory1;

                }
                // если слово больше 3x символов
                else
                {
                    char lastSymbol = word[wLen - 1];
                    string wordСonsonants = word.Trim().ToLower().Replace("а", "")
                                                                .Replace("о", "")
                                                                .Replace("у", "")
                                                                .Replace("и", "")
                                                                .Replace("е", "")
                                                                .Replace("э", "")
                                                                .Replace("ю", "")
                                                                .Replace("я", "")
                                                                //.Replace("й", "")
                                                                .Replace("ы", "");
                    string f3consL = String.Concat(wordСonsonants[0], wordСonsonants[1], wordСonsonants[2]);
                    string f3l = word.Substring(0, 3);


                    IQueryable<RussianDictionary> list3letters = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, f3l + "%"));
                    IQueryable<RussianDictionary> list3lettersAndLastSymbol = list3letters.Where(a => EF.Functions.Like(a.Word, "%[" + lastSymbol + "]"));
                    IQueryable<RussianDictionary> list3ConsLetters = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, f3consL + "%"));

                    // полное совпадение слова + еще символы
                    IQueryable<RussianDictionary> listCategory1 = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, word + "%"));

                    // совпадение - первые 3 буквы И длинна такая же +/- 1 символ
                    IQueryable<RussianDictionary> listCategory2 = list3letters.Where(a => a.Word.Length == wLen || a.Word.Length == wLen - 1 || a.Word.Length == wLen + 1);

                    // совпадение - первые 3 буквы И длинна такая же +/- 1 символ И последний символ совпадает
                    IQueryable<RussianDictionary> listCategory3 = listCategory2.Where(a => EF.Functions.Like(a.Word, "%[" + lastSymbol + "]"));

                    // совпадение - первые 3 согласные буквы И длинна такая же +/- 1 символ
                    IQueryable<RussianDictionary> listCategory4 = list3ConsLetters.Where(a => a.Word.Length == wLen || a.Word.Length == wLen - 1 || a.Word.Length == wLen + 1);

                    // совпадение - первые 3 согласные буквы И длинна такая же +/- 1 символ И последний символ совпадает
                    IQueryable<RussianDictionary> listCategory5 = listCategory4.Where(a => EF.Functions.Like(a.Word, "%[" + lastSymbol + "]"));


                    if (full)
                    {

                        listAssociation.FullAndSymbols = listCategory1;
                    }

                    if (f3)
                    {
                        listAssociation.FirstThreeLetters = listCategory2;
                    }

                    if (f3len)
                    {
                        listAssociation.FirstThreeLettersLength = listCategory3;
                    }

                    if (f3cons)
                    {
                        listAssociation.FirstThreeConsLetters = listCategory4;
                    }

                    if (f3consLen)
                    {
                        listAssociation.FirstThreeConsLettersLength = listCategory5;
                    }

                    //listAssociation.Category6 = listCategory6;
                    //listAssociation.Category7 = listCategory7;
                }

                return Json(listAssociation);
            }
            catch (Exception)
            {
                return Json("Неккоректно введено слово, или по введенному слову данных нет.");
            }
        }


        // Сохранение слов-ассоциаций
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssociation(string word, string wordAssociation)
        {
            IQueryable<SavedUserAssociation> userAss = _dbContext.SavedUserAssociations.Where(a => a.UserId == UserID);
            SavedUserAssociation isWord = userAss.FirstOrDefault(a => a.Word == word);
            SavedUserAssociation newAss;

            if (isWord != null)
            {
                newAss = isWord;
                newAss.AllWordsAss = String.Concat(newAss.AllWordsAss, "; ", wordAssociation);
                _dbContext.SavedUserAssociations.Update(newAss);
            }
            else
            {
                newAss = new SavedUserAssociation();
                newAss.UserId = UserID;
                newAss.Word = word;
                newAss.AllWordsAss = wordAssociation;
                _dbContext.SavedUserAssociations.Add(newAss);
            }
            _dbContext.SaveChanges();

            IQueryable<SavedUserAssociation> newUserAss = _dbContext.SavedUserAssociations.Where(a => a.UserId == UserID);


            return Json(newUserAss);
        }

    }
}

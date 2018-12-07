using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestNetCore.Data;
using TestNetCore.Models.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using TestNetCore.Data.TableData;

namespace TestNetCore.Controllers
{
    public class EFController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;

        public EFController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
        }


        public async Task<IActionResult> EntityFSP(int? company, string name, int page = 1,
        SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<UserSort> users = _dbContext.UsersSort.Include(x => x.Company);

            if (company != null && company != 0)
            {
                users = users.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            EntityFSPViewModel viewModel = new EntityFSPViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortUserViewModel = new SortUserViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_dbContext.CompaniesSort.ToList(), company, name),
                Users = items,
                UserName = CurrentUserName,
                AvatarPath = CurrentAvatarPath
            };
            return View("EntityFSP", viewModel);
        }



        //public IActionResult Phones(int? companyId)
        //{
        //    var listPhones = _dbContext.Phones.ToList();
        //    var listCompanies = _dbContext.Companies.ToList();

        //    // формируем список компаний для передачи в представление
        //    List<CompanyModel> compModels = listCompanies
        //        .Select(c => new CompanyModel { Id = c.Id, Name = c.Name }).ToList();
        //    compModels.Insert(0, new CompanyModel { Id = 0, Name = "Все" });

        //    PhonesViewModel viewModel = new PhonesViewModel { Companies = compModels, Phones = listPhones };
        //    viewModel.UserName = CurrentUserName;
        //    viewModel.AvatarPath = CurrentAvatarPath;
        //    viewModel.ChoosenCompany = companyId;

        //    // если передан id компании, фильтруем список
        //    if (companyId != null && companyId > 0)
        //        viewModel.Phones = listPhones.Where(p => p.Manufacturer.Id == companyId);

        //    return View(viewModel);
        //}


        //public void AddToCash(Phone phone)
        //{
        //    cache.Set(phone.Id, phone, new MemoryCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        //    });
        //}

        //public async Task<Phone> GetFromCash(int id)
        //{
        //    //Phone phone = await productService.GetProduct(id);

        //    Phone phone = null;
        //    if (!cache.TryGetValue(id, out phone))
        //    {
        //        phone = await _dbContext.Phones.FirstOrDefaultAsync(p => p.Id == id);
        //        if (phone != null)
        //        {
        //            cache.Set(phone.Id, phone,
        //            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
        //        }
        //    }

        //    //if (phone != null)
        //    //    return Content($"Product: {phone.Name}");
        //    //return Content("Product not found");
        //    return phone;
        //}

    }
}

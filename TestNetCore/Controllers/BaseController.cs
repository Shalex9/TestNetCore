using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Models;
using Microsoft.AspNetCore.Identity;
using TestNetCore.Data;

namespace TestNetCore.Controllers
{
    public class BaseController : Controller
    {
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public IServiceProvider _provider { get; set; }
        public ApplicationDbContext _dbContext;

        public string CurrentUserName { get; set; }
        public string CurrentAvatarPath { get; set; }

        public BaseController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;

            var viewModel = new BaseViewModel();
            var claimsData = _dbContext.ClaimsDataUsers.FirstOrDefault(a => a.UserEmail == UserEmail);
            if (claimsData != null)
            {
                viewModel.UserName = claimsData.UserName;
                viewModel.AvatarPath = claimsData.UserAvatar;
            }
            CurrentUserName = viewModel.UserName;
            CurrentAvatarPath = viewModel.AvatarPath;
        }

        public string UserID
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                    return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return null;
            }
        }

        public string UserName
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                    return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return null;
            }
        }

        public string UserEmail
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name) != null)
                    return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                return null;
            }
        }
        //public string UserName => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //public string UserEmail => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
    }
}
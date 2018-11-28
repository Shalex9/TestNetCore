using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestNetCore.Data;
using TestNetCore.Models;
using System.Net;
using Newtonsoft.Json;

namespace TestNetCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [JsonObject(MemberSerialization.OptIn)]
    public class ExternalLoginModel : PageModel
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ExternalLoginModel> _logger;
        private ApplicationDbContext _dbContext;

        public ExternalLoginModel(
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<ExternalLoginModel> logger,
            ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var claimsListData = info.Principal.Claims.ToList();
            var userProviderKey = claimsListData[0].Value;
            var userName = claimsListData[1].Value;
            var userFirstName = claimsListData[2].Value;
            var userSecondName = claimsListData[3].Value;
            var userLinkGooglePlus = claimsListData[4].Value;
            var userEmail = claimsListData[5].Value;

            var userLinkPicasa = "http://picasaweb.google.com/data/entry/api/user/" + userEmail + "?alt=json";
            var myRequest = new WebClient().DownloadString(userLinkPicasa);
            dynamic jsonFromPicasa = JsonConvert.DeserializeObject(myRequest);
            var userAvatar = jsonFromPicasa.entry["gphoto$thumbnail"]["$t"];

            ClaimsDataUser userData;
            var currentUserData = _dbContext.ClaimsDataUsers.FirstOrDefault(a => a.UserEmail == userEmail);
            if (currentUserData == null) // для нового юзера, при первой загрузке 
            {
                userData = new ClaimsDataUser();
                userData.UserProviderKey = userProviderKey;
                userData.UserFirstName = userFirstName;
                userData.UserSecondName = userSecondName;
                userData.UserName = userName;
                userData.UserLinkGooglePlus = userLinkGooglePlus;
                userData.UserEmail = userEmail;
                userData.UserLinkPicasa = userLinkPicasa;
                userData.UserAvatar = userAvatar;

                _dbContext.ClaimsDataUsers.Add(userData);
            }
            else // если юзер уже есть - стянуть соответствующие данные с базы
            {
                userData = currentUserData;
                userData.UserName = userName; // на всякий случай перезаписываем имя, в случае, если имя юзера в гугле поменялось
                userData.UserFirstName = userFirstName;
                userData.UserSecondName = userSecondName;
                userData.UserAvatar = userAvatar;
                _dbContext.ClaimsDataUsers.Update(userData);
            }

            _dbContext.SaveChanges();

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}

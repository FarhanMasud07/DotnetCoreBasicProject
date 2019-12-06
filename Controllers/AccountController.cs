using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRD_High_School.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace PRD_High_School.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
           
            return RedirectToAction("Login", "Account");

        }
        /*[Authorize]*/
        public IActionResult Register()
        {
            return View();
        }

        [AcceptVerbs("Get","Post")]
        [HttpPost]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
          var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($" {email} is already in use try another Email");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registermodel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registermodel.Email, Email = registermodel.Email };
                var result = await userManager.CreateAsync(user, registermodel.Password);


                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationlink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token },Request.Scheme);
                    logger.Log(LogLevel.Warning, confirmationlink);

                    /*var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("PRD", "prdhigherschool@gmail.com"));

                    message.To.Add(new MailboxAddress(user.UserName, user.Email));

                    message.Subject = "Please verify your account";

                    message.Body = new TextPart("plain")
                    {
                        Text = confirmationlink,
                        
                    };

                    using (var client = new SmtpClient())
                    {

                        await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);

                        await client.AuthenticateAsync("prdhigherschool@gmail.com", "Sherlock1234@").ConfigureAwait(false);

                        client.Send(message);

                        client.Disconnect(true);
                    }*/

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListRoles", "Administration");
                    }
                    ViewBag.ErrorTitle = "Registration Successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your" + " " + "email, by clicking on the confirmation link we have emailed you see the regular inbox or search in spam";
                    return View("ErrorForRegistration");

                    /*await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");*/
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(registermodel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
                if(userId == null || token == null)
                {
                    return RedirectToAction("Login", "Account");
                }

            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email Can't be confirmed";
            return View("ErrorConfirm");
        }


        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public  IActionResult ExternalLogin(string provider, string returnUrl)
        { 
            var redirectUrl = Url.Action("ExternalLoginCallback","Account",new {ReturnUrl = returnUrl});

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null,string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("/Admin/ShowOldStudentAdmissionFees");

            LoginModel loginmodel = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if(remoteError != null)
            {
                ModelState.AddModelError(string.Empty,$"Error from external provier: {remoteError}");
                return View("Login", loginmodel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");
                return View("Login", loginmodel);
            }

             var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            
             IdentityUser user = null;

            if (email != null)
            {
                user = await userManager.FindByEmailAsync(email);

                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View("Login", loginmodel);
                }
            }

            var signinResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signinResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    //return Redirect(returnUrl);
                    /*
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        //return RedirectToAction("ShowOldStudentAdmissionFees", "Admin");
                        //return RedirectToAction("Login", "Account");
                        return Redirect(returnUrl);
                    }
                    else*/ if (signInManager.IsSignedIn(User) && User.IsInRole("Students"))
                    {
                        //return RedirectToAction("ShowOldStudentAdmissionFees", "Admin");
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return View("ErrorLogin");
                    }
                }

                //else 
            }
            else
            {
                if(email != null)
                {
                    if(user == null)
                    {
                        user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await userManager.CreateAsync(user);

                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationlink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                       
                        logger.Log(LogLevel.Warning, confirmationlink);

                        /*
                        var message = new MimeMessage();

                        message.From.Add(new MailboxAddress("PRD", "prdhigherschool@gmail.com"));

                        message.To.Add(new MailboxAddress(user.UserName, user.Email));

                        message.Subject = "Please verify your account";

                        message.Body = new TextPart("plain")
                        {
                            Text = confirmationlink,

                        };

                        using (var client = new SmtpClient())
                        {

                            await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);

                            await client.AuthenticateAsync("prdhigherschool@gmail.com", "Sherlock1234@").ConfigureAwait(false);

                            client.Send(message);
                            

                            client.Disconnect(true);
                        }
                        */
                        ViewBag.ErrorTitle = "Registration Successful";
                        ViewBag.ErrorMessage = "Before you can Login, please confirm your" + " " + "email, by clicking on the confirmation link we have emailed you see the regular inbox or search in spam";
                        return View("ErrorForRegistration");

                    }

                    await userManager.AddLoginAsync(user,info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                
                return View("Error");
            }

           return View("Login", loginmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginmodel,string returnUrl)
        {
            loginmodel.ExternalLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginmodel.Email);

                if(user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user,loginmodel.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(loginmodel);
                }
                var result = await signInManager.PasswordSignInAsync(loginmodel.Email, loginmodel.Password,loginmodel.RememberMe,false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ShowOldStudentAdmissionFees", "Admin");
                            //return RedirectToAction("Login", "Account");
                        }
                        else if (signInManager.IsSignedIn(User) && User.IsInRole("Students"))
                        {
                            return RedirectToAction("ShowOldStudentAdmissionFees", "Admin");
                        }
                        else
                        {

                            return View("Error");
                        }
                    }
                   
                }

                ModelState.AddModelError(string.Empty,"Invalid Login Attempt");
              
            }

            return View(loginmodel);
        }
    }
}
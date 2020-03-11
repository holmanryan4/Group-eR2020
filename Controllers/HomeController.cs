using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Authentication.Models;
using Activity = System.Diagnostics.Activity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using Authentication.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext options, UserManager<IdentityUser> userManager)
        {
            _context = options;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = _userManager.FindByIdAsync(user).Result;
            var userAcct = userId;
                       
            if (User.IsInRole("UserAccount") && userAcct == null)
            {
                return RedirectToAction("Create", "UserAccounts");
            }
            else if (userAcct != null)
            {

                return RedirectToAction("UserHomePage", "UserAccounts");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("Enter Your Email", "Enter Name");
                    var receiverEmail = new MailAddress("help.grouper@gmail.com", "Grouper Support");
                    var password = "Your Email Password here";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Unexpected Error";
            }
            return View();
        }
        public async Task<IActionResult> ContactAsync()
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("test@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("help.grouper@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

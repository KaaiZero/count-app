using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Count.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Count.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<User> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;


            // Once you add a real email sender, you should remove this code that lets you confirm the account

          
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                //var apiKey = Environment.GetEnvironmentVariable("CountAppKey");
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("kalinina.grace@gmail.com", "Exaple1");
                //var subject = "Confirm Your Email.";
                //var to = new EmailAddress(user.Email, "Exaple2");
                //var plainTextContent = "You just made a registration in CountApp. Please confirm you email address.";
                //var htmlContent = "<strong>You just made a registration in CountApp.</strong><p style='color:green;'> Please confirm you email address.</p>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();






        }
    }
}

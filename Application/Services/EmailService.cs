using Application.Interface;
using Application.Response;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public const string EmailUserSystem = "hoanghiep8401@gmail.com";
        public const string EmailPasswordSystem = "luyj hckb ksdx ybgv";
        public async Task<ApiResponse> SendValidationEmail(string recievedUser, string emailContent)
        {
            try
            {
                // Replace placeholders with actual values


                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("KOIDelivery", "KOIDelivery@gmail.com"));
                message.To.Add(new MailboxAddress("", recievedUser));
                message.Subject = $"Verification Email";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailContent; // Use the modified emailContent with the placeholders replaced
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync(EmailUserSystem, EmailPasswordSystem);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return new ApiResponse().SetOk("Mail Sent!");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest($"Something went wrong: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Configuration;
using BadgesSystem.Models;

namespace BadgesSystem.Web.Services
{
    public static class SmtpService
    {
        public static void Send(User recipient, UserBadge userBadgeGift, User currentUser, List<Tag> tagsParam)
        {
            var tags = new StringBuilder();

            foreach (var item in tagsParam)
            {
                tags.Append(item.Name + ", ");
            }

            tags.Remove(tags.ToString().LastIndexOf(","), 1);
            SmtpClient(recipient, userBadgeGift, currentUser, tags);
        }

        private static void SmtpClient(User recipient, UserBadge userBadgeGift, User currentUser, StringBuilder tags)
        {
            var isGift = userBadgeGift.BadgesGift.FlagType;
            var badgeOrGift = "Badge ";

            if(isGift)
            {
                badgeOrGift = "Gift ";
            }

            var emailLink = ConfigurationManager.AppSettings["EmailLinkUrl"];
            var htmlBody = new StringBuilder();

            htmlBody.Append("<html><body><div>Congratulations ")
                .Append(recipient.UserName)
                .Append(", you have earned a new ")
                .Append(badgeOrGift)
                .Append("<a href='")
                .Append(emailLink)
                .Append("'>")
                .Append(userBadgeGift.BadgesGift.Name)
                .Append("</a>")
                .Append("</div><br /><div><img src=\"cid:Pic1\"></div><br /><div>Tags: ")
                .Append(tags)
                .Append("</div><br /><div>")
                .Append(userBadgeGift.Reason)
                .Append("</div><br /><div>You get it ")
                .Append("from ")
                .Append(currentUser.Name)
                .Append("</div></body></html>");

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                 (htmlBody.ToString(), null, MediaTypeNames.Text.Html);

            var service = new ImageService();
            var ms = new MemoryStream(userBadgeGift.BadgesGift.File.FileContent);

            var pic1 = new LinkedResource(ms, MediaTypeNames.Image.Jpeg);
            pic1.ContentId = "Pic1";
            avHtml.LinkedResources.Add(pic1);

            var mail = new MailMessage()
            {
                From = new MailAddress("noreply@mail.com"),
                Subject = "You have earned a new " + badgeOrGift,
                IsBodyHtml = true,
            };

            mail.To.Add(recipient.Email);
            mail.AlternateViews.Add(avHtml);

            var host = ConfigurationManager.AppSettings["SmtpHost"];
            var port = Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var smtpCredentialsUserName = ConfigurationManager.AppSettings["SmtpCredentialsUserName"];
            var smtpCredentialsPassword = ConfigurationManager.AppSettings["SmtpCredentialsPassword"];
            var smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(smtpCredentialsUserName, smtpCredentialsPassword),
                EnableSsl = true
            };
            smtp.Send(mail);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using SoftPortalBot.Model.DataBaseContext;
using SoftPortalBot.Model.DataBaseTable;

namespace SoftPortalBot.Model.EmailServices
{
    public class EmailServices
    {
        private const string Sender = "";

        private const string Host = "smtp.gmail.com";

        private const int Port = 587;

        private const bool IsUseSsl = false;

        private const string AuthenticateUserName = "";

        private const string AuthenticatePassword = "";

        [Obsolete("Obsolete")]
        public static async void SendEmailCustom(Request request)
        {
            var context = new Context();
            var applicationName = 
                context.Applications.First(app => app.Id == request.ApplicationId).Name;
            var requestCreator =
                context.Users.First(user => user.Id == request.CreatorId);
            var groupId = 
                context.ApplicationResponsibleGroups.First(group => 
                    group.ApplicationId == request.ApplicationId).ResponsibleGroupId;
            var userIds = 
                (from item in context.UserResponsibleGroup 
                    where item.ResponsibleGroupId == groupId select item.UserId).ToList();
            var userLogins = 
                userIds.Select(id => context.Users.First(user => user.Id == id).Login).ToList();
            var reason =
                context.RequestReasons.First(item => item.Id == request.RequestReasonId);
            var installReasonId =
                context.RequestReasons.First(item => item.Name.Equals("Установка")).Id;
            var optionalMessage = "";
            if (installReasonId == reason.Id)
            {
                optionalMessage = $"Номер ПК: {request.ComputerNumber}";
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Sender));
            foreach (var email in userLogins)
            {
                message.To.Add(new MailboxAddress(email));
            }

            message.Subject = $"{applicationName}. Причина: {reason.Name}";
            message.Body = new BodyBuilder() 
                { HtmlBody = $"{request.ProblemDescription} <br> Создатель заявки: " +
                             $"{requestCreator.Surname} {requestCreator.Name} {requestCreator.Patronymic}" +
                             $"<br> {optionalMessage} <br>" +
                             $"<br> <i>{requestCreator.Login}</i>"}.ToMessageBody();
            using (var client = new SmtpClient())
            {
               await client.ConnectAsync(Host, Port, IsUseSsl);
               await client.AuthenticateAsync(AuthenticateUserName, AuthenticatePassword);
               await client.SendAsync(message);
               await client.DisconnectAsync(true);
            }

        }
    }
}

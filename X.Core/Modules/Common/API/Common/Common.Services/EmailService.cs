using Common.Model;
using Common.Model.Requests;
using Common.Services.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    partial class EmailService
    {
        public IOptions<EmailConfig> EmailConfig { get; set; }
        public override async Task BeforeInsertInternal(EmailInsertRequest request, Database.Email internalEntity)
        {
            internalEntity.From = EmailConfig.Value.From; //TODO: allow array?
            await base.BeforeInsertInternal(request, internalEntity);
        }
        public async Task<int> Send(int id)
        {
            var message = await GetByIdInternalAsync(id);

            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(message.To));

            // From
            mailMsg.From = new MailAddress(EmailConfig.Value.From, EmailConfig.Value.FromName);

            //// Subject and multipart/alternative Body
            mailMsg.Subject = message.Subject;
            string text = message.Content;
            string html = message.Content;
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = html;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            using (var smtpClient = new SmtpClient(EmailConfig.Value.Host, EmailConfig.Value.Port))
            {
                System.Net.NetworkCredential credentials = null;
                if (string.IsNullOrWhiteSpace(EmailConfig.Value.Username))
                {
                    credentials = new System.Net.NetworkCredential();
                }
                else
                {
                    credentials = new System.Net.NetworkCredential(EmailConfig.Value.Username, EmailConfig.Value.Password);
                    smtpClient.Credentials = credentials;
                }

                smtpClient.EnableSsl = EmailConfig.Value.Ssl;

                await smtpClient.SendMailAsync(mailMsg);
            }


            return id;
        }
    }
}

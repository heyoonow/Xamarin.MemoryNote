using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MemoryNote.ViewModels
{
    public partial class BaseViewModel
    {
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "메모공유"
            });
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = ""
            });
        }

        public async Task SendEmail(string to, string subject)
        {
            var message = new EmailMessage
            {
                Subject = subject,
                Body ="",
                To = new List<string>() { to },
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }
    }
}

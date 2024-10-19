using GamePlatform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage allMails);
    }
}

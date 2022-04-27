using System.Collections.Generic;
using System.Net.Mail;

namespace CleanArchitecture.Domain.Commons
{
    public class Email
    {
        public Email(string title, string subject, string message, List<string> recipientList)
        {
            Title = title;
            Subject = subject;
            Message = message;
            RecipientList = recipientList;
        }

        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<string> RecipientList { get; set; }
        public List<string> CopyList { get; set; } = new();
        public List<string> HiddenCopyList { get; set; } = new();
        public List<Attachment> Attachments { get; set; } = new();
    }
}

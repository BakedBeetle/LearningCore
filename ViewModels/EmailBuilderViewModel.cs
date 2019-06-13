using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.ViewModels
{
    public class EmailBuilderViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string From { get; set; }
        public string Body { get; set; }

        public List<string> ToRecipients { get; set; }
        public List<string> BccRecipients { get; set; }
        public List<string> CcRecipients { get; set; }

        public bool? hasAttachments { get; set; }

        public List<EmailAttachment> EmailAttachemnets { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? SentDateTime { get; set; }

        public string WebLink { get; set; }
        public string InternetMessageId { get; set; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; set; }
    }
}

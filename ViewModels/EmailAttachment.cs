using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMANA.ViewModels
{
    public class EmailAttachment
    {
        public string AttachmentID { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public int? Size { get; set; }
    }
}

#region Information
///-----------------------------------------------------------------
///   Namespace:      <Common>
///   Class:          <GraphController>
///   Description:    <Getting Email from O365 using Graph-API>
///   Author:         <Vasu Panchal>                    Date: <11-06-2019>
///   Notes:          <Static Token is used and No Authentication Method to office365 account is Implemented >
///-----------------------------------------------------------------
///
#endregion

using EMPMANA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using Attachment = System.Net.Mail.Attachment;

namespace EMPMANA.Controllers
{
    public class GraphController : Controller
    {
        public async Task<IActionResult> Index()
        {
        
            string token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYU3dTSEFKZzN0S3g0WGYzZjg1TDNhc2pnQ0o4c3ZxVXBQVi1PNWI0SVNVRHhpcmcycHhmMUJ2YjBCbzQ1QjE1S3czYTBUM0V1bnBhZXJma3hOY0xTX0NBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIiwia2lkIjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85ZjFhMGZlMi1iMTY4LTQyMjgtYjM0Yi04NWRjNjQ2ZmE3NTMvIiwiaWF0IjoxNTYwMzQyMDkwLCJuYmYiOjE1NjAzNDIwOTAsImV4cCI6MTU2MDM0NTk5MCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFTUUEyLzhMQUFBQTBpUGt1NTdYUGo3UjNuUFZiZFh4a0ZOcUJQdkd4K1lGWU83WC9iakU5RFk9IiwiYW1yIjpbInB3ZCJdLCJhcHBfZGlzcGxheW5hbWUiOiJFbWFpbExvZ2dpbmctQWRkLUluIiwiYXBwaWQiOiJhNjQ0MmE1NC1jZTVhLTQwOWEtOWI4NS03Y2EyMmU2NDhjYmMiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IlBhbmNoYWwiLCJnaXZlbl9uYW1lIjoiVmFzdSIsImlwYWRkciI6IjE4MC4yMTEuMTAzLjE4NSIsIm5hbWUiOiJWYXN1IFBhbmNoYWwiLCJvaWQiOiJiN2NlMzhmNy1hNGRlLTRhNzgtYTdhNS0wNWJhZmQ2ZDcyMjUiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDA0MkE2NTE3OSIsInNjcCI6IkZpbGVzLlJlYWRXcml0ZS5BbGwgTWFpbC5SZWFkIG9wZW5pZCBwcm9maWxlIFNpdGVzLlJlYWQuQWxsIFVzZXIuUmVhZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJpbmtub3dubnR3ayJdLCJzdWIiOiJ2Z0pKaU1oZFdOMjJyQjA4QXBON0pkVXlxRk5wT3NUdGRjQWtDUmpfM2E0IiwidGlkIjoiOWYxYTBmZTItYjE2OC00MjI4LWIzNGItODVkYzY0NmZhNzUzIiwidW5pcXVlX25hbWUiOiJ2YXN1QHByYWthc2hpbmZvdGVjaC5jb20iLCJ1cG4iOiJ2YXN1QHByYWthc2hpbmZvdGVjaC5jb20iLCJ1dGkiOiJUc0dXdlhtd18wS0hRNjl0YmIwbUFBIiwidmVyIjoiMS4wIiwieG1zX3N0Ijp7InN1YiI6Ilh6aHNzVHBZbmNObUZjbkxoQ1hrZHMwSERoeUNFXzJNV0NObVl0aktfTVkifSwieG1zX3RjZHQiOjEzMzgzNjAyNTR9.NdaOhi4jVmg0IwmmczbmkOQriEdm3BxPShvc9WsLCAcNDg1TCgc2RuycBboHLlRS36zft9f3LBpyfYjEdmb-Gwb6Ndce-hsDqyN6h4WuRNZEIN3QZ37I2qsiTvPp8-mRBhlDWyKgUNvaJ98zFTPo1WhtivcevBhl6yPVh9c2RNcs91UTq-3K7t8Ea54_6XmKj65zRJ0PLGUrbt_4u1cDtzl8uD9eD5KAyZr6adINoZkr_kj--95VZR_XlPqGFKy24dNm0LSUYR2MmflsJ5f2cORKZh3FAi7E4s9br-oWWTscQjnYH_fc4luAF8zpV8u7SlPAvDYRv4fsm7F6drks8A";

            if (string.IsNullOrEmpty(token))
            {
                // If there's no token in the session, redirect to Home
                return Redirect("/");
            }
            try
            {
                //Creating Request Graph Client with Authorization Token
                GraphServiceClient client = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    (requestMessage) =>
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        return Task.FromResult(0);
                    }));

                //Getting Top (2) Emails with Attachments from SentItems
                IMailFolderMessagesCollectionPage mailResults = await client.Me.MailFolders.SentItems.Messages.Request().Expand("attachments").Top(2).GetAsync();


               List<EmailBuilderViewModel> EviewModel = FillEmailModel(mailResults);

                GraphModel md = new GraphModel();
                md.EmailItems = EviewModel;

                GenerateEmail(md.EmailItems);

                return View(md);
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "ERROR retrieving messages", debug = ex.Message });
            }
        }

        /// <summary>
        /// Binds Email Message to EmailBuilderViewModel Model
        /// </summary>
        /// <param name="mailResults">IMailFolderMessagesCollectionPage response from Graph API Request</param>
        /// <returns>List of EmailBuilderViewModel</returns>
        private List<EmailBuilderViewModel> FillEmailModel(IMailFolderMessagesCollectionPage mailResults)
        {
            List<EmailBuilderViewModel> EviewModel = new List<EmailBuilderViewModel>();
            foreach (Message msg in mailResults)
            {
                EviewModel.Add(new EmailBuilderViewModel
                {
                    Id = msg.Id,
                    Subject = msg.Subject ?? string.Empty,
                    Sender = msg.Sender.EmailAddress.Address.ToString() ?? string.Empty,
                    From = msg.From.EmailAddress.Address.ToString() ?? string.Empty,
                    Body = msg.Body.Content.ToString() ?? string.Empty,

                    EmailAttachemnets = msg.Attachments.Select(x => new EmailAttachment
                    {
                        AttachmentID = msg.Attachments.CurrentPage.Select(z => z.Id).FirstOrDefault(),
                        Name = msg.Attachments.CurrentPage.Select(z => z.Name).FirstOrDefault(),
                        ContentType = msg.Attachments.CurrentPage.Select(z => z.ContentType).FirstOrDefault(),
                        Size = msg.Attachments.CurrentPage.Select(z => z.Size).FirstOrDefault(),
                        File = msg.Attachments.CurrentPage.Cast<FileAttachment>().Select(z => z.ContentBytes).FirstOrDefault(),

                    }).ToList(),

                    ToRecipients = msg.ToRecipients.Select(x => x.EmailAddress.Address).ToList<string>(),
                    BccRecipients = msg.BccRecipients.Select(x => x.EmailAddress.Address).ToList<string>(),
                    CcRecipients = msg.CcRecipients.Select(x => x.EmailAddress.Address).ToList<string>(),

                    hasAttachments = msg.HasAttachments,
                    CreatedDateTime = msg.CreatedDateTime,
                    SentDateTime = msg.SentDateTime,
                    ConversationId = msg.ConversationId,
                    InternetMessageId = msg.InternetMessageId,
                    ParentFolderId = msg.ParentFolderId,
                    WebLink = msg.WebLink,
                });
            }
            return EviewModel;
        }

        /// <summary>
        /// Parallel Task to create new MailMessages and saves 
        /// </summary>
        /// <param name="md">List of EmailBuilderViewModel</param>
        /// <returns>void</returns>
        private void GenerateEmail(List<EmailBuilderViewModel> md)
        {
            Parallel.ForEach(md, (m) =>
            {
                MailMessage mg = new MailMessage();
                mg.IsBodyHtml = true;
                mg.From = new MailAddress(m.From);
                mg.Sender = new MailAddress(m.Sender);

                mg.Subject = m.Subject;
                mg.Body = m.Body;

                foreach (string a in m.ToRecipients)
                {
                    mg.To.Add(a);
                }
                foreach (string a in m.CcRecipients)
                {
                    mg.CC.Add(a);
                }
                foreach (string a in m.BccRecipients)
                {
                    mg.Bcc.Add(a);
                }
                if (m.hasAttachments == true)
                {
                    foreach (EmailAttachment a in m.EmailAttachemnets)
                    {
                      
                        //creating attachment of byte Array
                        mg.Attachments.Add(new Attachment(new MemoryStream(a.File), a.Name));

                    }
                }
                //Saving at a location
                SmtpClient client = new SmtpClient("mysmtphost");
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\";
                client.Send(mg);
            });
        }
    }
}
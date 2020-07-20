using ConsoleApp1.Mailers;
using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Startup
    {
        private readonly IRegistrationMailer mailer;

        public Startup(IRegistrationMailer mailer)
        {
            this.mailer = mailer;
        }

        public void Run()
        {

            var toAddresses = new List<MailboxAddress>();
            toAddresses.Add(new MailboxAddress("King Wilder", "king@gizmobeach.com"));

            var from = new List<MailboxAddress>();
            from.Add(new MailboxAddress("King W", "info@kingwilder.com"));

            var path = @"C:\Users\king\OneDrive\Pictures\Golf Courses\WP_20170429_001.jpg";
            var mimeType = MimeTypes.GetMimeType(path);
            var contentType = ContentType.Parse(mimeType);
            var imageAttachment = new MimePart(contentType)
            {
                Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            var pdfPath = @"C:\Users\king\OneDrive\Documents\tfsstationerydemo.azurewebsites.net_bizcard.pdf";
            mimeType = MimeTypes.GetMimeType(pdfPath);
            contentType = ContentType.Parse(mimeType);
            var pdfAttachment = new MimePart(contentType)
            {
                Content = new MimeContent(File.OpenRead(pdfPath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(pdfPath)
            };

            List<MimeEntity> attachments = null;

            //attachments = new List<MimeEntity>();
            //attachments.Add(imageAttachment);
            //attachments.Add(pdfAttachment);

            var context = new RegistrationMailerContext
            {
                Now = DateTime.Now.ToString(),
                FirstName = "Kinger",
                LastName = "Wilderness",
                AdminMessage = "This email should have an image in the header.",
                Subject = "Test from TechWiz",
                ToAddresses = toAddresses,
                From = from
            };

            if (attachments != null)
            {
                context.AttachmentCollection = attachments;
            }

            try
            {
                mailer.Customer(context).SendAsync().Wait();
                Console.WriteLine("Sent email successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: {0}", ex.Message);
            }
            Console.ReadLine();
        }
    }
}

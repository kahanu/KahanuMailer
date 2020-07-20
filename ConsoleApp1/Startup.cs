using ConsoleApp1.Mailers;
using KahanuMailer;
using MimeKit;
using Models;
using System;
using System.Collections.Generic;

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
            toAddresses.Add(new MailboxAddress("Recipient Name", "king@gizmobeach.com"));

            var from = new List<MailboxAddress>();
            from.Add(new MailboxAddress("From Name", "email@from.com"));


            // Begin Attachments code
            //var path = @"C:\Users\king\Pictures\Golf Courses\golfcourse.jpg";
            //var mimeType = MimeTypes.GetMimeType(path);
            //var contentType = ContentType.Parse(mimeType);
            //var imageAttachment = new MimePart(contentType)
            //{
            //    Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName(path)
            //};

            //var pdfPath = @"C:\Users\king\Documents\some.pdf";
            //mimeType = MimeTypes.GetMimeType(pdfPath);
            //contentType = ContentType.Parse(mimeType);
            //var pdfAttachment = new MimePart(contentType)
            //{
            //    Content = new MimeContent(File.OpenRead(pdfPath), ContentEncoding.Default),
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName(pdfPath)
            //};

            List<MimeEntity> attachments = null;

            //attachments = new List<MimeEntity>();
            //attachments.Add(imageAttachment);
            //attachments.Add(pdfAttachment);

            List<LinkedResource> linkedResources = null;
            linkedResources = new List<LinkedResource>();
            linkedResources.Add(new LinkedResource { Name = "logoCid", Cid="techwizLogo", Path = @"C:\Users\king\source\repos\KahanuMailer\ConsoleApp1\Views\RegistrationMailer\techwiz-logo.png" });


            var context = new RegistrationMailerContext
            {
                Now = DateTime.Now.ToString(),
                FirstName = "John",
                LastName = "Doh",
                CustomerMessage = @"<p>Thank you for being a loyal customer.</p>
<p>Your next visit will get 20% off your purchase.</p>",
                Subject = "Test from TechWiz",
                ToAddresses = toAddresses,
                From = from
            };

            if (linkedResources != null)
            {
                context.LinkedResources = linkedResources;
            }

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

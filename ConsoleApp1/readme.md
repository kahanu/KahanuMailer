# Working Example
This console project is a working example of how to use KahanuMailer.  Since it is based on MvcMailer, it has much of the same functionality and usage as MvcMailer, but it is not as feature rich as MvcMailer at the current version.

## Snippets
There are Visual Studio Snippets you can install to scaffold out the mailer and the interface, but you still have to manually create the Html Layout and partials yourself.

## Usage
The `Startup.cs` class represents what you might use in your C# controllers to send emails.  You'll notice that most of the code is creating the object (RegistrationMailerContext) that will contain the properties with values that are used to populate the Mailer and the Html templates.

## Example Code 
This is an example on how to create the mailer and populate the context that contains the data for the email.

```csharp
            // Set the 'To' addresses
            var toAddresses = new List<MailboxAddress>();
            toAddresses.Add(new MailboxAddress("Recipient Name", "king@gizmobeach.com"));

            // Set the 'From' addresses
            var from = new List<MailboxAddress>();
            from.Add(new MailboxAddress("From Name", "email@from.com"));


            // Begin Attachments code
            var path = @"C:\Users\king\Pictures\Golf Courses\golfcourse.jpg";
            var mimeType = MimeTypes.GetMimeType(path);
            var contentType = ContentType.Parse(mimeType);
            var imageAttachment = new MimePart(contentType)
            {
                Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            var pdfPath = @"C:\Users\king\Documents\some.pdf";
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

            attachments = new List<MimeEntity>();
            attachments.Add(imageAttachment);
            attachments.Add(pdfAttachment);

            // Add the linked resources which contain the logo and the image in the email content.
            List<LinkedResource> linkedResources = null;
            linkedResources = new List<LinkedResource>();
            linkedResources.Add(new LinkedResource { Name = "logoCid", Cid="techwizLogo", Path = @"C:\Users\king\source\repos\KahanuMailer\ConsoleApp1\Views\RegistrationMailer\techwiz-logo.png" });
            linkedResources.Add(new LinkedResource { Name = "parakeetCid", Cid = "parakeet", Path = @"C:\Users\king\source\repos\KahanuMailer\ConsoleApp1\Views\RegistrationMailer\parakeet.png" });

            // Create the context object that provides the data for the email template.
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
                await mailer.Customer(context).SendAsync().ConfigureAwait(false);
                Console.WriteLine("Sent email successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: {0}", ex.Message);
            }
    ```
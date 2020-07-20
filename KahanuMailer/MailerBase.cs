using HandlebarsDotNet;
using MimeKit;
using System;
using System.IO;

namespace KahanuMailer
{
    public class MailerBase<T> where T : BaseContext
    {
        #region ctors

        public MailerBase(ISmtpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region Properties
        private EmailMessage _message;
        private readonly ISmtpConfiguration configuration;

        public virtual string MailerName { get { return GetType().Name; } }

        /// <summary>
        /// When the LayoutName is not empty, it will use that name (_Layout.html) as the layout for the HTML email message.
        /// The .html extension is optional.
        /// </summary>
        public string LayoutName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Populate the message object with the context property values for the email message.
        /// </summary>
        /// <param name="action">MailMessage action</param>
        /// <returns>MailMessage</returns>
        public EmailMessage Populate(Action<EmailMessage> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _message = new EmailMessage(configuration);

            action(_message);

            return _message;
        }

        /// <summary>
        /// Builds the HTML body, and adds any attachments.
        /// </summary>
        /// <param name="context">The context that contains the values for the email message.</param>
        /// <returns>MimeEntity</returns>

        public MimeEntity BuildBody(T context)
        {
            string result;

            var builder = new BodyBuilder();

            if (LayoutExists())
            {
                result = ComposeBodyAndLayout(context, builder);
            }
            else
            {
                result = NoLayout(context, builder);
            }

            if (context.AttachmentCollection.Count > 0)
            {
                foreach (var item in context.AttachmentCollection)
                {
                    builder.Attachments.Add(item);
                }
            }

            builder.HtmlBody = result;

            return builder.ToMessageBody();
        }

        #endregion

        #region Private Methods

        private bool LayoutExists()
        {
            string layoutName = this.LayoutName;
            var hasExtension = this.LayoutName.IndexOf(".html") > -1;
            if (!hasExtension)
            {
                layoutName = this.LayoutName + ".html";
            }

            var layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\" + this.MailerName + @"\" + layoutName);
            return File.Exists(layoutPath);
        }

        private string ComposeBodyAndLayout(T context, BodyBuilder builder)
        {
            foreach (var link in context.LinkedResources)
            {
                var res = builder.LinkedResources.Add(link.Path);
                res.ContentId = link.Cid;
                Handlebars.RegisterTemplate(link.Name, link.Cid);
            }

            // Read partial template for the body and register it
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\" + this.MailerName + @"\" + _message.ViewName.ToLower() + ".html");
            var source = File.ReadAllText(templatePath);
            Handlebars.RegisterTemplate("body", source);

            // Read layout template and compose partial template
            string layoutName = this.LayoutName;
            var hasExtension = this.LayoutName.IndexOf(".html") > -1;
            if (!hasExtension)
            {
                layoutName = this.LayoutName + ".html";
            }
            var layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\" + this.MailerName + @"\" + layoutName);
            var layoutSource = File.ReadAllText(layoutPath);
            var layoutTemplate = Handlebars.Compile(layoutSource);

            return layoutTemplate(context);
        }

        private string NoLayout(T context, BodyBuilder builder)
        {
            foreach (var link in context.LinkedResources)
            {
                var res = builder.LinkedResources.Add(link.Path);
                var cid = res.ContentId;
                link.Cid = cid;
                Handlebars.RegisterTemplate(link.Name, link.Cid);
            }

            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\" + this.MailerName + @"\" + _message.ViewName.ToLower() + ".html");
            var source = File.ReadAllText(templatePath);
            var template = Handlebars.Compile(source);
            var result = template(context);

            return result;
        }

        #endregion
    }
}

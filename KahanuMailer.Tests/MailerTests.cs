using FluentAssertions;
using HandlebarsDotNet;
using KahanuMailer.Tests.Mailers;
using KahanuMailer.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;
using Models;
using System;
using System.Collections.Generic;

namespace KahanuMailer.Tests
{
    [TestClass]
    public class MailerTests
    {
        private ISmtpConfiguration smtpConfiguration;

        public MailerTests()
        {
            smtpConfiguration = new MockSmtpConfiguration();
        }

        [TestMethod]
        public void linked_resources_included_in_html_body()
        {
            // Arrange
            var mailer = new RegistrationMailer(smtpConfiguration);
            var addresses = new List<MailboxAddress>();
            addresses.Add(new MailboxAddress("Kinger", "king@email.com"));
            var guid = "18c1c20f-81f0-4308-8fd8-f3c65a276882";

            List<LinkedResource> linkedResources = null;
            linkedResources = new List<LinkedResource>();
            linkedResources.Add(new LinkedResource { Name = "logoCid", Cid = "techwizLogo", Path = @"C:\Users\king\source\repos\KahanuMailer\ConsoleApp1\Views\RegistrationMailer\techwiz-logo.png" });
            linkedResources.Add(new LinkedResource { Name = "parakeetCid", Cid = "parakeet", Path = @"C:\Users\king\source\repos\KahanuMailer\ConsoleApp1\Views\RegistrationMailer\parakeet.png" });

            var ctx = new RegistrationMailerContext
            {
                Now = "7/13/2020 4:05:49 PM",
                FirstName = "Mike",
                LastName = "Jones",
                CustomerMessage = "This is the customer message.",
                Subject = "This is the customer subject",
                ToAddresses = addresses,
                Guid = guid
            };

            if (linkedResources != null)
            {
                ctx.LinkedResources = linkedResources;
            }

            // Act
            var actual = mailer.Customer(ctx);
            var expectedBody = @"<!DOCTYPE html>"
 + Environment.NewLine + "<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">"
 + Environment.NewLine + "<head>"
 + Environment.NewLine + "    <meta charset=\"utf-8\" />"
 + Environment.NewLine + "    <title></title>"
 + Environment.NewLine + "    <style>"
 + Environment.NewLine + "        .header {"
 + Environment.NewLine + "            padding: 10px;"
 + Environment.NewLine + "            background-color: #f7d736;"
 + Environment.NewLine + "            margin-bottom: 20px;"
 + Environment.NewLine + "            width: 100%;"
 + Environment.NewLine + "        }"
 + Environment.NewLine + ""
 + Environment.NewLine + "        .footer {"
 + Environment.NewLine + "            padding: 10px;"
 + Environment.NewLine + "            background-color: #f7f7f7;"
 + Environment.NewLine + "            margin-top: 20px;"
 + Environment.NewLine + "            width: 100%;"
 + Environment.NewLine + "        }"
 + Environment.NewLine + "    </style>"
 + Environment.NewLine + "</head>"
 + Environment.NewLine + "<body>"
 + Environment.NewLine + "    <div class=\"header\">"
 + Environment.NewLine + "        <img src=\"cid:techwizLogo\" />"
 + Environment.NewLine + "    </div>"
 + Environment.NewLine + "<p>7/13/2020 4:05:49 PM</p>"
 + Environment.NewLine + "<p>Mike Jones,</p>"
 + Environment.NewLine + "This is the customer message."
 + Environment.NewLine + "<p>My bird: <img src='cid:parakeet' /></p>"
 + Environment.NewLine + "<p>Link: <a href=\"somepage/18c1c20f-81f0-4308-8fd8-f3c65a276882\">Link with Guid</a></p>"
 + Environment.NewLine + "<p>Thanks, TechWiz Support.</p>"
 + Environment.NewLine + "    <div class=\"footer\">"
 + Environment.NewLine + "        &copy;2020 Tech - All rights reserved."
 + Environment.NewLine + "    </div>"
 + Environment.NewLine + "</body>"
 + Environment.NewLine + "</html>";

            // assert
            actual.HtmlBody.Should().Be(expectedBody);
            
        }
    }
}

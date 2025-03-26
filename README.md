# KahanuMailer
An Html Mailer component for .Net 8 inspired by MvcMailer.

## NuGet
This feature will be coming soon.

## Overview
The KahanuMailer is inspired by MvcMailer, but it has no dependency on ASP.NET MVC.  It can be used directly in a .Net 8 application.

Like MvcMailer it is designed to be used with HTML view templates in a Views folder in the file system.  Mailer C# classes are created that encapsulate the logic for populating the templates with your own data.

KahanuMailer uses `Handlebars.Net` to merge your data with the HTML templates.

Unfortunately KahanuMailer doesn't contain a PowerShell script to generate the views and classes, but there are Visual Studio snippets that make that task easier.

## C# Interface and class
Interfaces and classes directly mimic the MvcMailer interfaces and classes.

```csharp
    public interface IRegistrationMailer
    {
        EmailMessage Customer(RegistrationMailerContext context);
    }
```

The class contains the logic to populate the HTML templates.

```csharp
    public class RegistrationMailer : MailerBase<RegistrationMailerContext>, IRegistrationMailer
    {
        #region ctors

        public RegistrationMailer(ISmtpConfiguration configuration) : base(configuration)
        {
            LayoutName = "_Layout";
        }

        #endregion

        #region Public Methods

        public EmailMessage Customer(RegistrationMailerContext context)
        {
            return Populate(m =>
            {
                m.ViewName = "customer";
                m.Subject = context.Subject;
                m.To.AddRange(context.ToAddresses);
                m.Body = BuildBody(context);
                m.From.AddRange(context.From);
            });
        }

        #endregion
    }
```

## Views
For the HTML views, the `_Layout.html` is just an HTML layout view with `Handlebars.Net` placeholders.

```html
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
    <style>
        .header {
            padding: 10px;
            background-color: #f7d736;
            margin-bottom: 20px;
            width: 100%;
        }

        .footer {
            padding: 10px;
            background-color: #f7f7f7;
            margin-top: 20px;
            width: 100%;
        }
    </style>
</head>
<body>
    <div class="header">
        <img src="cid:{{ > logoCid }}" />
    </div>
    {{ > body }}
    <div class="footer">
        &copy;2020 Tech - All rights reserved.
    </div>
</body>
</html>
```

If you don't know about `Handlebars.Net` syntax, it will help to do some research in order to better understand how it works.

** Quick Handlebars Explanation **
- {{ FirstName }} - the double curly braces are the basic placeholder tokens.  In your content, the `FirstName` property will replace the `{{ FirstName }}` Handlebars placeholder.
- {{ > parakeetCid }} - this placeholder contains a greater-than character (>) before the token name.  This means that it's a template partial, mostly used for optional elements like linkedresources, etc.
- {{{ CustomerMessage }}} - the triple curly braces denotes `raw html`.  If your content to be inserted into the HTML template contains HTML tags, in order to render it as HTML and not print it to the screen as HTML, you need to use the triple curly braces.

The content HTML part also contains `Handlebars.Net` placeholders. 

```html
<p>{{ Now }}</p>
<p>{{ FirstName }} {{ LastName }},</p>
{{{ CustomerMessage }}}
<p>My bird: <img src='cid:{{ > parakeetCid }}' /></p>
<p>Thanks, TechWiz Support.</p>
```

## Setup and Configuration
To setup and configure KahanuMailer, you need to do some things depending on how you want to provide the Smtp configuration settings for the mailer.  You have two options at the moment, `appSettings` and `database`.

### AppSettings Configuration
This is the easiest configuration to setup.  You need to create an `SmtpConfiguration` section in the `appSettings` file.

```javascript
  "SmtpConfiguration": {
    "Server": "secure.host.com",
    "Port": 587,
    "Username": "sample@host.com",
    "Password": "password",
    "UseAuthentication": true
  }
```

If you won't be using authentication for the mail server connection, set the `UseAuthentication` to false, then you don't need to set the `UserName` and `Password` properties.

### Database Configuration
At the moment, KahanuMailer is only compatible with SQL Server.  To get the Smtp configuration settings from the database, you need to create a table called anything you want, but with the same properties names as the `appSettings` configuration section.

## Startup File Configuration
In the `Startup` file, you just need to set the KahanuMailer in the `RegisterServices` method.

```csharp
        static void RegisterServices(IConfiguration config)
        {
            var services = new ServiceCollection();
            services.AddScoped<IRegistrationMailer, RegistrationMailer>();
            services.AddScoped<Startup>();

            services.AddKahanuMailer(config, options =>
            {
                options.UseConfig(db =>
                {
                    db.ConnectionStringName = "SampleConnection";
                    db.SmtpConfigTableName = "SmtpConfiguration";
                });
            });

        }
```
The `AddKahanuMailer` ServiceCollection extension method has a couple overloads that allow for different configurations, either by `appSettings` or `database`.

If you are just going to use the `appSettings`, you just need to add the method alone.

```csharp
services.AddKahanuMailer();
```

This will look for the`SmtpConfiguration` section of the `appSettings` configuration, and use those settings.

If you will be using the database configuration, then you need to set some properties.

```csharp
            services.AddKahanuMailer(config, options =>
            {
                options.UseConfig(db =>
                {
                    db.ConnectionStringName = "SampleConnection";
                    db.SmtpConfigTableName = "SmtpConfiguration";
                });
            });
```

You just need to set the connection string name in the `appSettings`, and set the SQL Server table for the configuration.  The connection string will look something like this:

```javascript
  "ConnectionStrings": {
    "SampleConnection": "Data Source=localhost;Initial Catalog=MailHub;Integrated Security=True;MultipleActiveResultSets=true;"
  }
```

That's all that is needed to setup and configure KahanuMailer.

I hope you enjoy using it.

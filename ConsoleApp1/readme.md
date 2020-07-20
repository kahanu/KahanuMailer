# Working Example
This console project is a working example of how to use KahanuMailer.  Since it is based on MvcMailer, it has much of the same functionality and usage as MvcMailer, but it is not as feature rich as MvcMailer at the current version.

## Snippets
There are Visual Studio Snippets you can install to scaffold out the mailer and the interface, but you still have to manually create the Html Layout and partials yourself.

## Usage
The `Startup.cs` class represents what you might use in your C# controllers to send emails.  You'll notice that most of the code is creating the object (RegistrationMailerContext) that will contain the properties with values that are used to populate the Mailer and the Html templates.


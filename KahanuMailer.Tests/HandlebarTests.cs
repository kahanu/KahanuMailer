using FluentAssertions;
using HandlebarsDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KahanuMailer.Tests
{
    [TestClass]
    public class HandlebarTests
    {
        [TestMethod]
        public void registering_partials()
        {
            string source =
                @"<img src='cid:{{ > logoCid }}' />
<h2>Names</h2>
                {{#names}}
                  {{> user}}
                {{/names}}";

            string partialSource =
            @"<strong>{{name}}</strong>";

            Handlebars.RegisterTemplate("user", partialSource);
            Handlebars.RegisterTemplate("logoCid", "123355");

            var template = Handlebars.Compile(source);

            var data = new
            {
                names = new[] {
                    new {
                        name = "Karen"
                    },
                    new {
                        name = "Jon"
                    }
                }
            };

            var result = template(data);
            var expected = @"<img src='cid:123355' />
<h2>Names</h2>
<strong>Karen</strong><strong>Jon</strong>";

            result.Should().Be(expected);
        }

    }
}

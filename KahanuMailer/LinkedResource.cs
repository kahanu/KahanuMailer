namespace KahanuMailer
{
    /// <summary>
    /// The object to create a LinkedResource for the email.
    /// </summary>
    public class LinkedResource
    {
        /// <summary>
        /// The name of the Handlebars template reference in the template. Example: &lt;img src="cid:{{ > logoCid }}" /&gt;
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The fully qualified path to the resource.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The identifier for the linked resource.  You can generate this value.  Example: myCompanyLogo
        /// <example>
        /// <code>
        /// Before compiling with the Handlebars template
        /// &lt;img src="cid:{{ > logoCid }}" /&gt;
        /// 
        /// After compiling
        /// &lt;img src="cid:myCompanyLogo" /&gt;
        /// </code>
        /// </example>
        /// </summary>
        public string Cid { get; set; } 
    }
}

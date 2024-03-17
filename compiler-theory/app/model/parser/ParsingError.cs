namespace compiler_theory.app.model.parser
{
    /// <summary>
    /// Represents a parsing error in the source code.
    /// </summary>
    public class ParsingError
    {
        /// <summary>
        /// Gets or sets the number of the error.
        /// </summary>
        public int NumberOfError { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Gets or sets the token that caused the error.
        /// </summary>
        public string ErrorToken { get; set; } = "";

        /// <summary>
        /// Gets or sets the token that was expected.
        /// </summary>
        public string NeedToken { get; set; } = "";

        /// <summary>
        /// Gets or sets the start index of the error in the source code.
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the end index of the error in the source code.
        /// </summary>
        public int EndIndex { get; set; }
    }
}
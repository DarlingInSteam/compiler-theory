namespace compiler_theory.app.model;

    /// <summary>
    /// Represents a token in the source code.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets or sets the code of the token.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the type of the lexeme.
        /// </summary>
        public LexemeType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the token.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the start index of the token in the source code.
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the end index of the token in the source code.
        /// </summary>
        public int EndIndex { get; set; }
    }

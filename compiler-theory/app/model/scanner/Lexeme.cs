namespace compiler_theory.app.model;

    /// <summary>
    /// Represents a lexeme in the source code.
    /// </summary>
    public class Lexeme
    {
        /// <summary>
        /// Gets or sets the type of the lexeme.
        /// </summary>
        public LexemeType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the lexeme.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the start index of the lexeme in the source code.
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the end index of the lexeme in the source code.
        /// </summary>
        public int EndIndex { get; set; }
    }

namespace compiler_theory.app.model;

    /// <summary>
    /// Enum representing the type of a lexeme in the source code.
    /// </summary>
    public enum LexemeType
    {
        /// <summary>
        /// Represents a keyword in the source code.
        /// </summary>
        Keyword,

        /// <summary>
        /// Represents an identifier in the source code.
        /// </summary>
        Identifier,

        /// <summary>
        /// Represents an operator in the source code.
        /// </summary>
        Operator,

        /// <summary>
        /// Represents a separator in the source code.
        /// </summary>
        Separator,

        /// <summary>
        /// Represents a data type in the source code.
        /// </summary>
        DataType,

        /// <summary>
        /// Represents an invalid lexeme in the source code.
        /// </summary>
        Invalid
    }

namespace compiler_theory.app.model;

/// <summary>
/// The Scanner class is responsible for scanning the input text and generating a list of tokens.
/// </summary>
public class Scanner
    {
        private readonly string _inputText;
        private int _currentIndex;
        public readonly List<Token> Tokens;

        /// <summary>
        /// Initializes a new instance of the Scanner class with the specified input text.
        /// </summary>
        /// <param name="inputText">The input text to scan.</param>
        public Scanner(string inputText)
        {
            _inputText = inputText;
            _currentIndex = 0;
            Tokens = new List<Token>();
        }
        
        /// <summary>
        /// Determines whether the specified identifier is a variable name.
        /// </summary>
        /// <param name="identifier">The identifier to check.</param>
        /// <returns>A LexemeType indicating whether the identifier is a variable name.</returns>
        private static LexemeType IsVariableName(string identifier)
        {
            if (char.IsLetter(identifier[0]) != false) return LexemeType.Identifier;
            return identifier[0] == '_' ? LexemeType.Identifier : LexemeType.Invalid;
        }
        
        /// <summary>
        /// Analyzes the input text and generates a list of tokens.
        /// </summary>
        public void Analyze()
        {
            while (_currentIndex < _inputText.Length)
            {
                var currentChar = _inputText[_currentIndex];

                if (char.IsWhiteSpace(currentChar))
                {
                    _currentIndex++;
                    
                    Tokens.Add(new Token
                    {
                        Code = GetCode(" Пробел ", LexemeType.Separator),
                        Type = LexemeType.Separator,
                        Value = " Пробел ",
                        StartIndex = _currentIndex,
                        EndIndex = _currentIndex
                    });

                    continue; 
                }
                if (char.IsLetterOrDigit(currentChar) || currentChar == '_')
                {
                    AnalyzeIdentifier();
                }
                else if (currentChar == '\n')
                {
                    AddToken(LexemeType.Separator, "\\n");
                    _currentIndex++;
                }
                else if (currentChar == '=' || currentChar == '<' || currentChar == '>')
                {
                    AnalyzeOperator();
                }
                else if (currentChar == ',' || currentChar == ';')
                {
                    AnalyzeSeparator();
                }
                else if (currentChar == '(' || currentChar == ')')
                {
                    AnalyzeSeparator();
                }
                else
                {
                    AddToken(LexemeType.Invalid, currentChar.ToString());
                    _currentIndex++;
                }
            }
        }

        private void AnalyzeIdentifier()
        {
            var startIndex = _currentIndex;
            while (_currentIndex < _inputText.Length && (char.IsLetterOrDigit(_inputText[_currentIndex]) || _inputText[_currentIndex] == '_'))
            {
                _currentIndex++;
            }

            var identifier = _inputText.Substring(startIndex, _currentIndex - startIndex);

            var identifierType = GetIdentifierType(identifier);
            AddToken(identifierType, identifier);
        }

        private static LexemeType GetIdentifierType(string identifier)
        {
            var keywords = new List<string> { "HashMap", "int", "string", "String", "Map", "new", "bool", "float", "byte", "short", "long", "boolean", "char" };

            if (keywords.Contains(identifier))
            {
                return LexemeType.Keyword;
            }
            else if (char.IsDigit(identifier[0]))
            {
                return LexemeType.Invalid; 
            }
            else
            {
                return IsVariableName(identifier);
            }
        }


        private void AnalyzeOperator()
        {
            var startIndex = _currentIndex;
            _currentIndex++;
            AddToken(LexemeType.Operator, _inputText.Substring(startIndex, 1));
        }

        private void AnalyzeSeparator()
        {
            var startIndex = _currentIndex;
            _currentIndex++;
            AddToken(LexemeType.Separator, _inputText.Substring(startIndex, 1));
        }

        private void AddToken(LexemeType type, string value)
        {
            Tokens.Add(new Token
            {
                Code = GetCode(value, type),
                Type = type,
                Value = value,
                StartIndex = _currentIndex - value.Length + 1,
                EndIndex = _currentIndex
            });
        }

        private static string GetCode(string value, LexemeType type)
        {
            if (type == LexemeType.Identifier) return "12";

            return value switch
            {
                "int" => "1",
                "bool" => "2",
                "string" or "String" => "3",
                "Map" => "4",
                "new" => "5",
                "float" => "6",
                "byte" => "7",
                "short" => "8",
                "long" => "9",
                "char" => "10",
                "HashMap" => "11",
                " Пробел " => "13",
                "=" => "14",
                ";" => "15",
                "<" => "16",
                ">" => "17",
                "," => "18",
                "(" => "19",
                ")" => "20",
                "\n" => "21",
                _ => "Недопустимый символ"
            };
        }
    }


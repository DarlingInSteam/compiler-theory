namespace compiler_theory.app.view_model;

using System;
using System.Collections.Generic;


    public enum LexemeType
    {
        Keyword,
        Identifier,
        Operator,
        Separator,
        DataType,
        Invalid
    }

    public class Lexeme
    {
        public LexemeType Type { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }

    public class Token
    {
        public string Code { get; set; }
        public LexemeType Type { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }

    public class Analyzer
    {
        private string inputText;
        private int currentIndex;
        public List<Token> tokens;

        public Analyzer(string inputText)
        {
            this.inputText = inputText;
            this.currentIndex = 0;
            this.tokens = new List<Token>();
        }
        
        private LexemeType IsVariableName(string identifier)
        {
            if (Char.IsLetter(identifier[0]) == false)
            {
                if (identifier[0] == '_') return LexemeType.Identifier;
                return LexemeType.Invalid;
            }

            return LexemeType.Identifier;
        }
        
        public void Analyze()
        {
            while (currentIndex < inputText.Length)
            {
                char currentChar = inputText[currentIndex];

                if (Char.IsWhiteSpace(currentChar))
                {
                    currentIndex++;
                    AddToken(LexemeType.Separator, " Пробел ");

                    continue; 
                }
                if (Char.IsLetterOrDigit(currentChar) || currentChar == '_')
                {
                    AnalyzeIdentifier();
                }
                else if (currentChar == '\n')
                {
                    // Символ новой строки, добавляем его как токен Separator
                    AddToken(LexemeType.Separator, "\\n");
                    currentIndex++;
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
                    // Неопределенный символ, считаем его ошибкой
                    AddToken(LexemeType.Invalid, currentChar.ToString());
                    currentIndex++;
                }
            }
        }

        private void AnalyzeIdentifier()
        {
            int startIndex = currentIndex;
            while (currentIndex < inputText.Length && (Char.IsLetterOrDigit(inputText[currentIndex]) || inputText[currentIndex] == '_'))
            {
                currentIndex++;
            }

            string identifier = inputText.Substring(startIndex, currentIndex - startIndex);

            // Добавляем проверку на ключевые слова или недопустимые идентификаторы
            LexemeType identifierType = GetIdentifierType(identifier);
            AddToken(identifierType, identifier);
        }

        private LexemeType GetIdentifierType(string identifier)
        {
            // Пример: Список ключевых слов
            List<string> keywords = new List<string> { "HashMap", "int", "string", "String", "Map", "new", "bool", "float", "byte", "short", "long", "boolean", "char" };

            if (keywords.Contains(identifier))
            {
                return LexemeType.Keyword;
            }
            else if (Char.IsDigit(identifier[0]))
            {
                return LexemeType.Invalid; // Идентификатор не может начинаться с цифры
            }
            else
            {
                return IsVariableName(identifier);
            }
        }


        private void AnalyzeOperator()
        {
            int startIndex = currentIndex;
            currentIndex++;
            AddToken(LexemeType.Operator, inputText.Substring(startIndex, 1));
        }

        private void AnalyzeSeparator()
        {
            int startIndex = currentIndex;
            currentIndex++;
            AddToken(LexemeType.Separator, inputText.Substring(startIndex, 1));
        }

        private void AddToken(LexemeType type, string value)
        {
            tokens.Add(new Token
            {
                Code = GetCode(value, type),
                Type = type,
                Value = value,
                StartIndex = currentIndex - value.Length,
                EndIndex = currentIndex - 1
            });
        }

        private string GetCode(string value, LexemeType type)
        {
            if (type == LexemeType.Identifier) return "12";
            
            switch (value)
            {
                case "int" :
                    return "1";
                case "bool" :
                    return "2";
                case "string" or "String" :
                    return "3";
                case "Map" :
                    return "4";
                case "new" :
                    return "5";
                case "float" :
                    return "6";
                case "byte" :
                    return "7";
                case "short" :
                    return "8";
                case "long" :
                    return "9";
                case "char" :
                    return "10";
                case "HashMap" :
                    return "11";
                case " Пробел " :
                    return "13";
                case "=" :
                    return "14";
                case ";" :
                    return "15";
                case "<" :
                    return "16";
                case ">" :
                    return "17";
                case "," :
                    return "18";
                case "(" :
                    return "19";
                case ")" :
                    return "20";
                case "\n" :
                    return "21";
                default:
                    return "Недопустимый символ";
            }
        }
    }


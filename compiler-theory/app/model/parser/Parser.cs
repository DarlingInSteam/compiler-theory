using System.Windows.Documents;

namespace compiler_theory.app.model.parser;

public class Parser
{
    private List<Token> TokensBuff { get; set; }
    private List<ParsingError> Errors { get; set; }
    private List<ParsingError> overErrors { get; set; }
    private List<ParsingError> smallErrors { get; set; }
    private List<string> DataTypes { get; } = new List<string> {"int", "string", "String", "bool", "float", "byte", "short", "long", "boolean", "char" };
    private int errorsCount = 0;

    public List<ParsingError> Parse(List<Token> tokens)
    {
        TokensBuff = new List<Token>();
        Errors = new List<ParsingError>();
        overErrors = new List<ParsingError>();
        smallErrors = new List<ParsingError>();

        // Убираем пробелы из списка токенов
        foreach (var token in tokens)
        {
            if (token.Code != "13")
            {
                TokensBuff.Add(token);
            }
        }

        if (TokensBuff.Count == 0)
        {
            Errors.Add(new ParsingError
            {
                Message = $"Ожидался токен Map",
                StartIndex = 0,
                EndIndex = 0,
                ExpectedToken = ""
            });

            return Errors;
        }
        
        if (TokensBuff.Count > 18)
        {
            for (int i = 18; i < TokensBuff.Count; i++)
            {
                errorsCount += 1;
                overErrors.Add(new ParsingError
                {
                    Message = "Недопустимый токен",
                    StartIndex = TokensBuff[i].StartIndex,
                    EndIndex = TokensBuff[i].EndIndex,
                    ExpectedToken = TokensBuff[i].Value,
                    NumberOfError = errorsCount
                });
            }
        }

        if (TokensBuff.Count < 18)
        {
            for (int i = TokensBuff.Count; i <= 18; i++)
            {
                var expectedToken = i switch
                {
                    1 => "Map",
                    2 => "<",
                    3 => "Data type",
                    4 => ",",
                    5 => "Data type",
                    6 => ">",
                    7 => "Identifier",
                    8 => "=",
                    9 => "new",
                    10 => "HashMap",
                    11 => "<",
                    12 => "Data type",
                    13 => ",",
                    14 => "Data type",
                    15 => ">",
                    16 => "(",
                    17 => ")",
                    18 => ";"
                };

                errorsCount += 1;
                
                smallErrors.Add(new ParsingError
                {
                    Message = $"Ожидался токен '{expectedToken}'",
                    StartIndex = tokens.Count > 0 ? tokens[tokens.Count - 1].EndIndex + 1 : 0,
                    EndIndex = tokens.Count > 0 ? tokens[tokens.Count - 1].EndIndex + 1 : 0,
                    ExpectedToken = expectedToken,
                    NumberOfError = errorsCount
                });
            }
        }
        
        CheckExpectedToken("Map");
        CheckExpectedToken("<");
        CheckExpectedToken("Data type");
        CheckExpectedToken(",");
        CheckExpectedToken("Data type");
        CheckExpectedToken(">");
        CheckExpectedToken("Identifier");
        CheckExpectedToken("=");
        CheckExpectedToken("new");
        CheckExpectedToken("HashMap");
        CheckExpectedToken("<");
        CheckExpectedToken("Data type");
        CheckExpectedToken(",");
        CheckExpectedToken("Data type");
        CheckExpectedToken(">");
        CheckExpectedToken("(");
        CheckExpectedToken(")");
        CheckExpectedToken(";");

        if (smallErrors.Count > 0)
        {
            foreach (var error in smallErrors)
            {
                Errors.Add(error);
            }
        }

        if (overErrors.Count > 0)
        {
            foreach (var error in overErrors)
            {
                Errors.Add(error);
            }
        }
        
        return Errors;
    }

    private void CheckExpectedToken(string expected)
    {
        if (TokensBuff.Count > 0)
        {
            var token = TokensBuff[0];
            TokensBuff.RemoveAt(0);

            switch (expected)
            {
                case "Identifier":
                {
                    if (token.Type != LexemeType.Identifier)
                    {
                        errorsCount += 1;
                        
                        Errors.Add(new ParsingError
                        {
                            Message = $"Ожидалось '{expected}'",
                            StartIndex = token.StartIndex,
                            EndIndex = token.EndIndex,
                            ExpectedToken = token.Value,
                            NumberOfError = errorsCount
                        });
                    }

                    break;
                }
                case "Data type":
                {
                    if (!DataTypes.Contains(token.Value))
                    {
                        errorsCount += 1;
                        
                        Errors.Add(new ParsingError
                        {
                            Message = $"Ожидалось Data type",
                            StartIndex = token.StartIndex,
                            EndIndex = token.EndIndex,
                            ExpectedToken = token.Value,
                            NumberOfError = errorsCount
                        });
                    }

                    break;
                }
                default:
                {
                    if (token.Value != expected)
                    {
                        errorsCount += 1;
                        
                        Errors.Add(new ParsingError
                        {
                            Message = $"Ожидалось '{expected}'",
                            StartIndex = token.StartIndex,
                            EndIndex = token.EndIndex,
                            ExpectedToken = token.Value,
                            NumberOfError = errorsCount
                        });
                    }

                    break;
                }
            }
        }
    }
}

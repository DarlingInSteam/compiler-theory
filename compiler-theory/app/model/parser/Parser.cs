using System.Windows.Documents;

namespace compiler_theory.app.model.parser;

public class Parser
{
    public static List<ParsingError> Parse(List<Token> tokens)
    {
        var errors = new List<ParsingError>();
        var tokensBuff = new List<Token>();
        var currentToken = 1;
        var dataTypes = new List<string> {"int", "string", "String", "bool", "float", "byte", "short", "long", "boolean", "char" };

        foreach (var token in tokens)
        {
            if (token.Code != "13")
            {
                tokensBuff.Add(token);
            }
        }

        foreach (var token in tokensBuff)
        {
            if (tokensBuff.Count == 0)
            {
                // Если нет токенов, добавляем ошибку о том, что ожидается определенная строка
                errors.Add(new ParsingError
                {
                    Message = $"Ожидалось ключевое слово Map, но получен пустой ввод",
                    StartIndex = 0,
                    EndIndex = 0,
                    ExpectedToken = ""
                });
            }
            
            if (currentToken == 1 && tokensBuff.Count >= 1)
            {
                if (token.Value != "Map")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось ключевое слово Map",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }

            if (currentToken == 2 && tokensBuff.Count >= 2)
            {
                if (token.Value != "<")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось <",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 3 && tokensBuff.Count >= 3)
            {
                if (!dataTypes.Contains(token.Value))
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось DataType",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 4 && tokensBuff.Count >= 4)
            {
                if (token.Value != ",")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось ,",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 5 && tokensBuff.Count >= 5)
            {
                if (!dataTypes.Contains(token.Value))
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось DataType",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 6 && tokensBuff.Count >= 6)
            {
                if (token.Value != ">")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось >",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 7 && tokensBuff.Count >= 7)
            {
                if (token.Type != LexemeType.Identifier)
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидался Identifier",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 8 && tokensBuff.Count >= 8)
            {
                if (token.Value != "=")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось =",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 9 && tokensBuff.Count >= 9)
            {
                if (token.Value != "new")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось new",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 10 && tokensBuff.Count >= 10)
            {
                if (token.Value != "HashMap")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось HashMap",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 11 && tokensBuff.Count >= 11)
            {
                if (token.Value != "<")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось <",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 12 && tokensBuff.Count >= 12)
            {
                if (!dataTypes.Contains(token.Value))
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось DataType",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 13 && tokensBuff.Count >= 13)
            {
                if (token.Value != ",")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось ,",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 14 && tokensBuff.Count >= 14)
            {
                if (!dataTypes.Contains(token.Value))
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось DataType",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 15 && tokensBuff.Count >= 15)
            {
                if (token.Value != ">")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось >",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 16 && tokensBuff.Count >= 16)
            {
                if (token.Value != "(")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось (",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 17 && tokensBuff.Count >= 17)
            {
                if (token.Value != ")")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось )",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }
            
            if (currentToken == 18 && tokensBuff.Count >= 18)
            {
                if (token.Value != ";")
                {
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидалось ;",
                        StartIndex = token.StartIndex,
                        EndIndex = token.EndIndex,
                        ExpectedToken = token.Value
                    });
                }

                currentToken += 1;
                continue;
            }

            if (tokensBuff.Count > 18)
            {
                for (int i = 18; i < tokensBuff.Count; i++)
                {
                    errors.Add(new ParsingError
                    {
                        Message = "недопустимый токен",
                        StartIndex = tokensBuff[i].StartIndex,
                        EndIndex = tokensBuff[i].EndIndex,
                        ExpectedToken = tokensBuff[i].Value
                    });
                }

                break;
            }
            
            if (tokensBuff.Count < 18)
            {
                for (int i = tokensBuff.Count; i <= 18; i++)
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
                    
                    errors.Add(new ParsingError
                    {
                        Message = $"Ожидался токен '{expectedToken}', но достигнут конец ввода",
                        StartIndex = tokens.Count > 0 ? tokens[tokens.Count - 1].EndIndex + 1 : 0,
                        EndIndex = tokens.Count > 0 ? tokens[tokens.Count - 1].EndIndex + 1 : 0,
                        ExpectedToken = expectedToken
                    });
                }
                
                break;
            }
        }

        return errors;
    }
}
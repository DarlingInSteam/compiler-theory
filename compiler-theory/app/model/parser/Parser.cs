using System.Windows.Documents;

namespace compiler_theory.app.model.parser;

public class Parser
{
    private List<Token> _tokens;
    private List<ParsingError> _parsingErrors;
    private List<string> _dataTypes;
    private List<string> _keyWords;
    private List<string> _signs;
    private List<string> _operators;
    private int _currentToken;

    public Parser(List<Token> tokens, int currentToken = 0)
    {
        _tokens = tokens;
        _currentToken = currentToken;
        _parsingErrors = new List<ParsingError>();
        _dataTypes = new List<string>
            { "int", "string", "String", "bool", "float", "byte", "short", "long", "boolean", "char" };
        _keyWords = new List<string>
            { "Map", "HashMap", "new" };
        _signs = new List<string>
            { ",", ";", "(", ")", "<", ">" };
        _operators = new List<string>
            { "=" };
    }

    public List<ParsingError> Parse()
    {
        List<ParsingError> errors = new List<ParsingError>();
        
        while (_currentToken < _tokens.Count)
        {
            List<ParsingError> buff = ParseStatement();

            foreach (var error in buff)
            {
                errors.Add(error);
            }
            
            _parsingErrors.AddRange(errors);
            if (errors.Count > 0)
            {
                SkipToNextStatement();
            }
        }

        return _parsingErrors;
    }

    private List<ParsingError> ParseStatement()
    {
        List<ParsingError> errors = new List<ParsingError>();
        
        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "4")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалось ключевое слово 'Map', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 2,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "16")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался символ '<', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 3,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || !_dataTypes.Contains(_tokens[_currentToken].Value))
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался тип данных, но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 4,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "18")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалась запятая ',', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 5,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || !_dataTypes.Contains(_tokens[_currentToken].Value))
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался тип данных, но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 6,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "17")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался символ '>', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 7,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || !IsIdentifier(_tokens[_currentToken].Value))
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался идентификатор (имя переменной), но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 8,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "14")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался оператор '=', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 9,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "5")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалось ключевое слово 'new', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 10,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "11")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалось ключевое слово 'HashMap', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 11,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "16")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалась открывающая '<', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 18,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || !_dataTypes.Contains(_tokens[_currentToken].Value))
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался тип данных, но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 13,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "18")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалась запятая ',', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 14,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || !_dataTypes.Contains(_tokens[_currentToken].Value))
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался тип данных, но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 15,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "17")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался символ '>', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 16,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "19")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалась открывающая круглая скобка '(', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 19,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "20")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидалась закрывающая круглая скобка ')', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 19,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }
        else
        {
            _currentToken++;
        }

        if (_currentToken >= _tokens.Count || _tokens[_currentToken].Code != "15")
        {
            errors.Add(new ParsingError
            {
                Message = $"Ожидался символ ';', но получено '{GetTokenValue(_currentToken)}'",
                NumberOfError = 17,
                StartIndex = GetTokenStartIndex(_currentToken),
                EndIndex = GetTokenEndIndex(_currentToken)
            });
            _currentToken++;
        }

        _currentToken++;

        
        return errors;
    }

    private void SkipToNextStatement()
    {
        while (_currentToken < _tokens.Count && _tokens[_currentToken].Code != "15")
        {
            _currentToken++;
        }

        if (_currentToken < _tokens.Count && _tokens[_currentToken].Code == "15")
        {
            _currentToken++; // Переходим к следующему токену после ;
        }
    }

    private bool IsIdentifier(string value)
    {
        // Проверка на соответствие идентификатора (имени переменной)
        // Здесь можно добавить более сложную логику проверки
        return !_dataTypes.Contains(value) && !_keyWords.Contains(value) && !_signs.Contains(value) && !_operators.Contains(value);
    }

    private string GetTokenValue(int index)
    {
        return index < _tokens.Count ? _tokens[index].Value : "Конец входной строки";
    }

    private int GetTokenStartIndex(int index)
    {
        return index < _tokens.Count ? _tokens[index].StartIndex : -1;
    }

    private int GetTokenEndIndex(int index)
    {
        return index < _tokens.Count ? _tokens[index].EndIndex : -1;
    }
}



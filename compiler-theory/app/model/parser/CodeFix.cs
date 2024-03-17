using System.Security.Claims;

namespace compiler_theory.app.model.parser;

public class CodeFix
{
    public static string Fix(string code, List<ParsingError> parsingErrors)
    {
        string codeBuff = code;
        
        foreach (var parsingError in parsingErrors)
        {
            if (parsingError.ErrorToken != "<EOF>")
            {
                codeBuff = codeBuff.Replace(parsingError.ErrorToken, parsingError.NeedToken);
            }
            else
            {
                if (parsingError.NeedToken == "HashMap")
                {
                    codeBuff += " ";
                    codeBuff += parsingError.NeedToken;
                }
                else
                {
                    codeBuff += parsingError.NeedToken;
                }
            }
        }

        return codeBuff;
    }
}
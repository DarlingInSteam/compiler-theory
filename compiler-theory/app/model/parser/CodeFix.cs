using System.Security.Claims;

namespace compiler_theory.app.model.parser
{
    /// <summary>
    /// The CodeFix class provides a method for fixing code based on a list of parsing errors.
    /// </summary>
    public class CodeFix
    {
        /// <summary>
        /// Fixes the specified code based on the provided list of parsing errors.
        /// </summary>
        /// <param name="code">The code to fix.</param>
        /// <param name="parsingErrors">The list of parsing errors.</param>
        /// <returns>The fixed code.</returns>
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
                    if (parsingError.NeedToken == "Char" || parsingError.NeedToken == "," || parsingError.NeedToken == "<" || parsingError.NeedToken == ">" || parsingError.NeedToken == "(" || parsingError.NeedToken == ")" || parsingError.NeedToken == ";")
                    {
                        codeBuff += parsingError.NeedToken;
                    }
                    else if (parsingError.NeedToken == "_asd")
                    {
                        codeBuff = codeBuff.Insert(parsingError.StartIndex, parsingError.NeedToken);
                    }
                    else
                    {
                        codeBuff += " ";
                        codeBuff += parsingError.NeedToken;
                    }
                }
            }

            return codeBuff;
        }
    }
}
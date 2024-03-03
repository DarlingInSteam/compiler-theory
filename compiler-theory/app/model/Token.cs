namespace compiler_theory.app.view_model;

public class Token
{
    public string Code { get; set; }
    public LexemeType Type { get; set; }
    public string Value { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}
namespace MimaSim.Core.Tokenizer
{
    public class TokenMatch
    {
        public TokenKind TokenType { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Precedence { get; set; }
    }
}
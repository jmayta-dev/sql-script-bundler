namespace SSB.Shared.Helpers
{
    public static class StringHelpers
    {
        public static string AsciiUppercase => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string AsciiLowercase => "abcdefghijklmnopqrstuvwxyz";
        public static string Digits => "0123456789";
        public static string Punctuation => "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        public static string Printable => AsciiUppercase + AsciiLowercase + Digits + Punctuation;
        public static string PrintableSafe => AsciiUppercase + AsciiLowercase + Digits;


        public static string RandomString(string stringCharacters, int length)
        {
            Random random = new();
            return new string(
                Enumerable.Repeat(stringCharacters, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

using swiftmt799_web_api.Models;
using System.Text.RegularExpressions;

namespace swiftmt799_web_api.Utils
{
    public static class SwiftMT799Factory
    {
        private const string Block1Pattern = @"^{1:([A-Z0-9]+)}";
        private const string Block2Pattern = @"{2:([A-Z0-9]+)}";
        private const string Block3Pattern = @"{3:([^}]*)}";
        private const string Block4Pattern = @"{4:([^}]*)}";
        private const string Block5Pattern = @"{5:(.+)}";

        private const string Field20Pattern = @":20:([^\s]{1,16})";
        private const string Field21Pattern = @":21:([^\s]{1,16})";
        private const string Field79Pattern = @":79:(.+)-";

        public static SwiftMT799Message createFromString(string text)
        {
            string? basicHeaderText, applicationHeaderText, userHeaderText, transactionReferenceNumber, relatedReference, narrative, trailerBlockText;
            basicHeaderText = applicationHeaderText = userHeaderText = transactionReferenceNumber = relatedReference = narrative = trailerBlockText = null;

            var block1Match = Regex.Match(text, Block1Pattern);
            if (block1Match.Success)
            {
                basicHeaderText = block1Match.Groups[1].Value;
            }

            var block2Match = Regex.Match(text, Block2Pattern);
            if (block2Match.Success)
            {
                applicationHeaderText = block2Match.Groups[1].Value;
            }

            var block3Match = Regex.Match(text, Block3Pattern);
            if (block3Match.Success)
            {
                userHeaderText = block3Match.Groups[1].Value;
            }

            var block4Match = Regex.Match(text, Block4Pattern);
            string block4Content = block4Match.Groups[1].Value;

            var field20Match = Regex.Match(block4Content, Field20Pattern);
            if (field20Match.Success)
            {
                transactionReferenceNumber = field20Match.Groups[1].Value;
            }

            var field21Match = Regex.Match(block4Content, Field21Pattern);
            if (field21Match.Success)
            {
                relatedReference = field21Match.Groups[1].Value;
            }

            var field79Match = Regex.Match(block4Content, Field79Pattern, RegexOptions.Singleline);
            if (field79Match.Success)
            {
                narrative = field79Match.Groups[1].Value;
            }

            var block5Match = Regex.Match(text, Block5Pattern);
            if (block5Match.Success)
            {
                trailerBlockText = block5Match.Groups[1].Value;
            }

            return new SwiftMT799Message(basicHeaderText, applicationHeaderText, userHeaderText, transactionReferenceNumber, relatedReference, narrative, trailerBlockText);
        }
    }
}

using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class SimpleFlag : IFlag
    {
        private string Name { get; set; }
        private string Value { get; set; }

        public SimpleFlag()
        {
        }

        public SimpleFlag(string name, string value)
        {
            SetNameAndValue(name, value);
        }

        public SimpleFlag SetNameAndValue(string name, string value)
        {
            Name = name;
            Value = value;
            return this;
        }

        public string WritePart()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"-{Name}");

            if (!string.IsNullOrEmpty(Value))
                sb.Append($" {Value} ");
            else
                sb.Append(" ");

            return sb.ToString();
        }
    }
}

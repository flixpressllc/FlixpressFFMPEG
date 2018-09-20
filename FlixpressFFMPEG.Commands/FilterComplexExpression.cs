using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class FilterComplexExpression : ISelfWriter
    {
        public List<string> InputIdentifiers { get; set; }
        public string OutputIdentifier { get; set; }
        public List<Filter> Filters { get; set; }

        public FilterComplexExpression()
        {
            InputIdentifiers = new List<string>();
            Filters = new List<Filter>();
        }

        public FilterComplexExpression AddFilter(Filter filter)
        {
            Filters.Add(filter);
            return this;
        }

        public FilterComplexExpression AddInputIdentifier(string inputIdentifier)
        {
            InputIdentifiers.Add(inputIdentifier);
            return this;
        }

        public FilterComplexExpression SetOutputIdentifier(string outputIdentifier)
        {
            OutputIdentifier = outputIdentifier;
            return this;
        }

        public string WritePart()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(string inputIdentifier in InputIdentifiers)
                sb.Append($"[{inputIdentifier}]");

            for(int i = 0; i < Filters.Count; i++)
            {
                sb.Append(Filters[i].WritePart());

                if (i < Filters.Count - 1)
                    sb.Append(", ");
            }

            if (!string.IsNullOrEmpty(OutputIdentifier))
                sb.Append($"[{OutputIdentifier}]");

            return sb.ToString();
        }
    }
}

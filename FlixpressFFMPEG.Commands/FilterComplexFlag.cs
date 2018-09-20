using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class FilterComplexFlag : IFlag
    {
        public List<FilterComplexExpression> FilterComplexExpressions { get; set; }

        public FilterComplexFlag()
        {
            FilterComplexExpressions = new List<FilterComplexExpression>();
        }

        public FilterComplexFlag AddFilterComplexExpression(FilterComplexExpression filterComplexExpression)
        {
            FilterComplexExpressions.Add(filterComplexExpression);
            return this;
        }

        public string WritePart()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("-filter_complex \"");

            for (int i = 0; i < FilterComplexExpressions.Count; i++)
            {
                sb.Append(FilterComplexExpressions[i].WritePart());

                if (i < FilterComplexExpressions.Count - 1)
                    sb.Append(", ");
            }

            sb.Append("\"");
            return sb.ToString();
        }
    }
}

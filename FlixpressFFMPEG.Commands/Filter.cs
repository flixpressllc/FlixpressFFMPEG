using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class Filter : ICommandPart
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public List<FilterAttribute> FilterAttributes { get; set; }

        public Filter()
        {
            FilterAttributes = new List<FilterAttribute>();
        }

        public Filter AddAttribute(string name, string value)
        {
            return AddAttribute(new FilterAttribute { Name = name, Value = value });
        }

        public Filter AddAttribute(FilterAttribute filterAttribute)
        {
            FilterAttributes.Add(filterAttribute);
            return this;
        }

        public Filter SetName(string name)
        {
            Name = name;
            return this;
        }

        public Filter SetValue(string value)
        {
            Value = value;
            return this;
        }

        public string WritePart()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);

            if ((FilterAttributes.Count > 0) || (!string.IsNullOrEmpty(Value)))
                sb.Append("=");

            if (!string.IsNullOrEmpty(Value))
                sb.Append(Value);
            else
            {
                for (int i = 0; i < FilterAttributes.Count; i++)
                {
                    sb.Append(FilterAttributes[i].WritePart());

                    if (i < FilterAttributes.Count - 1)
                    {
                        sb.Append(": ");
                    }
                }
            }

            return sb.ToString();
        }
    }
}

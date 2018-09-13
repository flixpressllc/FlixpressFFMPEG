namespace FlixpressFFMPEG.Commands
{
    public class FilterAttribute : ICommandPart
    {
        public string Name { get; set; }

        /// <summary>
        /// Value may actually be an expression, but we'll write out that expression string explicitly for now.
        /// </summary>
        public string Value { get; set; }

        public string WritePart()
        {
            return $"{Name}={Value}";
        }
    }
}

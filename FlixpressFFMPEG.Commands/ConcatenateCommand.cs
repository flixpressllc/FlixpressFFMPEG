using System.Collections.Generic;

namespace FlixpressFFMPEG.Commands
{
    public class ConcatenateCommand : CommandBase, ISelfWriter
    {
        public List<string> Files { get; set; }

        public ConcatenateCommand(string executePath) : base(executePath)
        {
            Files = new List<string>();
        }

        public ConcatenateCommand AddFile(string file)
        {
            Files.Add(file);
            return this;
        }

        public ConcatenateCommand AddFiles(List<string> additionalFiles)
        {
            Files.AddRange(additionalFiles);
            return this;
        }

        public ConcatenateCommand SetOutput(string output)
        {
            FFMPEGCommand.SetOutput(output);
            return this;
        }

        public string WritePart()
        {
            Files.ForEach(file =>
            {
                FFMPEGCommand.AddInput(file);
            });

            FilterComplexFlag filterComplexFlag = new FilterComplexFlag();

            for(int n = 0; n < Files.Count - 1; n++)
            {
                string resultingBaseFromPrevious = (n == 0) ? "0" : "res" + n;
                filterComplexFlag.AddFilterComplexExpression(new FilterComplexExpression()
                    .AddInputIdentifier(resultingBaseFromPrevious)
                    .AddInputIdentifier($"{n + 1}")
                    .AddFilter(new Filter()
                        .SetName("concat")
                        .AddAttribute("n","2")
                    )
                    .SetOutputIdentifier((n < Files.Count - 2) ? $"res{n + 1}" : "")
                );
            }

            FFMPEGCommand.AddFlag(filterComplexFlag);

            return FFMPEGCommand.WritePart();
        }
    }
}

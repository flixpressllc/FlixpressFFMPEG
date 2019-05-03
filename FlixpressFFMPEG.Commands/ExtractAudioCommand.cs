using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class ExtractAudioCommand : CommandBase, ISelfWriter
    {
        private string InputVideoPath { get; set; }

        public ExtractAudioCommand(string executePath) : base(executePath)
        {
        }

        public ExtractAudioCommand SetInputValues(string inputVideoPath)
        {
            InputVideoPath = inputVideoPath;
            return this;
        }

        public ExtractAudioCommand SetOutput(string outputVideoPath)
        {
            FFMPEGCommand.SetOutput(outputVideoPath);
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(InputVideoPath);
            
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

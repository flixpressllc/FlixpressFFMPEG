using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class ResizeVideoCommand : CommandBase, ISelfWriter
    {
        private string InputVideoPath { get; set; }
        private Dimension VideoDimensions { get; set; }

        public ResizeVideoCommand(string executePath) : base(executePath)
        {
        }

        public ResizeVideoCommand SetInputValues(string inputVideoPath, Dimension imageDimensions)
        {
            InputVideoPath = inputVideoPath;
            VideoDimensions = imageDimensions;
            return this;
        }

        public ResizeVideoCommand SetOutput(string outputVideoPath)
        {
            FFMPEGCommand.SetOutput(outputVideoPath);
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(InputVideoPath);

            string dimensionString = $"{VideoDimensions.Width.ToString()}:{VideoDimensions.Height.ToString()}";
            FFMPEGCommand.AddFlag(new SimpleFlag("vf", $"scale={dimensionString}"));

            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

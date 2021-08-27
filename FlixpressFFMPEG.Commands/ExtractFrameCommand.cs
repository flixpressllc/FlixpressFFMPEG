using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class ExtractFrameCommand : CommandBase, ISelfWriter
    {
        private string InputVideoPath { get; set; }
        private Dimension ImageDimensions { get; set; }
        private double ExtractAtSecondMark { get; set; }

        public ExtractFrameCommand(string executePath) : base(executePath)
        {
        }

        public ExtractFrameCommand SetInputValues(string inputVideoPath,
            double extractAtSecondMark, Dimension imageDimensions = null)
        {
            InputVideoPath = inputVideoPath;
            ExtractAtSecondMark = extractAtSecondMark;
            ImageDimensions = imageDimensions;
            return this;
        }

        public ExtractFrameCommand SetOutput(string outputImagePath)
        {
            FFMPEGCommand.SetOutput(outputImagePath);
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(InputVideoPath);

            FFMPEGCommand.AddFlag(new SimpleFlag("ss", ExtractAtSecondMark.ToString()));
            FFMPEGCommand.AddFlag(new SimpleFlag("vframes", "1"));

            if (ImageDimensions != null)
            {
                string dimensionString = $"{ImageDimensions.Width.ToString()}x{ImageDimensions.Height.ToString()}";
                FFMPEGCommand.AddFlag(new SimpleFlag("s", dimensionString));
            }

            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

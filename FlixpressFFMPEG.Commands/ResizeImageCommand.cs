using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class ResizeImageCommand : CommandBase, ISelfWriter
    {
        private string InputImagePath { get; set; }
        private Dimension ImageDimensions { get; set; }

        public ResizeImageCommand(string executePath) : base(executePath)
        {
        }

        public ResizeImageCommand SetInputValues(string inputImagePath, Dimension imageDimensions)
        {
            InputImagePath = inputImagePath;
            ImageDimensions = imageDimensions;
            return this;
        }

        public ResizeImageCommand SetOutput(string outputImagePath)
        {
            FFMPEGCommand.SetOutput(outputImagePath);
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(InputImagePath);

            string dimensionString = $"{ImageDimensions.Width.ToString()}:{ImageDimensions.Height.ToString()}";
            FFMPEGCommand.AddFlag(new SimpleFlag("vf", $"scale={dimensionString}"));

            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

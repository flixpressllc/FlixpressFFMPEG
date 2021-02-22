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
            // fix for .png transparency thumbnails
            FFMPEGCommand.AddFlag(new SimpleFlag("filter_complex", $"\"[0]scale={dimensionString}[scaled];[scaled]split = 2[bg][fg];[bg] drawbox=c=0xCCCCCC:replace=1:t=fill[bg];[bg] [fg] overlay\" -f image2 "));

            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class ExtractAudioCommand : CommandBase, ISelfWriter
    {
        private string InputVideoPath { get; set; }
        private int SamplingFrequency { get; set; }
        private string BitRate { get; set; }
        private int NumAudioChannels { get; set; }

        public ExtractAudioCommand(string executePath) : base(executePath)
        {
        }

        public ExtractAudioCommand SetInputValues(string inputVideoPath)
        {
            InputVideoPath = inputVideoPath;
            return this;
        }

        public ExtractAudioCommand SetSamplingFrequency(int samplingFrequency = 48000)
        {
            SamplingFrequency = samplingFrequency;
            return this;
        }

        public ExtractAudioCommand SetBitrate(string bitRate = "192k")
        {
            BitRate = bitRate;
            return this;
        }

        public ExtractAudioCommand SetOutput(string outputVideoPath)
        {
            FFMPEGCommand.SetOutput(outputVideoPath);
            return this;
        }

        public ExtractAudioCommand SetStereo()
        {
            NumAudioChannels = 2;
            return this;
        }

        public ExtractAudioCommand SetMono()
        {
            NumAudioChannels = 0;
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(InputVideoPath);

            if (SamplingFrequency > 0)
            {
                FFMPEGCommand.AddFlag(new SimpleFlag("ar", SamplingFrequency.ToString()));
            }

            if (!string.IsNullOrEmpty(BitRate))
            {
                FFMPEGCommand.AddFlag(new SimpleFlag("b:a", BitRate));
            }

            if (NumAudioChannels >= 2)
            {
                FFMPEGCommand.AddFlag(new SimpleFlag("ac", NumAudioChannels.ToString()));
            }
            
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

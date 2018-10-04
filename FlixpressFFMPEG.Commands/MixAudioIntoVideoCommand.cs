namespace FlixpressFFMPEG.Commands
{
    public class MixAudioIntoVideoCommand : CommandBase, ISelfWriter
    {
        private string VideoPath { get; set; }
        private double VideoDuration { get; set; }
        private string AudioPath { get; set; }
        private double AudioVolume { get; set; }
        private double AudioFadeDuration { get; set; }

        private double Threshold { get; set; }
        private string Ratio { get; set; }
        private double Attack { get; set; }
        private double Release { get; set; }
        private double MakeupGain { get; set; }

        public MixAudioIntoVideoCommand(string executePath) : base(executePath)
        {

        }

        public MixAudioIntoVideoCommand SetVideoPath(string videoPath, double duration)
        {
            VideoPath = videoPath;
            VideoDuration = duration;
            return this;
        }

        public MixAudioIntoVideoCommand SetAudioPath(string audioPath, double volume = 1, double fadeDuration = 1)
        {
            AudioPath = audioPath;
            AudioVolume = volume;
            AudioFadeDuration = fadeDuration;
            return this;
        }

        public MixAudioIntoVideoCommand SetCompressorValues(
            double threshold = 0.25,
            string ratio = "1:3",
            double attack = 1,
            double release = 100,
            double makeupGain = 1)
        {
            Threshold = threshold;
            Ratio = ratio;
            Attack = attack;
            Release = release;
            MakeupGain = makeupGain;

            return this;
        }

        public MixAudioIntoVideoCommand SetOutput(string output)
        {
            FFMPEGCommand.SetOutput(output);
            return this;
        }


        public string WritePart()
        {
            FFMPEGCommand.AddInput(VideoPath);
            FFMPEGCommand.AddInput(AudioPath);
            /*
            FFMPEGCommand.AddFlag(new SimpleFlag("filter_complex", "[0:a][1:a]amix=duration=first[aout]"));
            */

            FilterComplexExpression fadeExpression = new FilterComplexExpression()
                .AddInputIdentifier("1:a")
                .AddFilter(new Filter()
                    .SetName("volume")
                    .SetValue(AudioVolume.ToString()))
                .AddFilter(new Filter()
                    .SetName("afade")
                    .AddAttribute("t", "in")
                    .AddAttribute("st", "0")
                    .AddAttribute("d", AudioFadeDuration.ToString()))                    
                .AddFilter(new Filter()
                    .SetName("afade")
                    .AddAttribute("t", "out")
                    .AddAttribute("st", (VideoDuration - AudioFadeDuration).ToString())
                    .AddAttribute("d", AudioFadeDuration.ToString()))
                .SetOutputIdentifier("afaded");

            FilterComplexExpression mixExpression = new FilterComplexExpression()
                .AddInputIdentifier("0:a")
                .AddInputIdentifier("afaded")
                .AddFilter(new Filter()
                    .SetName("amix")
                    .AddAttribute("duration", "first"))
                .SetOutputIdentifier("acompressed");

            /* Audio Compressor.
             */
            FilterComplexExpression compressorExpression = new FilterComplexExpression()
                .AddInputIdentifier("acompressed")
                .AddFilter(new Filter()
                    .SetName("acompressor")
                    .AddAttribute("threshold", Threshold.ToString())
                    .AddAttribute("ratio", Ratio)
                    .AddAttribute("attack", Attack.ToString())
                    .AddAttribute("release", Release.ToString())
                    .AddAttribute("makeup", MakeupGain.ToString()))
                .SetOutputIdentifier("aout");


            FilterComplexFlag fcf = new FilterComplexFlag()
                .AddFilterComplexExpression(fadeExpression)
                .AddFilterComplexExpression(mixExpression)
                .AddFilterComplexExpression(compressorExpression);

            FFMPEGCommand.AddFlag(fcf);
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "0:v"));
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "[aout]"));       
            
            FFMPEGCommand.AddFlag(new SimpleFlag("c:v", "copy"));
            
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class VideoAudioSyncCommand : CommandBase, ISelfWriter
    {
        private string VideoPath { get; set; }
        private double OffsetAdjustment { get; set; }

        public VideoAudioSyncCommand(string executePath) : base(executePath)
        {
        }

        public VideoAudioSyncCommand SetVideoPath(string videoPath)
        {
            VideoPath = videoPath;
            FFMPEGCommand.AddFlag(new SimpleFlag("i", VideoPath));
            return this;
        }

        public VideoAudioSyncCommand SetOffsetAdjustment(double offsetAdjustment)
        {
            OffsetAdjustment = offsetAdjustment;
            FFMPEGCommand.AddFlag(new SimpleFlag("itsoffset", OffsetAdjustment.ToString()));
            FFMPEGCommand.AddFlag(new SimpleFlag("i", VideoPath));
            FFMPEGCommand.AddFlag(new SimpleFlag("vcodec", "copy"));
            FFMPEGCommand.AddFlag(new SimpleFlag("acodec", "copy"));
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "0:0"));
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "1:1"));
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));
            return this;
        }

        public VideoAudioSyncCommand SetOutput(string output)
        {
            FFMPEGCommand.SetOutput(output);
            return this;
        }


        public string WritePart()
        {
            //StringBuilder sb = new StringBuilder();


            return FFMPEGCommand.WritePart();
        }
    }
}

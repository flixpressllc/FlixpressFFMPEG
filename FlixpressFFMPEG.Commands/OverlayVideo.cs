namespace FlixpressFFMPEG.Commands
{
    public class OverlayVideo
    {
        public string Path { get; set; }
        public int StartOffsetInSeconds { get; set; }
        public int DurationInSeconds { get; set; }

        public OverlayVideo()
        {
        }

        public OverlayVideo(string path, int startOffsetInSeconds, int durationInSeconds)
        {
            Path = path;
            StartOffsetInSeconds = startOffsetInSeconds;
            DurationInSeconds = durationInSeconds;
        }
    }
}

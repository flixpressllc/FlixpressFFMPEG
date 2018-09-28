using FlixpressFFMPEG.Common;

namespace FlixpressFFMPEG.Commands
{
    public class OverlayVideo
    {
        public string Path { get; set; }
        public int StartOffsetInSeconds { get; set; }
        public int DurationInSeconds { get; set; }
        public Dimension Dimension { get; set; }
        public Coordinate Coordinate { get; set; }

        public OverlayVideo()
        {
            Dimension = new Dimension();
            Coordinate = new Coordinate();
        }

        public OverlayVideo(
            string path,
            int startOffsetInSeconds,
            int durationInSeconds,
            Coordinate coordinate = null,
            Dimension dimension = null)
        {
            Path = path;
            StartOffsetInSeconds = startOffsetInSeconds;
            DurationInSeconds = durationInSeconds;

            Coordinate = (coordinate == null) ? new Coordinate() : coordinate;
            Dimension = (dimension == null) ? new Dimension() : dimension;
        }
    }
}

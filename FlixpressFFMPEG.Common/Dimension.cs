namespace FlixpressFFMPEG.Common
{
    public class Dimension
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Dimension()
        {
        }

        public Dimension(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static Dimension Scale(Dimension dimension, double factor)
        {
            return new Dimension(
                width: (int)(dimension.Width * factor),
                height: (int)(dimension.Height * factor));
        }

        public static Dimension ScaleToHeight(Dimension dimension, int desiredHeight)
        {
            double factor = (double)desiredHeight / dimension.Height;
            return Scale(dimension, factor);
        }

        public static Dimension ScaleToWidth(Dimension dimension, int desiredWidth)
        {
            double factor = (double)desiredWidth / dimension.Width;
            return Scale(dimension, factor);
        }
    }
}

using FlixpressFFMPEG.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class SuperImposeCommand : CommandBase, ISelfWriter
    {
        private string BaseVideoPath { get; set; }
        private List<OverlayVideo> OverlayVideos { get; set; }

        public SuperImposeCommand(string executePath) : base(executePath)
        {
            OverlayVideos = new List<OverlayVideo>();
        }

        public SuperImposeCommand SetBaseVideoPath(string baseVideoPath)
        {
            BaseVideoPath = baseVideoPath;
            return this;
        }

        public SuperImposeCommand AddOverlayVideo(OverlayVideo overlayVideo)
        {
            OverlayVideos.Add(overlayVideo);
            return this;
        }

        public SuperImposeCommand AddOverlayVideo(string path, int startOffsetInSeconds, int durationInSeconds, Coordinate coordinate = null, Dimension dimension = null)
        {
            return AddOverlayVideo(new OverlayVideo(path, startOffsetInSeconds, durationInSeconds, coordinate, dimension));
        }

        public SuperImposeCommand SetOutput(string output)
        {
            FFMPEGCommand.SetOutput(output);
            return this;
        }

        public string WritePart()
        {
            FFMPEGCommand.AddInput(BaseVideoPath);

            foreach (OverlayVideo overlayVideo in OverlayVideos)
                FFMPEGCommand.AddInput(overlayVideo.Path);

            FilterComplexFlag filterComplexFlag = new FilterComplexFlag();
            
            // Add the first expression, and that is to merge the first video and the base
            for(int n = 0; n < OverlayVideos.Count; n++)
            {
                OverlayVideo overlayVideo = OverlayVideos[n];


                string resultingBaseFromPrevious = (n == 0) ? "0:v" : "res" + n;

                FilterComplexExpression timingExpression = new FilterComplexExpression()
                    .AddInputIdentifier($"{n + 1}")
                    .AddFilter(new Filter()
                        .SetName("setpts")
                        .SetValue($"PTS+{OverlayVideos[n].StartOffsetInSeconds}/TB")
                    )
                    .SetOutputIdentifier($"top{n + 1}");

                if (overlayVideo.Dimension.Width > 0 || overlayVideo.Dimension.Height > 0)
                    timingExpression.AddFilter(new Filter()
                        .SetName("scale")
                        .SetValue($"{overlayVideo.Dimension.Width.ToString()}:{overlayVideo.Dimension.Height.ToString()}")
                    );

                filterComplexFlag.AddFilterComplexExpression(timingExpression);

                int startOffset = OverlayVideos[n].StartOffsetInSeconds;
                int until = startOffset + OverlayVideos[n].DurationInSeconds;

                Filter overlayFilter = new Filter()
                        .SetName("overlay")
                        .AddAttribute("enable", $"'between(t, {startOffset}, {until})'");

                if (overlayVideo.Coordinate.X > 0 || overlayVideo.Coordinate.Y > 0)
                {
                    overlayFilter.AddAttribute("x", overlayVideo.Coordinate.X.ToString())
                        .AddAttribute("y", overlayVideo.Coordinate.Y.ToString());
                }  

                filterComplexFlag.AddFilterComplexExpression(new FilterComplexExpression()
                    .AddInputIdentifier(resultingBaseFromPrevious)
                    .AddInputIdentifier($"top{n + 1}")
                    .AddFilter(overlayFilter)
                    .SetOutputIdentifier((n < OverlayVideos.Count - 1) ? $"res{n + 1}" : "")
                );

                
            }

            FFMPEGCommand.AddFlag(filterComplexFlag);
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

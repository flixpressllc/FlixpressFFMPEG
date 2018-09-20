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

        public SuperImposeCommand AddOverlayVideo(string path, int startOffsetInSeconds, int durationInSeconds)
        {
            return AddOverlayVideo(new OverlayVideo(path, startOffsetInSeconds, durationInSeconds));
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
                string resultingBaseFromPrevious = (n == 0) ? "0:v" : "res" + n;
                filterComplexFlag.AddFilterComplexExpression(new FilterComplexExpression()
                    .AddInputIdentifier($"{n + 1}")
                    .AddFilter(new Filter()
                        .SetName("setpts")
                        .SetValue($"PTS+{OverlayVideos[n].StartOffsetInSeconds}/TB")

                    )
                    .SetOutputIdentifier($"top{n + 1}")
                );

                int startOffset = OverlayVideos[n].StartOffsetInSeconds;
                int until = startOffset + OverlayVideos[n].DurationInSeconds;

                filterComplexFlag.AddFilterComplexExpression(new FilterComplexExpression()
                    .AddInputIdentifier(resultingBaseFromPrevious)
                    .AddInputIdentifier($"top{n + 1}")
                    .AddFilter(new Filter()
                        .SetName("overlay")
                        .AddAttribute("enable", $"'between(t, {startOffset}, {until})'")
                    )
                    .SetOutputIdentifier((n < OverlayVideos.Count - 1) ? $"res{n + 1}" : "")
                );

                FFMPEGCommand.AddFlag(filterComplexFlag);
            }

            return FFMPEGCommand.WritePart();
        }
    }
}

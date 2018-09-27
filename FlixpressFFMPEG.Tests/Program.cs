using FlixpressFFMPEG.Commands;
using FlixpressFFMPEG.Probe;
using System;
using System.Collections.Generic;

namespace FlixpressFFMPEG.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FilterComplexFlag filterComplexFlag = new FilterComplexFlag()
                .AddFilterComplexExpression(
                    new FilterComplexExpression()
                        .AddInputIdentifier("1")
                        .AddFilter(
                            new Filter()
                                .SetName("setpts")
                                .SetValue("PTS+5/TB")
                        )
                        .SetOutputIdentifier("top")
                )
                .AddFilterComplexExpression(
                    new FilterComplexExpression()
                        .AddInputIdentifier("0:v")
                        .AddInputIdentifier("top")
                        .AddFilter(
                            new Filter()
                                .SetName("overlay")
                                .AddAttribute("enable","'between(t,5,10)'")
                        )
                );

            /*
            FFMPEGCommand fFMPEGCommand = new FFMPEGCommand("ffmpegnew")
                .AddInput(@"D:\Videos\subg.mp4")
                .AddInput(@"D:\Videos\tnt-te.mp4")
                .SetFilterComplexFlag(filterComplexFlag)
                .SetOutput(@"D:\Videos\subg-imposed.mpr");
                */
            /*
            SuperImposeCommand superImposeFFMPEGCommand = new SuperImposeCommand("ffmpegnew")
                .SetBaseVideoPath(@"D:\Videos\subg.mp4")
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 5, 5)
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 15, 5)
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 20, 5)
                .SetOutput(@"D:\Videos\subg-imposed-3.mp4");

            ConcatenateCommand concatenateCommand = new ConcatenateCommand("ffmpegnew")
                .AddFiles(new List<string>
                {
                    @"D:\Videos\tnt-te.mp4",
                    @"D:\Videos\tnt-te.mp4",
                    @"D:\Videos\tnt-te.mp4"
                })
                .SetOutput(@"D:\Videos\tnt-concat.mp4");
            */
            /*
            VideoAudioSyncCommand videoAudioSyncCommand = new VideoAudioSyncCommand(@"C:\tools\ffmpegnew.exe")
                .SetVideoPath(@"D:\Videos\offsync.mp4")
                .SetOffsetAdjustment(-0.4)
                .SetOutput(@"D:\Videos\synched.mp4");
            
            FFMPEGExecutor.Execute(videoAudioSyncCommand);            

            string commandString = videoAudioSyncCommand.WritePart();
            */

            Dimension dimension = FFProbeTools.GetDimensions(@"c:\tools\ffprobe.exe", @"C:\Users\Iz\Pictures\Downloads\celtics.jpg");
            Dimension thirded = Dimension.ScaleToWidth(dimension, 1080);
            Console.ReadLine();
        }
    }
}

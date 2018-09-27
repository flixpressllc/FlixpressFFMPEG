using FlixpressFFMPEG.Commands;
using FlixpressFFMPEG.Common;
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

            /*
            FFMPEGCommand fFMPEGCommand = new FFMPEGCommand("ffmpegnew")
                .AddInput(@"D:\Videos\subg.mp4")
                .AddInput(@"D:\Videos\tnt-te.mp4")
                .SetFilterComplexFlag(filterComplexFlag)
                .SetOutput(@"D:\Videos\subg-imposed.mpr");
                */

            /*
            SuperImposeCommand superImposeFFMPEGCommand = new SuperImposeCommand(@"C:\tools\ffmpegnew.exe")
                .SetBaseVideoPath(@"D:\Videos\subg-short.mp4")
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 5, 5, new Coordinate(100,100))
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 15, 5, new Coordinate(200,200))
                .AddOverlayVideo(@"D:\Videos\tnt-te.mp4", 20, 5)
                .SetOutput(@"D:\Videos\subg-imposed-coords-1.mp4");
            */
            /*
            SuperImposeCommand superImposeFFMPEGCommand = new SuperImposeCommand(@"C:\tools\ffmpegnew.exe")
               .SetBaseVideoPath(@"D:\Videos\subg-short.mp4")
               .AddOverlayVideo(@"D:\Videos\banner_4.jpg", 0, 5)
               .AddOverlayVideo(@"D:\Videos\banner_2.jpg", 5, 5)
               .AddOverlayVideo(@"D:\Videos\banner_3.jpg", 10, 5)
               .AddOverlayVideo(@"D:\Videos\banner_1.jpg", 15, 5)
               .SetOutput(@"D:\Videos\subg-imposed-banners1.mp4");

            //string command = superImposeFFMPEGCommand.WritePart();

            FFMPEGExecutor.Execute(superImposeFFMPEGCommand);
            */
            var dummy = 9;
            
            /*
            ConcatenateCommand concatenateCommand = new ConcatenateCommand("ffmpegnew")
                .AddFiles(new List<string>
                {
                    @"D:\Videos\tnt-te.mp4",
                    @"D:\Videos\tnt-te.mp4",
                    @"D:\Videos\tnt-te.mp4"
                })
                .SetOutput(@"D:\Videos\tnt-concat.mp4");
            */
            
            VideoAudioSyncCommand videoAudioSyncCommand = new VideoAudioSyncCommand(@"C:\tools\ffmpegnew.exe")
                .SetVideoPath(@"D:\Videos\offsync.mp4")
                .SetOffsetAdjustment(-0.4)
                .SetOutput(@"D:\Videos\synched.mp4");
            
            FFMPEGExecutor.Execute(videoAudioSyncCommand);            
            
            //string commandString = videoAudioSyncCommand.WritePart();            
            
            //Dimension dimension = FFProbeTools.GetDimensions(@"c:\tools\ffprobe.exe", @"C:\Users\Iz\Pictures\Downloads\celtics.jpg");
            //Dimension thirded = Dimension.ScaleToWidth(dimension, 1080);
            Console.ReadLine();
        }
    }
}

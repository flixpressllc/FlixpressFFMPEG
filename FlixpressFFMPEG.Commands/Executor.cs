﻿using System.Diagnostics;

namespace FlixpressFFMPEG.Commands
{
    public static class Executor
    {
        public static void Execute(FFMPEGCommand fFMPEGCommand)
        {
            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = fFMPEGCommand.WriteExecutePath();
            ffmpeg.StartInfo.Arguments = fFMPEGCommand.WriteArguments();
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.StartInfo.RedirectStandardError = true;
            ffmpeg.Start();
            ffmpeg.WaitForExit();
        }

        public static void Execute<TFilterComplexCommand>(TFilterComplexCommand filterComplexCommand)
            where TFilterComplexCommand : FilterComplexCommandBase
        {
            Execute(filterComplexCommand.GetFFMPEGCommand());
        }
    }
}
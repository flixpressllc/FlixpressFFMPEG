using System.Diagnostics;
using System.IO;

namespace FlixpressFFMPEG.Commands
{
    public static class FFMPEGExecutor
    {
        public static void Execute(FFMPEGCommand fFMPEGCommand)
        {
            using (Process ffmpeg = new Process())
            {
                ffmpeg.StartInfo.FileName = fFMPEGCommand.WriteExecutePath();
                ffmpeg.StartInfo.WorkingDirectory = Path.GetDirectoryName(ffmpeg.StartInfo.FileName);
                ffmpeg.StartInfo.UseShellExecute = false;

                ffmpeg.StartInfo.Arguments = fFMPEGCommand.WriteArguments();                
                //ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.Start();
                ffmpeg.WaitForExit();
            }
        }

        public static void Execute<TCommand>(TCommand filterComplexCommand)
            where TCommand : CommandBase, ISelfWriter
        {
            filterComplexCommand.WritePart();
            Execute(filterComplexCommand.GetFFMPEGCommand());
        }
    }
}

using System.Diagnostics;
using System.IO;

namespace FlixpressFFMPEG.Probe
{
    public static class FFProbeExecutor
    {
        public static string Execute(string ffProbePath, string commandString)
        {
            using (Process ffmpeg = new Process())
            {
                ffmpeg.StartInfo.FileName = ffProbePath;
                ffmpeg.StartInfo.WorkingDirectory = Path.GetDirectoryName(ffProbePath);
                ffmpeg.StartInfo.UseShellExecute = false;

                ffmpeg.StartInfo.Arguments = commandString;
                ffmpeg.StartInfo.RedirectStandardOutput = true;
                ffmpeg.Start();
                string output = ffmpeg.StandardOutput.ReadToEnd().Trim();
                ffmpeg.WaitForExit();

                return output;
            }
        }
    }
}

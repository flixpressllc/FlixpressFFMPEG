using FlixpressFFMPEG.Common;
using System;

namespace FlixpressFFMPEG.Probe
{
    public static class FFProbeTools
    {
        public static double GetVideoDuration(string ffProbePath, string inputLocalPath)
        {
            return Convert.ToDouble(FFProbeExecutor.Execute(ffProbePath, "-i " + inputLocalPath + " -show_entries format=duration -v quiet -of csv=\"p=0\""));
        }

        public static Dimension GetDimensions(string ffProbePath, string inputLocalPath)
        {
            string readout = FFProbeExecutor.Execute(ffProbePath, "-i " + inputLocalPath + " -v error -show_entries stream=width,height -of csv=s=x:p=0");

            try
            {
                string[] dimensions = readout.Split('x');
                return new Dimension(Convert.ToInt32(dimensions[0]), Convert.ToInt32(dimensions[1]));

            }
            catch (Exception)
            {
                return new Dimension();
            }
        }
    }
}

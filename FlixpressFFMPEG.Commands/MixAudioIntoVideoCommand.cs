using System;
using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class MixAudioIntoVideoCommand : CommandBase, ISelfWriter
    {
        private string VideoPath { get; set; }
        private string AudioPath { get; set; }

        public MixAudioIntoVideoCommand(string executePath) : base(executePath)
        {

        }

        public MixAudioIntoVideoCommand SetVideoPath(string videoPath)
        {
            VideoPath = videoPath;
            return this;
        }

        public MixAudioIntoVideoCommand SetAudioPath(string audioPath)
        {
            AudioPath = audioPath;
            return this;
        }

        public MixAudioIntoVideoCommand SetOutput(string output)
        {
            FFMPEGCommand.SetOutput(output);
            return this;
        }


        public string WritePart()
        {
            FFMPEGCommand.AddInput(VideoPath);
            FFMPEGCommand.AddInput(AudioPath);

            FFMPEGCommand.AddFlag(new SimpleFlag("filter_complex", "[0:a][1:a]amix=duration=first[aout]"));
            
            
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "0:v"));
            FFMPEGCommand.AddFlag(new SimpleFlag("map", "[aout]"));       
            
            FFMPEGCommand.AddFlag(new SimpleFlag("c:v", "copy"));
            
            FFMPEGCommand.AddFlag(new SimpleFlag("y", null));

            return FFMPEGCommand.WritePart();
        }
    }
}

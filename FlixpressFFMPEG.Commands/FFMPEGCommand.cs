﻿using System.Collections.Generic;
using System.Text;

namespace FlixpressFFMPEG.Commands
{
    public class FFMPEGCommand : ISelfWriter
    {
        public string ExecutePath { get; set; }
        public List<string> Inputs { get; set; }
        //public FilterComplexFlag FilterComplexFlag { get; set; }
        public List<IFlag> Flags { get; set; }
        public string Output { get; set; }

        public FFMPEGCommand()
        {
            Inputs = new List<string>();
            Flags = new List<IFlag>();
        }

        public FFMPEGCommand(string executePath) : this()
        {
            ExecutePath = executePath;
        }

        public FFMPEGCommand AddInput(string input)
        {
            Inputs.Add(input);
            return this;
        }

        public FFMPEGCommand AddFlag(IFlag flag)
        {
            Flags.Add(flag);
            return this;
        }

        /*
        public FFMPEGCommand SetFilterComplexFlag(FilterComplexFlag filterComplexFlag)
        {
            FilterComplexFlag = filterComplexFlag;
            return this;
        }
        */

        public FFMPEGCommand SetOutput(string output)
        {
            Output = output;
            return this;
        }

        public string WritePart()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{WriteExecutePath()} {WriteArguments()}");
            return sb.ToString();
        }

        public string WriteExecutePath()
        {
            return ExecutePath;
        }

        public string WriteArguments()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string input in Inputs)
            {
                sb.Append($"-i {input} ");
            }

            foreach(var flag in Flags)
            {
                sb.Append(flag.WritePart() + " ");
            }

            sb.Append($"{Output}");

            return sb.ToString();
        }
    }
}

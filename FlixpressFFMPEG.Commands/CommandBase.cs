namespace FlixpressFFMPEG.Commands
{
    public abstract class CommandBase
    {
        protected FFMPEGCommand FFMPEGCommand { get; set; }

        public CommandBase(string executePath)
        {
            FFMPEGCommand = new FFMPEGCommand(executePath);
        }

        internal FFMPEGCommand GetFFMPEGCommand()
        {
            return FFMPEGCommand;
        }
    }
}

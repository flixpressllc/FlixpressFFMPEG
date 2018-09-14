namespace FlixpressFFMPEG.Commands
{
    public abstract class FilterComplexCommandBase
    {
        protected FFMPEGCommand FFMPEGCommand { get; set; }

        public FilterComplexCommandBase(string executePath)
        {
            FFMPEGCommand = new FFMPEGCommand(executePath);
        }
    }
}

using AudioManager.Invokers;

namespace AudioManager.Library.Groups.Global
{
    public class SoundsAudioGroup : AudioGroupBase
    {
        public SoundsAudioGroup(IAudioEventReceiver audioEventReceiver) : base(audioEventReceiver)
        {
        }
        
        public IAudioEventInvoker Click()
        {
            const string events = "event:/sounds/random_click";
            return GetEvent(events);
        }
    }
}
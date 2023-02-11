using AudioManager.Invokers;

namespace AudioManager
{
    public interface IAudioEventReceiver : IAudioEventInvoker
    {
        void SetAudio(AudioName audioName);
    }
}
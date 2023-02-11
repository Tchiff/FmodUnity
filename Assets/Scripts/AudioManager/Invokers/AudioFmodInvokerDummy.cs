using System.Threading;
using Cysharp.Threading.Tasks;
using FMOD;

namespace AudioManager.Invokers
{
    public class AudioFmodInvokerDummy : IAudioEventInvoker
    {
        public void Play()
        {
        }

        public void Play(float xPos)
        {
        }

        public void Stop()
        {
        }

        public void Dispose()
        {
        }

        public GUID GetCurrentGuid()
        {
            return new GUID();
        }

        public bool IsEventPlaying(GUID eventGuid)
        {
            return false;
        }

        public bool IsEventPlaying()
        {
            return true;
        }

        public UniTask PlayAwaitable(CancellationToken cancellationToken, float getSoundSkinValue)
        {
            return UniTask.CompletedTask;
        }
    }
}
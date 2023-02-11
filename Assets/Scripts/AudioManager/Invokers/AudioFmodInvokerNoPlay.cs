using System.Threading;
using Cysharp.Threading.Tasks;
using FMOD;

namespace AudioManager.Invokers
{
    public class AudioFmodInvokerNoPlay : IAudioEventInvoker
    {
        private readonly IAudioEventInvoker _baseInvoker;

        public AudioFmodInvokerNoPlay(IAudioEventInvoker baseInvoker)
        {
            _baseInvoker = baseInvoker;
        }
        
        public void Play()
        {
            
        }

        public void Play(float xPos)
        {
            
        }

        public void Stop()
        {
            _baseInvoker.Stop();
        }

        public void Dispose()
        {
            _baseInvoker.Dispose();
        }

        public GUID GetCurrentGuid() => _baseInvoker.GetCurrentGuid();

        public bool IsEventPlaying(GUID eventGuid) => _baseInvoker.IsEventPlaying(eventGuid);
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
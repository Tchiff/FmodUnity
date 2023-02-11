using System.Threading;
using Cysharp.Threading.Tasks;
using FMOD;

namespace AudioManager.Invokers
{
    public interface IAudioEventInvoker
    {
        void Play();
        void Play(float xPos);
        void Stop();
        void Dispose();
        GUID GetCurrentGuid();
        bool IsEventPlaying(GUID eventGuid);
        bool IsEventPlaying();
        UniTask PlayAwaitable(CancellationToken cancellationToken, float getSoundSkinValue);
    }
}
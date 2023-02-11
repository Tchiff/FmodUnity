namespace Fmod.Events
{
    public interface IFmodEvent
    {
        void Start();
        void Start(float xPos);
        void Stop();
        void Dispose();
        bool IsPlaying();
    }
}
using FMOD;
using Fmod.Events;
using UnityEngine;

namespace Fmod
{
    public interface IFmodManager
    {
        GUID ConvertPathToGuid(string path);
        void StartEvent(GUID eventGuid);
        void StartEvent(GUID eventGuid, float xPos);
        void StopEvent(GUID eventGuid);
        void DisposeInstance(GUID eventGuid);
        void LoadBank(TextAsset bankAsset);
        bool HasBankLoaded(string bankName);
        bool IsStreamingAssetsLoaded();
        void SetVolumeBus(string busPath, float volume);
        float GetVolumeBus(string busPath);
        bool IsEventPlaying(GUID eventGuid);
        IFmodEvent GetEvent(GUID guid);
    }
}
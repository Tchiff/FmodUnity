using System;
using System.Collections.Generic;
using FMOD;
using Fmod.Events;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Fmod
{
    public class FmodManager : IFmodManager
    {
        private readonly Dictionary<GUID, IFmodEvent> _events = new Dictionary<GUID, IFmodEvent>();

        public GUID ConvertPathToGuid(string path)
        {
            try
            {
                return RuntimeManager.PathToGUID(path);
            }
            catch (EventNotFoundException e)
            {
                Debug.LogError(e.Message);
            }

            return default;
        }

        public void StartEvent(GUID eventGuid)
        {
            GetEvent(eventGuid).Start();
        }

        public void StartEvent(GUID eventGuid, float xPos)
        {
            GetEvent(eventGuid).Start(xPos);
        }

        public void StartSnapshot(GUID snapshotGuid)
        {
            GetSnapshot(snapshotGuid).Start();
        }

        public void StopEvent(GUID eventGuid)
        {
            if (TryGetEvent(eventGuid, out IFmodEvent fmodEvent))
            {
                fmodEvent.Stop();
            }
        }

        public void StopSnapshot(GUID snapshotGuid) => StopEvent(snapshotGuid);

        public void DisposeInstance(GUID eventGuid)
        {
            if (TryGetEvent(eventGuid, out IFmodEvent fmodEvent))
            {
                fmodEvent.Dispose();
            }
        }

        public bool IsEventPlaying(GUID eventGuid)
        {
	        if (TryGetEvent(eventGuid, out IFmodEvent fmodEvent))
	        {
		        return fmodEvent.IsPlaying();
	        }
	        return false;
        }

        public void LoadBank(TextAsset bankAsset)
        {
            RuntimeManager.LoadBank(bankAsset);
        }

        public bool HasBankLoaded(string bankName)
        {
            return RuntimeManager.HasBankLoaded(bankName);
        }

        public bool IsStreamingAssetsLoaded()
        {
            return RuntimeManager.HaveAllBanksLoaded;
        }

        public void SetVolumeBus(string busPath, float volume)
        {
            try
            {
                var bus = RuntimeManager.GetBus(busPath);
                bus.setVolume(volume);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}\n{e.StackTrace}");
            }
        }
        
        public float GetVolumeBus(string busPath)
        {
	        float volume = 0f;
	        try
	        {
		        var bus = RuntimeManager.GetBus(busPath);
		        bus.getVolume(out volume);
	        }
	        catch (Exception e)
	        {
		        Debug.LogError($"{e.Message}\n{e.StackTrace}");
	        }
	        return volume;
        }

        public IFmodEvent GetEvent(GUID guid)
        {
            if (!_events.ContainsKey(guid))
            {
                _events[guid] = new InstanceFmodEvent(guid);
            }
            return _events[guid];
        }
        
        private IFmodEvent GetSnapshot(GUID guid)
        {
            if (!_events.ContainsKey(guid))
            {
                _events[guid] = new InstanceFmodSnapshot(guid);
            }
            return _events[guid];
        }

        private bool TryGetEvent(GUID guid, out IFmodEvent fmodEvent)
        {
            if (_events.ContainsKey(guid))
            {
                fmodEvent = _events[guid];
                return true;
            }
            
            fmodEvent = null;
            return false;
        }
    }
}
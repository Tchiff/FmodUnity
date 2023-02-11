using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Fmod.Events
{
    public class InstanceFmodEvent : IFmodEvent
    {
        private readonly List<EventInstance> _instances = new List<EventInstance>();
        private EventDescription _description;
        private GUID _guid;

        private const int POOL_SIZE = 5;

        public InstanceFmodEvent(GUID eventGuid)
        {
            _guid = eventGuid;
        }

        public void Start()
        {
	        bool hasFree = TryGetFreeInstance(out var instance);
	        if (!hasFree)
            {
	            instance = CreateInstance();
            }
            
            instance.start();
            
            if (_instances.Count >= POOL_SIZE && !hasFree)
            {
	            instance.release();
            }
            else if (!hasFree)
            {
	            _instances.Add(instance);
            }
        }

		public void Start(float xPos)
		{
			bool hasFree = TryGetFreeInstance(out var instance);
			if (!hasFree)
			{
				instance = CreateInstance();
			}
			
			instance.set3DAttributes(new Vector3(xPos, 0,0).To3DAttributes());
			instance.start();
            
			if (_instances.Count >= POOL_SIZE && !hasFree)
			{
				instance.release();
			}
			else if (!hasFree)
			{
				_instances.Add(instance);
			}
		}

		public void Stop()
        {
	        _instances.ForEach(i=>i.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT));
        }

        public void Dispose()
        {
	        _instances.ForEach(i=>
	        {
		        i.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		        i.release();
	        });
	        _instances.Clear();
        }

        public bool IsPlaying()
        {
	        return _instances.Any(IsPlaying);
        }
        
        private EventInstance CreateInstance()
        {
	        return RuntimeManager.CreateInstance(_guid);
        }

        private bool TryGetFreeInstance(out EventInstance freeInstance)
        {
	        foreach (var instance in _instances)
	        {
		        if (!IsPlaying(instance))
		        {
			        freeInstance = instance;
			        return true;
		        }
	        }

	        freeInstance = default;
	        return false;
        }

        private bool IsPlaying(EventInstance instance)
        {
	        instance.getPlaybackState(out PLAYBACK_STATE state); 
	        return state != PLAYBACK_STATE.STOPPED && state != PLAYBACK_STATE.STOPPING;
        }
    }
}
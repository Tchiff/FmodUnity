using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Fmod.Events
{
    public class InstanceFmodSnapshot : IFmodEvent
    {
        private EventDescription _description;
        private GUID _guid;
        private EventInstance _instance;

        public InstanceFmodSnapshot(GUID eventGuid)
        {
            _guid = eventGuid;
            _instance = CreateInstance();
        }

        public void Start()
        {
	        _instance.start();
        }

		public void Start(float xPos)
		{
			_instance.set3DAttributes(new Vector3(xPos, 0,0).To3DAttributes());
			Start();
		}

		public void Stop()
        {
	        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void Dispose()
        {
	        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	        _instance.release();
        }

        public bool IsPlaying()
        {
	        _instance.getPlaybackState(out PLAYBACK_STATE state); 
	        return state != PLAYBACK_STATE.STOPPED;
        }
        
        private EventInstance CreateInstance()
        {
	        return RuntimeManager.CreateInstance(_guid);
        }
    }
}
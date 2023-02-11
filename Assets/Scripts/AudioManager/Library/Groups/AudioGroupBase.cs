using System.Collections.Generic;
using AudioManager.Invokers;

namespace AudioManager.Library.Groups
{
    public abstract class AudioGroupBase
    {
	    private readonly List<AudioGroupBase> _children = new List<AudioGroupBase>();
        private readonly HashSet<AudioName> _usedEvents = new HashSet<AudioName>();
        
        protected readonly IAudioEventReceiver AudioEventReceiver;

        protected AudioGroupBase(IAudioEventReceiver audioEventReceiver)
        {
            AudioEventReceiver = audioEventReceiver;
        }

        public void Dispose()
        {
            foreach (var usedEvent in _usedEvents)
            {
                AudioEventReceiver.SetAudio(usedEvent);
                AudioEventReceiver.Dispose();
            }
            foreach (var child in _children)
            {
	            child.Dispose();
            }
        }

        protected void AddChild(params AudioGroupBase[] child)
        {
	        _children.AddRange(child);
        }
        
        protected IAudioEventInvoker GetEvent(string events, bool repeatCheck = false)
        {
	        AddedEventToUsed(events);
            
	        if (repeatCheck && AudioEventReceiver.IsEventPlaying(AudioEventReceiver.GetCurrentGuid()))
	        {	
		        return new AudioFmodInvokerNoPlay(AudioEventReceiver);
	        }

	        return AudioEventReceiver;
        }
        
        protected IAudioEventInvoker GetSnapshot(string events)
        {
	        AddedEventToUsed(events);
	        return AudioEventReceiver;
        }
        private void AddedEventToUsed(string eventsName)
        {
	        var audioName = new AudioName { Name = eventsName, IsPath = false };
            
	        _usedEvents.Add(audioName);
            
	        AudioEventReceiver.SetAudio(audioName);
        }
    }
}


using AudioManager.Library.Groups.Global;

namespace AudioManager.Library
{
    public class AudioLibrary
    {
	    public GlobalAudioGroup GlobalAudioGroup { get; }

        public AudioLibrary(IAudioEventReceiver audioEventReceiver)
        {
	        GlobalAudioGroup = new GlobalAudioGroup(audioEventReceiver);
        }

		public void DisposeAllGroups()
		{
			GlobalAudioGroup.Dispose();
		}
	}
}
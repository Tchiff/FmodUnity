namespace AudioManager.Library.Groups.Global
{
	public class GlobalAudioGroup : AudioGroupBase
    {
	    public MusicAudioGroup Music { get; }
	    public SoundsAudioGroup Sounds { get; }
	    
	    public GlobalAudioGroup(IAudioEventReceiver audioEventReceiver) 
            : base(audioEventReceiver)
        {
	        Music = new MusicAudioGroup(audioEventReceiver);
	        Sounds = new SoundsAudioGroup(audioEventReceiver);
	        
	        AddChild(
		        Music,
		        Sounds
	        );
        }
    }
}
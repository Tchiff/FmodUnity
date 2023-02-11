using System.Collections.Generic;
using AudioManager.Invokers;

namespace AudioManager.Library.Groups.Global
{
    public class MusicAudioGroup : AudioGroupBase
    {
        private readonly List<string> _music = new()
        {
            "event:/music/music_1",
            "event:/music/music_2"
        };
        
        public MusicAudioGroup(IAudioEventReceiver audioEventReceiver) : base(audioEventReceiver)
        {
        }
        
        public IAudioEventInvoker MainMenuMusic(int index)
        {
            if (index > _music.Count || index < 0)
            {
                return new AudioFmodInvokerDummy();
            }

            return GetEvent(_music[index], true);
        }
    }
}
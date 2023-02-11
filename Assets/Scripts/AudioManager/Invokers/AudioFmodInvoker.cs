using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fmod;
using FMOD;
using FMODUnity;
using Debug = UnityEngine.Debug;

namespace AudioManager.Invokers
{
    public class AudioFmodInvoker : IAudioEventReceiver
    {
        private readonly IFmodManager _fmodManager;

        private GUID _guid;
        private AudioName _audioName;

        public AudioFmodInvoker(IFmodManager fmodManager)
        {
            _fmodManager = fmodManager;
        }

        public void SetAudio(AudioName audioName)
        {
            _audioName = audioName;
            _guid = _fmodManager.ConvertPathToGuid(_audioName.Name);
        }

        public void Play()
        {
	        try
	        {
#if SHOW_LOGS
                Debug.Log($"[FMOD] Play: {_audioName.Name}");
#endif
		        _fmodManager.StartEvent(_guid);
	        }
	        catch (EventNotFoundException e)
	        {
#if UNITY_EDITOR || DEV
		        Debug.LogError($"Cannot play event {_audioName.Name}!\nEventNotFoundException:{e.Message}\n{e.StackTrace}");
#endif
	        }
        }

        public void Play(float xPos)
        {
            try
            {
#if SHOW_LOGS
                Debug.Log($"[FMOD] PlayStereo: {_audioName.Name} with value: {xPos}");
#endif
                _fmodManager.StartEvent(_guid, xPos);
            }
            catch (EventNotFoundException e)
            {
#if UNITY_EDITOR || DEV
                Debug.LogError($"Cannot play stereo event {_audioName.Name}!\nEventNotFoundException:{e.Message}\n{e.StackTrace}");
#endif
            }
		}

        public async UniTask PlayAwaitable(CancellationToken cancellationToken, float getSoundSkinValue)
        {
            try
            {
                var fmodEvent = _fmodManager.GetEvent(_guid);
                fmodEvent.Start(getSoundSkinValue);
                await UniTask.WaitUntil(() => !fmodEvent.IsPlaying(), cancellationToken : cancellationToken);
            }
            catch (EventNotFoundException e)
            {
#if UNITY_EDITOR || DEV
                Debug.LogError($"Cannot play event {_audioName.Name}!\nEventNotFoundException:{e.Message}\n{e.StackTrace}");
#endif
            }
            catch (OperationCanceledException e)
            {
#if UNITY_EDITOR || DEV
                Debug.Log($"Fmod event play canceled: {_audioName.Name}!\nOperationCanceledException:{e.Message}\n{e.StackTrace}");
#endif
            }
            catch (Exception e)
            {
#if UNITY_EDITOR || DEV
                Debug.LogError($"Play event exception {_audioName.Name}!\n Exception:{e.Message}\n{e.StackTrace}");
#endif
            }
        }

        public void Stop()
        {
#if SHOW_LOGS
			Debug.Log($"[FMOD] Stop: {_audioName.Name}");
#endif
            _fmodManager.StopEvent(_guid);
        }

        public void Dispose()
        {
            _fmodManager.DisposeInstance(_guid);
        }

        public GUID GetCurrentGuid() => _guid;

        public bool IsEventPlaying(GUID eventGuid) => _fmodManager.IsEventPlaying(eventGuid);
        public bool IsEventPlaying() => _fmodManager.IsEventPlaying(GetCurrentGuid());
    }
}
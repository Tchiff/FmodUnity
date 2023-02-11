using System;
using System.Collections.Generic;
using System.Threading;
using AudioManager.Invokers;
using AudioManager.Library;
using Cysharp.Threading.Tasks;
using Fmod;
using UnityEngine;

namespace AudioManager
{
	public class AudioManagerFmod : Singleton<IAudioManager,AudioManagerFmod>, IAudioManager
	{
		private readonly IFmodManager _fmodManager;
		private readonly AudioLibrary _audioLibrary;
		
		private readonly HashSet<string> _banks = new HashSet<string>();
		private readonly Dictionary<VolumeType, string> _buses = new Dictionary<VolumeType, string>
		{
			{VolumeType.Music, "bus:/MainMixMusic"},
			{VolumeType.SFX, "bus:/MainMixSFX"}
		};
		
		public const float DEFAULT_VOLUME = 0.75f;
		public const string PREFS_KEY_VOLUME = "VOLUME_";

		public AudioManagerFmod()
		{
			_fmodManager = new FmodManager();
			_audioLibrary = new AudioLibrary(new AudioFmodInvoker(_fmodManager));
		}

		public async UniTask LoadBank(string name, TextAsset asset)
		{
			if (asset == null)
			{
				Debug.LogError($"LoadBank failed! Asset '{name}' is NULL!");
				return;
			}
			if (_fmodManager.HasBankLoaded(name))
			{
				return;
			}
			
			if (!_banks.Contains(name))
			{
				_banks.Add(name);
			}

			var cancellationSource = new CancellationTokenSource();

			try
			{
				_fmodManager.LoadBank(asset);
			}
			catch (Exception e)
			{
				_banks.Remove(name);
				cancellationSource.Cancel();
				cancellationSource.Dispose();
				Debug.LogError($"Cannot load bank {name}!");
#if UNITY_EDITOR || DEV
				throw new ArgumentException($"Cannot load bank {name}!");
#endif
			}
			await UniTask.WaitUntil(() => _fmodManager.HasBankLoaded(name), cancellationToken: cancellationSource.Token);

#if UNITY_EDITOR || DEV
			Debug.Log($"[FMOD] bank '{name}' Loaded!");
#endif
		}

		public void SetVolume(VolumeType volumeType, float volume)
		{
			string path = _buses[volumeType];
			_fmodManager.SetVolumeBus(path, volume);
		}

		public void SetVolumeAndSave(VolumeType volumeType, float volume)
		{
			SetVolume(volumeType, volume);
			SaveVolume(volumeType);
		}

		public float GetVolume(VolumeType volumeType)
		{
			string path = _buses[volumeType];
			return _fmodManager.GetVolumeBus(path);
		}

		public AudioLibrary GetAudioLibrary() => _audioLibrary;
		
		public bool IsAllBanksLoaded()
		{
			foreach (var bankName in _banks)
			{
				if (!_fmodManager.HasBankLoaded(bankName))
				{
					return false;
				}
			}
			return _fmodManager.IsStreamingAssetsLoaded();
		}
		
		public bool IsBankLoaded(string bundleName)
		{
			return _banks.Contains(bundleName);
		}

		public void LoadVolume()
		{
			float musVolume = PlayerPrefs.GetFloat(GetPrefsKeyVolume(VolumeType.Music), DEFAULT_VOLUME);
			SetVolume(VolumeType.Music,musVolume);
			float sfxVolume = PlayerPrefs.GetFloat(GetPrefsKeyVolume(VolumeType.SFX), DEFAULT_VOLUME);
			SetVolume(VolumeType.SFX,sfxVolume);
		}
		
		private void SaveVolume(VolumeType volumeType)
		{
			PlayerPrefs.SetFloat(GetPrefsKeyVolume(volumeType), GetVolume(volumeType));
		}

		private string GetPrefsKeyVolume(VolumeType type)
		{
			return $"{PREFS_KEY_VOLUME}{type}";
		}
	}
}
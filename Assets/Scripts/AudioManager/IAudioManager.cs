using AudioManager.Library;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AudioManager
{
	public interface IAudioManager
	{
		UniTask LoadBank(string name, TextAsset asset);
		void SetVolumeAndSave(VolumeType volumeType, float volume);
		void SetVolume(VolumeType volumeType, float volume);
		float GetVolume(VolumeType volumeType);
		AudioLibrary GetAudioLibrary();
		bool IsBankLoaded(string bundleName);
		bool IsAllBanksLoaded();
		void LoadVolume();
	}
}
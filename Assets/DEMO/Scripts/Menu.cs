using System.Collections.Generic;
using AudioManager;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private SimpleButton _firstMusicButton;
    [SerializeField] private SimpleButton _fecondMusicButton;
    [Header("Mute")]
    [SerializeField] private SimpleButton _muteMusicButton;
    [SerializeField] private SimpleButton _muteSoundsButton;

    private List<SimpleButton> _groupButtons;
    private int _lastMusic = -1;

    private void Awake()
    {
        _firstMusicButton.OnClick = () => SetPressedGroupButton(0);
        _fecondMusicButton.OnClick = () => SetPressedGroupButton(1);
        _muteSoundsButton.OnClick = () => SetMute(VolumeType.SFX);
        _muteMusicButton.OnClick = () => SetMute(VolumeType.Music);
        
        _groupButtons = new()
        {
            _firstMusicButton,
            _fecondMusicButton
        };
    }

    public void Initialize()
    {
        PlayMusic(0);
        _firstMusicButton.SetPressed(true);
        _fecondMusicButton.SetPressed(false);
        _muteSoundsButton.SetPressed(!IsMute(VolumeType.SFX));
        _muteMusicButton.SetPressed(!IsMute(VolumeType.Music));
    }

    private void PlayMusic(int index)
    {
        if (_lastMusic >= 0)
        {
            AudioManagerFmod.Instance.GetAudioLibrary().GlobalAudioGroup.Music.MainMenuMusic(_lastMusic).Stop();
        }
        
        _lastMusic = index;
        AudioManagerFmod.Instance.GetAudioLibrary().GlobalAudioGroup.Music.MainMenuMusic(index).Play();
    }
    
    private void SetMute(VolumeType volumeType)
    {
        float volume = IsMute(volumeType) ? 1f : 0f;
        AudioManagerFmod.Instance.SetVolumeAndSave(volumeType, volume);
    }

    private bool IsMute(VolumeType volumeType)
    {
        return AudioManagerFmod.Instance.GetVolume(volumeType) <= 0;
    }

    private void SetPressedGroupButton(int index)
    {
        for (int i = 0; i < _groupButtons.Count; i++)
        {
            bool isPressed = index == i;
            _groupButtons[i].SetPressed(isPressed);
        }
        PlayMusic(index);
    }
}

using System;
using AudioManager;
using UnityEngine;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private Color _unpressedColor;
    [SerializeField] private bool _isToggle;

    public bool IsPressed { get; private set; }
    public Action OnClick;

    private void Awake()
    {
        _button.onClick.AddListener(HandleClick);
    }

    public void SetPressed(bool isPressed)
    {
        IsPressed = isPressed;
        _button.targetGraphic.color = isPressed ? _pressedColor : _unpressedColor;
    }
    
    private void HandleClick()
    {
        if (_isToggle || !IsPressed)
        {
            SetPressed(!IsPressed);
            OnClick?.Invoke();
        }
        AudioManagerFmod.Instance.GetAudioLibrary().GlobalAudioGroup.Sounds.Click().Play();
    }
}

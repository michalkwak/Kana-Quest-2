using UnityEngine;
using UnityEngine.UI;

public class MusicMute : MonoBehaviour
{
    public Button muteButton;
    public Sprite unmutedSprite;
    public Sprite mutedSprite;

    private bool isMuted = false;

    private const string muteKey = "IsMuted";

    void Start()
    {
        // Load mute state from PlayerPrefs
        isMuted = PlayerPrefs.GetInt(muteKey, 0) == 1;

        // Set initial button sprite
        UpdateButtonSprite();

        // Add listener to the mute button
        muteButton.onClick.AddListener(ToggleMute);
    }

    void ToggleMute()
    {
        // Toggle mute state
        isMuted = !isMuted;

        // Save mute state to PlayerPrefs
        PlayerPrefs.SetInt(muteKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        // Use AudioManager to set music volume
        float targetVolume = isMuted ? -80f : 0f;
        AudioManager.Instance.SetMusicVolume(targetVolume);

        // Update button sprite based on mute state
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        Image buttonImage = muteButton.GetComponent<Image>();
        buttonImage.sprite = isMuted ? mutedSprite : unmutedSprite;
    }
}

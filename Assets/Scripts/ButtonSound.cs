using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public string soundName = "Button Click 2"; // Set this to the name of the sound you want to play

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }
    }

    private void PlayButtonSound()
    {
        // Assuming AudioManager is a singleton or accessible in some way
        AudioManager.Instance.Play(soundName);
    }

    
}

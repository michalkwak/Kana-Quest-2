using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Image targetImage; // Drag your Image component here in the Unity Editor

    private void Start()
    {
        // Set the initial visibility state of the image
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);

        }
    }

    public void ToggleVisibility()
    {
        if (targetImage != null)
        {
            // Toggle the visibility state of the image
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
        }
    }
}

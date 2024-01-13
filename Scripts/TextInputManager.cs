using TMPro;
using UnityEngine;

public class TextInputManager : MonoBehaviour
{
    public TMP_InputField inputField; 
    public TMP_Text displayText;

    public void UpdateText()
    {
        if (displayText != null && inputField != null)
        {
            displayText.text = inputField.text;
        }
    }
}

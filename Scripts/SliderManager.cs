using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public Slider slider;
    public TMP_Text metin;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        metin.text = slider.value.ToString("F1");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ToggleDescription : MonoBehaviour
{
    public List<string> InfoTexts;
    public TextMeshProUGUI InfoText;
    public GameObject InfoBox;

    private void Start()
    {
        InfoBox.SetActive(false);
    }

    public void ClickToggle(int toggle)
    {
        InfoBox.SetActive(true);
        InfoText.text = InfoTexts[toggle];
    }

    public void CloseBox() => InfoBox.SetActive(false);
}

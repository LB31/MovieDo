using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieSelection : MonoBehaviour
{
    public int languageSelected = 3;
    public GameObject descriptionText;
    public Text text;
      
    public void changeLanguage(int number)
    {
        languageSelected = number;
    }

    public void ElectronicOnClick()
    {
        switch (languageSelected)
        {
            case 1:
                descriptionText.SetActive(true);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}

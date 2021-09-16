using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject UKR;
    public GameObject RUS;
    public GameObject ENG;
    public GameObject GER;

    int screenState = 0;
    public GameObject description;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectLanguage()
    {
        
    }

    void showDescription()
    {
        if(screenState%4 != 0) 
        {
            description.SetActive(true);
        } 
        else
        {
            description.SetActive(false);
        }
    }
}

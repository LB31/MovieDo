using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Dialog CharacterDialog;
    public GameObject Dialogbox;
    public TextMeshProUGUI DialogText;

    public int CurrentDialog;

    private void Start()
    {
        ToggleDialogBox(false);
        DialogText.text = CharacterDialog.Dialogs[CurrentDialog].Text;
    }

    private void OnMouseDown()
    {
        ToggleDialogBox(true);
    }

    private void ToggleDialogBox(bool activate)
    {
        Dialogbox.SetActive(activate);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Dialog CharacterDialog;

    public GameObject Dialogbox;
    public TextMeshProUGUI DialogText;

    public int CurrentDialog;
    public SceneStateBush CurrentState;

    public Animator Animator;

    private bool rotatingToBush;
    private bool walkingToBush;

    public Transform WalkGoal;

    public DragController DragBook;
    public GameObject BookToRead;
    private bool rotateBack;

    private void Start()
    {
        ToggleDialogBox(false);
        DialogText.text = CharacterDialog.Dialogs[CurrentDialog].Text;

        CurrentState = SceneStateBush.Idle;

        DragBook.enabled = false;
        BookToRead.SetActive(false);
    }

    private void OnMouseDown()
    {
        switch (CurrentState)
        {
            case SceneStateBush.Idle:
                Animator.SetBool("RotateToBush", true);
                CurrentDialog--;
                ChangeDialog();
                rotatingToBush = true;
                CurrentState = SceneStateBush.GoToBush;
                break;
            case SceneStateBush.JumpOverBush:
                ChangeDialog();

                break;


            case SceneStateBush.GoToBush:

                break;
            default:
                break;
        }


        ToggleDialogBox(true);
    }

    internal async void ReadBook()
    {
        Animator.SetBool("GotBook", true);
        BookToRead.SetActive(true);
        await Task.Delay(1500);
        rotateBack = true;
    }

    private void ToggleDialogBox(bool activate)
    {
        Dialogbox.SetActive(activate);
    }

    private void ChangeDialog()
    {
        CurrentDialog++;
        if (CurrentDialog < CharacterDialog.Dialogs.Count)
            DialogText.text = CharacterDialog.Dialogs[CurrentDialog].Text;
    }

    private void Update()
    {
        if (rotatingToBush){
            bool readyToJump = AnimatorIsPlaying("PerepareToWalk");
            if (readyToJump)
            {
                Animator.SetBool("GoToBush", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rotatingToBush = false;
                walkingToBush = true;
            }
        }

        if (walkingToBush)
        {
            transform.position = Vector3.MoveTowards(transform.position, WalkGoal.position, Time.deltaTime);
            if (Vector3.Distance(transform.position, WalkGoal.position) < 0.2f)
            {
                ChangeDialog();
                walkingToBush = false;
            }
                
            DragBook.enabled = true;


        }

        if (rotateBack)
        {
            bool readyToRotate = !AnimatorIsPlaying("Looking Through Files Low");
            if (readyToRotate)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rotateBack = false;
                ChangeDialog();
                CurrentState = SceneStateBush.JumpOverBush;
            }
        }
    }


    private bool AnimatorIsPlaying(string stateName)
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}

public enum SceneStateBush
{
    Idle,
    GoToBush,
    SearchOnGround,
    ReadBook,
    JumpOverBush,
    ReterunToUser,
    ShowVideo
}
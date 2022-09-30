using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    public PlayerMovementTutorial playerMovementTutorial;
    public static float TheDistance;
    public GameObject ActionText;
    public GameObject Door;
    public GameObject GuideArrow;
    public GameObject ExtraCross;

    public bool isLocked = false;
    Animator animator;



    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        animator = GetComponent<Animator>();
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2 && isLocked == false || isLocked == true && playerMovementTutorial.doorKey > 0)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO OPEN THE DOOR";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action") && isLocked == false)
        {
            if (TheDistance <= 2)
            {
                animator.SetBool("Open", true);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);

            }
        }

        //locked

        if (TheDistance <= 2 && isLocked == true && playerMovementTutorial.doorKey == 0)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "DOOR IS LOCKED";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action") && isLocked == true)
        {
            if (TheDistance <= 2 && playerMovementTutorial.doorKey > 0)
            {
                animator.SetBool("Open", true);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                playerMovementTutorial.doorKey -= 1;
            }
        }
    }


    void OnMouseExit()
    {
        ActionText.SetActive(false);
        ExtraCross.SetActive(false);
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
}

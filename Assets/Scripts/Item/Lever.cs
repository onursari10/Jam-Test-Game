using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public static float TheDistance;
    public GameObject ActionText;
    public GameObject GuideArrow;
    public GameObject ExtraCross;

    public GameObject lava;
    public Animator animator;

    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO USE LEVER";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 2)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);

                animator.SetBool("leverIsOpen", true);
                lava.GetComponent<LavaMechanics>().enabled = false;

            }
        }
    }


    void OnMouseExit()
    {
        ActionText.SetActive(false);
        ExtraCross.SetActive(false);
    }
}

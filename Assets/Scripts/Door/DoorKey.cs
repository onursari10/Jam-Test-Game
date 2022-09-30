using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorKey : MonoBehaviour
{
    public PlayerMovementTutorial playerMovementTutorial;

    public static float TheDistance;
    public GameObject ActionText;
    public GameObject GuideArrow;
    public GameObject ExtraCross;

    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }


    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if (playerMovementTutorial.doorKey <= 0)
        {
            playerMovementTutorial.doorKey = 0;
        }
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO PICK UP KEY";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 2)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);

                playerMovementTutorial.doorKey += 1;
                Destroy(gameObject);
            }
        }
    }


    void OnMouseExit()
    {
        ActionText.SetActive(false);
        ExtraCross.SetActive(false);
    }

}

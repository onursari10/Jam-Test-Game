using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    public static float TheDistance;
    public GameObject ActionText;
    public GameObject crystal;
    public GameObject GuideArrow;
    public GameObject ExtraCross;

    public GameObject player;
    public GameObject Asa;

    public GameObject visionPowerText;

    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

    }


    void OnMouseOver()
    {
        if (TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO PICK UP CRYSTAL";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 2)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionText.SetActive(false);
                crystal.SetActive(false);
                ExtraCross.SetActive(false);
                visionPowerText.SetActive(true);

                player.GetComponent<Sense>().enabled = true;
                player.GetComponent<WallRunning>().enabled = true;
                Asa.GetComponent<GrappleGun>().enabled = true;
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

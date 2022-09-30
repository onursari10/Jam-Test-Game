using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPistol : MonoBehaviour
{

    public static float TheDistance;
    public GameObject ActionText;
    public GameObject WeaponPistol;
    public GameObject PlayerPistol;
    public GameObject GuideArrow;
    public GameObject ExtraCross;



    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if(TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO PICK UP PISTOL";
            ActionText.SetActive(true);
        }

        if(Input.GetButtonDown("Action"))
        {
            if(TheDistance <= 2)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionText.SetActive(false);
                WeaponPistol.SetActive(false);
                ExtraCross.SetActive(false);
                PlayerPistol.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSystem : MonoBehaviour
{
    public string noteText;

    public Text Text;

    public GameObject Player;
    public static float TheDistance;
    public GameObject ActionText;
    public GameObject NotePanel;
    public GameObject GuideArrow;
    public GameObject ExtraCross;
    bool noteIsOpen = false;

    public GameObject key;


    void Start()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2 && noteIsOpen == false)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "PRESS [E] TO PICK UP NOTE";
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown("Action") && noteIsOpen == false)
        {
            if (TheDistance <= 2)
            {

                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                NotePanel.SetActive(true);
                noteIsOpen = true;
                Player.GetComponent<PlayerMovementTutorial>().enabled = false;


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
        Text.text = noteText;

        TheDistance = PlayerCasting.DistanceFromTarget;

        if (noteIsOpen == true)
        {
            ActionText.GetComponent<Text>().text = "PRESS [Q] TO DROP NOTE";
            ActionText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            NotePanel.SetActive(false);
            ActionText.SetActive(false);
            noteIsOpen = false;
            Player.GetComponent<PlayerMovementTutorial>().enabled = true;
            key.SetActive(true);
        }
    }
}

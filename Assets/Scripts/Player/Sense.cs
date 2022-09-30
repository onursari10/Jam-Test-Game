using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sense : MonoBehaviour
{
    public bool sensed = false;
    public GameObject SensePanel;

    public float time = 0;

    public GameObject timeText;

    void Start()
    {
        timeText.SetActive(true);
    }


    void Update()
    {
        timeText.GetComponent<Text>().text = "Vision Power = " + (int)time;

        if (sensed == true)
        {
            time -= Time.deltaTime;
        }
        if (sensed == false)
        {
            time += Time.deltaTime;
        }

        if (time <= 0)
        {
            time = 0;
        }

        if (time >= 5)
        {
            time = 5;
        }

        if (time == 0)
        {
            sensed = false;

            SensePanel.SetActive(false);
        }

        if (sensed == false)
        {
            if (Input.GetKeyDown(KeyCode.X) && time != 0)
            {
                sensed = true;
                SensePanel.SetActive(true);
            }

        }

    }

}

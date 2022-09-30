using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMechanics : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        transform.localScale = transform.localScale + new Vector3(0, 1, 0) * Time.deltaTime;
    }
}

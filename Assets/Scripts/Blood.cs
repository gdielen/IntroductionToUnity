using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blood : MonoBehaviour
{
    // (Start) moving speed Blood:
    [SerializeField] 
    private float _bloodSpeed = 3.0f;


    // Update is called once per frame
    void Update()
    {
        // blood-object moving: move the blood object
        transform.Translate(Vector3.left * _bloodSpeed  * Time.deltaTime );
   
        // when left is reached restart from right
        if (transform.position.x < -9.25f)
        {
            transform.position = new Vector3(9.25f, 
                Random.Range(-6.0f, 8.0f),
                0f);
        }
    }
    
}
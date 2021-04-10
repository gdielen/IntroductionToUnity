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


    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        {
            // blood-object moving: move the blood object
            transform.Translate(Vector3.left * _bloodSpeed  * Time.deltaTime );
       
            // when left is reached restart from right
            if (transform.position.x < -11.0f)
            {
                transform.position = new Vector3(11.0f, 
                    Random.Range(-6.0f, 6.0f),
                    0f);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // this = Blood
        // Other = Player 
        // Kollusionen plotten:
        Debug.Log(other.name);
        // Alternatives:
        // Debug.LogWarning(other.name);
        // Debug.LogError(other.name);
        
       
       // if player destroyed:
       if (other.CompareTag("Player"))
       {
            Destroy(this.gameObject);
       }
       
    }
}
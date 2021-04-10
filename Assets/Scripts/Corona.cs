using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corona : MonoBehaviour
{

    // (Start) moving speed Corona:
    [SerializeField] 
    private float _infectionSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        {
            // vaccine-object moving: move the Corona object
            transform.Translate(Vector3.down * _infectionSpeed  * Time.deltaTime );
            // vaccine object rotating round y:
            transform.Rotate(0.0f,2.5f,0.0f);

       
            // when bottom is reached restart from top
            if (transform.position.y < -5.0f)
            {
                transform.position = new Vector3(Random.Range(-8.0f, 8.0f),
                    6.2f,
                    0f);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // this = Corona Virus
        // Other = Player oder Vaccine (je nach Kontext)
        // Kollusionen plotten:
        Debug.Log(other.name);
        // Alternatives:
        // Debug.LogWarning(other.name);
        // Debug.LogError(other.name);
        
       
       // if the Corona collides with the player:
       if (other.CompareTag("Player"))
       {
            // damage player or destroy it   and virus:
            Debug.LogWarning("player health not implemented");
            // Damage() in Player.cs aufrufen:
            other.GetComponent<Player>().Dammage();
            Destroy(this.gameObject);
       }
       
       // if the Corona collides with the Vaccine:
       else if (other.CompareTag("Vaccine"))
       {
            Debug.LogWarning("Vaccine hit");
            // destroy both
            // this has an issue, but what?? Other Object lebt kurz weiter, also zuerst other zerstören.
            Destroy(other.gameObject);
            Destroy(this.gameObject);
       }
    }
}
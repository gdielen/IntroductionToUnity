using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corona : MonoBehaviour
{
    // (Start) moving speed Corona:
    [SerializeField] 
    private float _infectionSpeed = 4.0f;  // or 5.0




    // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the Corona object
        transform.Translate(Vector3.down * _infectionSpeed  * Time.deltaTime );
        // vaccine object rotating round y:
        transform.Rotate(0.0f,2.5f,0.0f);
        
        
        
        // // Change color: Get the Renderer component
        // var coronaRenderer = gameObject.GetComponent<Renderer>();
        // // var component = gameObject.GetComponent<"Lives">();
        // // var _lives = component.Lives;
        // var _lives = 2;
        // Debug.Log(_lives);
        // // THIS does not have any effect (even idf we give the objects a mesch renderer):
        // // var coronaRenderer = gameObject.GetComponentInChildren<Renderer>();
        // if (_lives < 3)
        // {
        //     // Call SetColor using the shader property name "_Color" and setting the color to yellow
        //     coronaRenderer.material.SetColor("_Color", Color.yellow);
        //     Color parentColour = GetComponentsInParent<Renderer>()[1].material.color;
        // }
        // if (_lives < 2)
        // {
        //     // Now setting the color to red
        //     coronaRenderer.material.SetColor("_Color", Color.red);
        //     Color parentColour = GetComponentsInParent<Renderer>()[1].material.color;
        // }



        // when bottom is reached restart from top
        if (transform.position.y < -5.2f)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f),
                7.5f,
                0f);
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
            if (! other.name.Contains("UVlight"))
            {
                Destroy(other.gameObject);

            }
            Destroy(this.gameObject);
       }
        
    }
    
}
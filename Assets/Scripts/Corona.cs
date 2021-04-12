using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corona : MonoBehaviour
{
    // (Start) moving speed Corona:
    [SerializeField] 
    private float _infectionSpeed = 4.0f;  // or 5.0
    [SerializeField] 
    private float _horizontalSpeed = 10.0f;  // or 5.0

    public int _lives ;
    
    
    public void ChangeCorona(int health)
    {
        _lives = health;
    }
    

    // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the Corona object
        transform.Translate(Vector3.down * _infectionSpeed  * Time.deltaTime );
        // vaccine object rotating round y:
        transform.Rotate(0.0f,2.5f,0.0f);

        if (name.Contains("B117"))
        {
            transform.Translate(Vector3.right * Random.Range(-1.0f,1.0f) * _horizontalSpeed * Time.deltaTime);
        }
        
        // when bottom is reached restart from top
        if (transform.position.y < -5.2f)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f),
                7.5f,
                0f);
        }
        
        // Change color: Get the Renderer component
        // var coronaRenderer = gameObject.GetComponent<Renderer>();
        // TODO: just colors one virus!
        { var coronaRenderer = GameObject.FindGameObjectWithTag("Virus").GetComponent<Renderer>();
            
            if (_lives == 2)
            {
                // Call SetColor using the shader property name "_Color" and setting the color to yellow
                coronaRenderer.material.SetColor("_Color", Color.yellow);
            }
            if (_lives == 1)
            {
                // Now setting the color to red
                coronaRenderer.material.SetColor("_Color", Color.red);
            }
        }
            
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // this = Corona Virus
        // Other = Player oder Vaccine (je nach Kontext)
        // Debug.Log(other.name);
        // Debug.LogWarning(other.name);
        // Debug.LogError(other.name);

        // if the Corona collides with the player:
        if (other.CompareTag("Player"))
        {
            // damage player or destroy it   and virus:
            // Debug.LogWarning("player health not implemented");
            // Damage() in Player.cs aufrufen:
            other.GetComponent<Player>().Dammage();
            Destroy(this.gameObject);
        }
       // if the Corona collides with the Vaccine:
       else if (other.CompareTag("Vaccine"))
       {
            // Debug.LogWarning("Vaccine hit");
            // destroy both
            if (! other.name.Contains("UVlight"))
            {
                Destroy(other.gameObject);

            }

            if (name.Contains("B117"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
            
            Destroy(this.gameObject);
       }
        
    }

}
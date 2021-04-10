using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{

    // Speed shall be altered in Inspector, so use [SerialiseFiled]
    // (Start) moving speed
    [SerializeField] 
    private float _speed = 7.0f;
    
    // 
    [SerializeField] 
    private GameObject _vaccinePrefab;
    
    // Slow down vaccine rate:
    [SerializeField] 
    private float _vaccinerate = 0.3f;
    // [SerializeField] 
    public float _timeToVaccinate = 0.0F;    

    // Lives of the Player:
    [SerializeField] 
    private int _lives = 3;

    // reference to spawnManager:
    [SerializeField]
    private SpawnManager _spawnManager;
    
    // reference to spawnManager:
    [SerializeField]
    private BloodManager _bloodManager;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // reset player-object position on start
        // (add on:  rotate: = new Vector3(0, 0, 90);
        transform.position = new Vector3(0, 0, 0);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        // Call Player Movement Funktion
        PlayerMovement();

        // Call Boundaries:
        Boundaries();
        
        // Vaccinate
        Vaccinate();

    }

    // Destroy (and change color) in case of Collusion:
    public void Dammage()
    {
        // reduce _lives by 1
        _lives -= 1;  // variables +=  -=  *=  etc. , also ++variable, variable++, --variable, variable--
        // if _lives = 0 destroy player
        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.GetComponent<SpawnManager>().onPlayerDeath();
            _bloodManager.GetComponent<BloodManager>().onPlayerDeath();
        }
        else
        {
            // // Change color: Get the Renderer component
            // var playerRenderer = gameObject.GetComponent<Renderer>();
            // // THIS does not have any effect (even idf we give the objects a mesch renderer):
            // // var playerRenderer = gameObject.GetComponentInChildren<Renderer>();
            // if (_lives < 3)
            // {
            //     // Call SetColor using the shader property name "_Color" and setting the color to yellow
            //     playerRenderer.material.SetColor("_Color", Color.yellow);
            // }
            // if (_lives < 2)
            // {
            //     // Now setting the color to red
            //     playerRenderer.material.SetColor("_Color", Color.red);
            // }
        }
    }
    


    // Player Movement Funktion:
    void PlayerMovement()
        {
            // player-object moving:
            // read player object input horizontally and vertically
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            // move the player object
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }

    // set boundaries:
    void Boundaries()
    {
        // boundaries:
        // setting up the vertical boundaries
        // check if player position is in field 
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x,
                0f,
                0f);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x,
                -4.5F,
                0f);
        }
        // setting up the horizontal boundaries
        // check if player position is in field 
        if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f,
                transform.position.y,
                0f);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f,
                transform.position.y,
                0f);
        }
    }
    void Vaccinate()
    {
        // SPACE pressed?  time to wait long enough // then instantiate vaccine prefab
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToVaccinate)
        {
            Debug.Log("space bar pressed");
            // Instantiate vaccine:
            // 'create' and make position welldefined: get pos. and quaternion corresponds to "no rotation"
            _timeToVaccinate = Time.time + _vaccinerate;
            Instantiate(_vaccinePrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity); 
        }

    }




}
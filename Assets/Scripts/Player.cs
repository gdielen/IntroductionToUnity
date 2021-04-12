using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Packages.Rider.Editor.UnitTesting;
// using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    // All that shall be altered in Inspector use [SerialiseFiled]
    
    [Header("Externel Components")]
        // Virus
        [SerializeField] 
        private GameObject _vaccinePrefab;
        // reference to spawnManager:
        [SerializeField]
        private GameObject _uvLightPrefab;
        [SerializeField]
        private SpawnManager _spawnManager;
        // reference to bloodManager:
        [SerializeField]
        private BloodManager _bloodManager;
        [SerializeField] 
        private UIManager _uiManager;
        [SerializeField] 
        private Corona _corona;


    [Header("Vaccination")]
        // Slow down vaccine rate:
        [SerializeField] 
        private float _vaccinationRate = 0.3f;
        [SerializeField] 
        private float _timeToVaccinate = 0.0F;   
            // = private float _canVaccinate = -1.0f;
            // [SerializeField] 
            // private Transform _vaccineParent;
        [SerializeField]
        // variable for timeout PowerUp:
        private float _powerupTimeout = 5.0f;

        
   [Header("Player")]
        // (Start) Player move speed:
        [SerializeField] 
        private float _speed = 5.0f;
        // Lives of the Player:
        [SerializeField] 
        private int _lives = 3;
        // Rotate speed:
        [SerializeField]
        private float _rotateSpeed = 0f;


    [Header("PowerUP-Status")]
        [SerializeField]
        private bool  _isUVLightOn = false;

        
        
    // ----------------------------------------------------------------------------------------------

        
    // Start is called before the first frame update
    void Start()
    {
        // reset player-object position on start // (add on:  rotate: = new Vector3(0, 0, 90);
        transform.position = new Vector3(0, 0, 0);

        // reset:
        _isUVLightOn = false;
    } 
    
    // ----------------------------------------------------------------------------------------------
    
    // Update is called once per frame
    void Update()
    {
        // Call Player Movement Funktion
        PlayerMovement();

        // Call Boundaries:
        Boundaries();
        
        // Vaccinate
        Vaccinate();
        
        // Corona color:
        _corona.ChangeCorona(_lives);
      //  GameObject.FindWithTag("Virus").GetComponent<Corona>().ChangeCorona(_lives);

      //  Color vaccines dependiung on health:
      var vaccineRenderer = GameObject.FindGameObjectWithTag("Vaccine").GetComponent<Renderer>();
      if (_lives == 2)
      {
          // Call SetColor using the shader property name "_Color" and setting the color to yellow
          vaccineRenderer.material.SetColor("_Color", Color.yellow);
      }
      if (_lives == 1)
      {
          // Now setting the color to red
          vaccineRenderer.material.SetColor("_Color", Color.red);
      }
      
    }

    
    // Destroy (and change color) in case of Collusion:
    public void Dammage()
    {
        // reduce _lives by 1
        _lives -= 1;  // variables +=  -=  *=  etc. , also ++variable, variable++, --variable, variable--
        // Update health-Info:
        _uiManager.UpdateHealth(_lives);
        // speedup rotating when loosing a live:
        _rotateSpeed +=10;
        // if _lives = 0 destroy player
        if (_lives < 1)
        {
            if (_spawnManager != null)
            {
                // _spawnManager.onPlayerDeath();
                _spawnManager.GetComponent<SpawnManager>().onPlayerDeath();
                _bloodManager.GetComponent<BloodManager>().onPlayerDeath();
            }
            else
            {
                Debug.LogError("SpawnManager not assigned!");
            }
            _uiManager.GameOver();
            Destroy(this.gameObject);
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
            //     Color parentColour = GetComponentsInParent<Renderer>()[1].material.color;
            // }
            // if (_lives < 2)
            // {
            //     // Now setting the color to red
            //     playerRenderer.material.SetColor("_Color", Color.red);
            //     Color parentColour = GetComponentsInParent<Renderer>()[1].material.color;
            // }
        }
    }


    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }




    // Player Movement Funktion:
    void PlayerMovement()
        {
            // rotate Syringe:
            // transform.position = new Vector3( 0.0f, 0.0f, 0.0f);
            transform.GetChild(0).Rotate(0.0f, 0.0f, _rotateSpeed, Space.Self);
            
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
        // boundaries:  0 - -3.8 |  -9.25 - 9.5
        // setting up the vertical boundaries
        // check if player position is in field 
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x,
                0f,
                0f);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x,
                -3.8F,
                0f);
        }
        // setting up the horizontal boundaries
        // check if player position is in field 
        if (transform.position.x > 9.25f)
        {
            transform.position = new Vector3(-9.25f,
                transform.position.y,
                0f);
        }
        else if (transform.position.x < -9.25f)
        {
            transform.position = new Vector3(9.25f,
                transform.position.y,
                0f);
        }
    }
    
    
    void Vaccinate()
    {
        // SPACE pressed?  time to wait long enough // then instantiate vaccine prefab
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToVaccinate)
        {
            // Debug.Log("space bar pressed");
            // Instantiate vaccine drop: 'create' and make position welldefined: get pos. and quaternion corresponds to "no rotation"
            _timeToVaccinate = Time.time + _vaccinationRate;
            
            // Power-Up avaliable?:
            Debug.Log(_isUVLightOn);
            if (!_isUVLightOn)
            { 
                Instantiate(_vaccinePrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity); 
            }
            else
            {
                Instantiate(_uvLightPrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity); 
            }
        }
    }

    
    // PowerUp starts:
    public void ActivatePowerUp()
    {
        _isUVLightOn = true;
        StartCoroutine(DeactivatePowerup());
    }
    //PowerUp Ends
    IEnumerator DeactivatePowerup()
    {
        yield return new WaitForSeconds(_powerupTimeout);
        _isUVLightOn = false;
    }

}
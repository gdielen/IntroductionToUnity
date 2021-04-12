using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private int _score = 0;
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private bool _powerOn = false;
    [SerializeField] 
    private Text _scoreText;
    [SerializeField] 
    private Text _healthText;
    [SerializeField] 
    private Text _gameStartText;
    [SerializeField] 
    private Text _gameOverText;
    [SerializeField] 
    private Text _powerUpText;

    void Start()
    {
        Time.timeScale = 0;
        _gameStartText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _healthText.gameObject.SetActive(false);
        _powerUpText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Lives left: " + health;
}

    void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1;
            _gameStartText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
            _healthText.gameObject.SetActive(true);
        }
    }

 
    
    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    public void PowerUp()
    {
        _powerUpText.gameObject.SetActive(true);
    }
    
     public void NoPowerUp()
     {
         _powerUpText.gameObject.SetActive(false);
     }
     
   
    public void UpdateHealth(int health)
    {
        // Change Color??
        // TODO: change color
        _healthText.text = "Lives left: " + health;
    }


    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }
    
}
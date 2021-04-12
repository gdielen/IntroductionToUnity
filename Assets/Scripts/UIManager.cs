using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private int _score = 0;
    [SerializeField]
    private int health = 3;
    [SerializeField] 
    private Text _scoreText;
    [SerializeField] 
    private Text _healthText;
    [SerializeField] 
    private Text _gameStartText;
    [SerializeField] 
    private Text _gameOverText;

    void Start()
    {
        _gameStartText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Lives left: " + health;
    }
    
    
    public void StartBegin()
    {
        _gameStartText.gameObject.SetActive(true);
    }

    
    public void StartFinished()
    {
        _gameStartText.gameObject.SetActive(false);
    }

    
    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _gameStartText.gameObject.SetActive(false);
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

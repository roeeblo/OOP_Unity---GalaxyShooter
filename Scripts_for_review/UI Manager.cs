using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
    
{
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _highestscore;


    private int _bestScore = 0;
   
    
    private GameManager _GameManager;
    private Player player;



    void Start()
    {
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
       

        _bestScore = PlayerPrefs.GetInt("HighestScore", 0);
        _highestscore.text = "Highest Score: " + _bestScore.ToString();

        UpdateScoreSys(0);

        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_GameManager == null)
        {
            Debug.LogError("GameManager IS null");
        }
    }

    public void UpdateScoreSys(int playerscore)
    {
        _scoretext.text = "Score: " + playerscore.ToString();

        if (playerscore >= _bestScore)
        {
            _bestScore = playerscore;
            _highestscore.text = "Highest Score: " + _bestScore;
        }
        PlayerPrefs.SetInt("HighestScore", _bestScore);
        PlayerPrefs.Save();
    }

    public void UpdateLives (int currentLives)
    {
 
        _LivesImage.sprite = _livesSprites[currentLives];
    }
    public void GameOverScene()
    {
        _GameManager.isgameover();
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    public void GoToMainMenu()
    {
        _GameManager.GoToMainMenu();
    }

    public void ResumeGame()
    {
        _GameManager.ResumeGame();
    }


    public void QuitGame()
    {
        _GameManager.QuitGame();
    }




    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            _gameoverText.gameObject.SetActive(true);
        }

    }
    
}

  

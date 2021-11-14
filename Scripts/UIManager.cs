using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public UnityEvent gameEnd;
    [SerializeField] Text scoreText;
    [SerializeField] Text preScoreText;
    [SerializeField] Image hp;
    public int score;
    public float playerHp;
    public int preScore;

    void Start()
    {
        scoreText.gameObject.SetActive(false);
        preScoreText.gameObject.SetActive(false);
        hp.gameObject.SetActive(false);
        playerHp = 100;
    }

    void Update()
    {
        hp.fillAmount = playerHp / 100;
        GameEnd();
    }

    public void GameStart()
    {
        scoreText.gameObject.SetActive(true);
        preScoreText.gameObject.SetActive(true);
        scoreText.text = score.ToString();
        preScoreText.text = preScore.ToString();
        hp.gameObject.SetActive(true);
    }

    void GameEnd()
    {
        if(playerHp <= 0)
        {
            scoreText.gameObject.SetActive(false);
            hp.gameObject.SetActive(false);
            preScoreText.gameObject.SetActive(false);
            playerHp = 100;
            gameEnd.Invoke();
            score = 0;
        }
    }

    public void GetScore(int _score)
    {
        score += _score;
        if(preScore < score)
        {
            preScore = score;
        }
    }

    public void DamageHP(float _hp)
    {
        playerHp -= _hp;
    }
}

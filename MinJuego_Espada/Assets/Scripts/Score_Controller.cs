﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Controller : MonoBehaviour
{
    Player_Controller player;
    Enemy enemy;
    private int contpj1 = 0;
    private int contpc = 0;
    public Text scorepj1, scorepc;
    public GameObject win_lose;
    public Text lose;
    public GameObject round;
    public Text round_text;
    private int countRound = 1;
    public GameObject marcador;
    private bool adsActivar = false;
    LogicalAds ads;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player_Controller>();
        enemy = GameObject.FindObjectOfType<Enemy>();
        ads = GameObject.FindObjectOfType<LogicalAds>();
        round.SetActive(true);
    }

    void Update()
    {
        Vidas();
    }

    public void Vidas()
    {
        if (player.isDead)
        {
            contpc++;
            scorepc.text = contpc.ToString();
            player.isDead = false;
            countRound++;
            round_text.text = "Round " + countRound;
        }
        else if (enemy.isDead)
        {
            contpj1++;
            scorepj1.text = contpj1.ToString();
            enemy.isDead = false;
            countRound++;
            round_text.text = "Round " + countRound;
        }

        if (contpj1 == 2)
        {
            round.SetActive(false);
            win_lose.SetActive(true);
            marcador.SetActive(false);
            LogicalAds.instance.MostrarInterstitial();
        }
        if (contpc == 2)
        {
            lose.text = "YOU LOSE!";
            round.SetActive(false);
            win_lose.SetActive(true);
            marcador.SetActive(false);
            LogicalAds.instance.MostrarInterstitial();
        }
    }
}
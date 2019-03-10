using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Controller : MonoBehaviour
{
    public GameObject pj1, pc;
    Player_Controller player;
    Enemy enemy;
    public int contpj1 = 0;
    public int contpc = 0;
    public Text scorepj1, scorepc;
    public GameObject win_lose;
    public Text lose;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player_Controller>();
        enemy = GameObject.FindObjectOfType<Enemy>();
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
        }
        else if(enemy.isDead)
        {
            contpj1++;
            scorepj1.text = contpj1.ToString();
            enemy.isDead = false;
        }

        if (contpj1 == 2)
        {
            win_lose.SetActive(true);
        }
        if (contpc == 2)
        {
            lose.text = "YOU LOSE!";
            win_lose.SetActive(true);
        }
    }
}

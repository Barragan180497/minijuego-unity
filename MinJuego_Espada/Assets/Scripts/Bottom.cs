using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    Player_Controller Botom;
    // Start is called before the first frame update
    void Start()
    {
        Botom = GameObject.FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DerechaOn()
    {
        Botom.derecha = true;
    }

    public void IzquierdaOn()
    {
        Botom.izquierda = true;
    }

    public void SaltarOn()
    {
        Botom.saltar = true;
    }

    public void AtacarOn()
    {
        Botom.atacar = true;
    }

    public void Detener()
    {
        Botom.derecha = false;
        Botom.izquierda = false;
        Botom.saltar = false;
        Botom.atacar = false;
    }
}
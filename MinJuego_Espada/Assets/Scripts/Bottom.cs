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

    public void DerechaOff()
    {
        Botom.derecha = false;
    }

    public void IzquierdaOn()
    {
        Botom.izquierda = true;
    }

    public void IzquierdaOff()
    {
        Botom.izquierda = false;
    }

    public void SaltarOn()
    {
        Botom.saltar = true;
    }

    public void SaltarOff()
    {
        Botom.saltar = false;
    }

    public void AtacarOn()
    {
        Botom.atacar = true;
    }

    public void AtacarOff()
    {
        Botom.atacar = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botonScript : MonoBehaviour
{

    

    public void Escene(string nombreEscena){
		SceneManager.LoadScene(nombreEscena);
	}

	public void QuitGame(){

		Application.Quit();
	}
}

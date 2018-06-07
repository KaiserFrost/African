using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour {

    public GameObject guiObj;
    public string lvlToLoad;

	// Use this for initialization
	void Start () {
        guiObj.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("CasaPrincipal");

            }

        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(true);
            if ( Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("CasaPrincipal");
                
            }

        }

        
    }

    void OnTriggerExit()
    {
        guiObj.SetActive(false);
    }
}

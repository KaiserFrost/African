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

   
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(true);
            if ( Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(lvlToLoad);
                
            }

        }

        
    }

    void OnTriggerExit()
    {
        guiObj.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MoveScene : MonoBehaviour {

    public Text texto;
    public VideoPlayer vip;
	// Use this for initialization
	void Start () {
        texto.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("CenaJogo");
        }
        if (vip.isPlaying == false)
        {
            texto.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("CenaJogo");
            }
        }
        
	}

}

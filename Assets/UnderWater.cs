using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UnderWater : MonoBehaviour {
    private Collider colide;
    
    public GameObject disableSun;
    public GameObject disableStars;
    public float waterlevel;
    public bool isUnderWater = true;
    public Color underwaterColor;
    private bool defaultFog ;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material noSkybox;
    private PController controller;
    private CharacterController controllerChar;
    Vector3 moveDirection;
    public Animator animator;
    
    
    //buoyancy
    

    void Start () {
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        controller = GetComponent<PController>();
        controllerChar = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
      //  player = GetComponent<Rigidbody>();
      
    }
	
	// Update is called once per frame
	void Update () {
        //buoyancy
       
        if ((transform.position.y <= waterlevel - controllerChar.height/2) )
        {

            isUnderWater = false;
            print(isUnderWater);
            
            if(Camera.main.transform.position.y <= waterlevel)
            {
                RenderSettings.fog = true;
                RenderSettings.fogColor = underwaterColor;
                RenderSettings.fogDensity = 0.03f;
                disableSun.SetActive(false);
                disableStars.SetActive(false);
            }
        }
        else
        {
            isUnderWater = true;
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            disableSun.SetActive(true);
            disableStars.SetActive(true);
            animator.SetBool("isSwimming", false);

        }
    }
   /* void SetNormal()
    {
        RenderSettings.fog = defaultFog;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
    }

    void SetUnderWater()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.03f;
    }*/
}

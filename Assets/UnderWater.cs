using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UnderWater : MonoBehaviour {
    private Collider colide;
    public GameObject disableSun;
    public GameObject disableStars;
    public float waterlevel;
    private bool isUnderWater;
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
   /* private Vector3 upLift, actionPoint;
    public Vector3 buoyancyoffset;
    public float floatHeight,bounceDamp;
    private float forceFactor;*/

    void Start () {
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        controller = GetComponent<PController>();
        controllerChar = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
      
    }
	
	// Update is called once per frame
	void Update () {
        //buoyancy
        /* actionPoint = transform.position + transform.TransformDirection(buoyancyoffset);
         forceFactor = 1f - ((actionPoint.y - waterlevel) / floatHeight);
         if(forceFactor > 0f)
         {
             upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
             GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
         }*/
        if ((transform.position.y <= waterlevel - controllerChar.height/2) && Camera.main.transform.position.y <= waterlevel)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = 0.03f;
            disableSun.SetActive(false);
            disableStars.SetActive(false);
         
           // Physics.gravity = new Vector3(1f, 0.5f, 0f);
            animator.SetBool("isSwimming", true);

        }
        else
        {
           // Physics.gravity = new Vector3(0f, -9.8f, 0f);
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

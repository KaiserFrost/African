using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaCameras : MonoBehaviour {
   
    public GameObject player;
    public GameObject FirstPersonCamera;
    public GameObject ThirdPersonCam;
    bool Estado;//só serve pa verificar em q estado ta, pa facilitaar no update
    //public Texture2D mira;
    public bool miraOnOff;
    public Texture mira;
    private GUI DrawTexture;
    private float yaw = 0.0f;
    private float pitch, pitchfirst = 0.0f;
    public float speed;
    

    //public Transform player;
    public float distance = 2f;
    public float height = 1f;

    private Vector3 offsetX;
    private Vector3 offsetY;
    private Vector3 offset;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float yMinLimitfirst = -90f;
    public float yMaxLimitfirst = 60f;

    public float distanceMin = 4f;
    public float distanceMax = 15f;
    // Use this for initialization
    void Start () {
        //Pa escolher a camera, em principio vai ficar a começar na 3persona
       
        FirstPersonCamera.gameObject.active = false;
        ThirdPersonCam.gameObject.active = true;
      
        Vector3 angles = transform.eulerAngles;
        pitch= angles.y;
        yaw = angles.x;
       
    
    }

    private void LateUpdate()
    {
         
        pitchfirst -= Input.GetAxis("Mouse Y") * speed;

        pitchfirst = ClampAngle(pitchfirst, yMinLimitfirst, yMaxLimitfirst);

        FirstPersonCamera.transform.eulerAngles = new Vector3(pitchfirst, yaw, 0.0f);


        yaw += Input.GetAxis("Mouse X") * speed;

        pitch -= Input.GetAxis("Mouse Y") * speed;

        pitch = ClampAngle(pitch, yMinLimit, yMaxLimit);
       
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin-1, distanceMax);
        if(distance == distanceMin-1)
        {
            FirstPersonCamera.gameObject.active = true;
            ThirdPersonCam.gameObject.active = false;
        }
        else
        {
            FirstPersonCamera.gameObject.active = false;
            ThirdPersonCam.gameObject.active = true;
        }
          
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 heighten = new Vector3(0.0f, height, 0.0f);
            Vector3 position = rotation * negDistance + player.transform.position + heighten;

            ThirdPersonCam.transform.rotation = rotation;
            ThirdPersonCam.transform.position = position;
        
        

    }


    // Update is called once per frame
    void Update () {
        
     
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Estado)
            {   //Primeira Pessoa
                FirstPersonCamera.gameObject.active = true;
                ThirdPersonCam.gameObject.active = false;
               
            }
            else
            {   //Terceira Pessoa
                FirstPersonCamera.gameObject.active = false;
                ThirdPersonCam.gameObject.active = true;
                miraOnOff = false;
            }

            Estado = !Estado;
        }
	}

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    void OnGUI()
    {
        Cursor.visible = false;
        GUI.DrawTexture(new Rect(Screen.width / 2 - mira.width / 4, Screen.height / 2 - mira.height / 4, mira.width, mira.height), mira);

    }
}

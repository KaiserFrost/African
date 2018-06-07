using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float speed;

    public Vector3 followOffset, idleOffset;
    public float maxCameraVelocity;
    public float accelaration;
    public float slowDownDistance;


    float currentVelocity = 0;
    Transform player;
    Animator playerAnimator;
    Vector3 targetPositon;
    bool zoomingIn = true;
     

    // Use this for initialization
    void Start()
    {

        //get player referene
        player = GameObject.FindGameObjectWithTag("Player").transform;//Fazer uso da tag

        playerAnimator = player.GetComponentInParent<Animator>();


        transform.position = player.position -
                player.forward * followOffset.z +
                player.up * followOffset.y;

    }

    // Update is called once per frame
    void Update()
    {
       // yaw += Input.GetAxis("Mouse X") * speed;
       // pitch -= Input.GetAxis("Mouse Y") * speed;
        //camera.transform.Rotate(lookhere);
       // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (playerAnimator.GetBool("isWalking"))
        {
            if (zoomingIn) currentVelocity = 0;
            //Place camera in top-down position
            targetPositon = player.position -
                player.forward * followOffset.z +
                player.up * followOffset.y;

        }
        else
        {
            if (!zoomingIn) currentVelocity = 0;
            //Place camera in top-down position
            targetPositon = player.position -
                player.forward * idleOffset.z +
                player.up * idleOffset.y;
            currentVelocity = 0;

        }
        zoomingIn = !playerAnimator.GetBool("isWalking");

        if(zoomingIn && (transform.position - targetPositon).magnitude < slowDownDistance)
        {
            currentVelocity = Time.deltaTime * (transform.position - targetPositon).magnitude * maxCameraVelocity / slowDownDistance;
        }

        currentVelocity = currentVelocity + accelaration * Time.deltaTime;
        if (currentVelocity > maxCameraVelocity)
        {
            currentVelocity = maxCameraVelocity;
        }

        //move camera towards target position
        if ((targetPositon - transform.position).magnitude > currentVelocity)
        {
            transform.position += (targetPositon - transform.position).normalized * currentVelocity;
        }
        else
        {
            transform.position = targetPositon;

        }

        //transform.position += (tragetPosition - transform.position).normalized * maxCameraVelocity;
        //make camera look at player
        transform.LookAt(player.position);
    }
}

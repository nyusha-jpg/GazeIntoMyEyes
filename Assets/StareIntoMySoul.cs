using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareIntoMySoul : MonoBehaviour
{
    // Start is called before the first frame update

    public float sphereRadius; //how "big" the mouse is, ie from how far away it's detected

    public float zoomSpeed; //speed of eyeballs moving towards camera

    public GameObject leftEyeball;

    public GameObject rightEyeball;

    public GameObject text;
    public GameObject button;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eyePosition = transform.position; //camera pos
        Vector3 mousePos = Input.mousePosition; //mouse pos
        Vector3 leftEyePos = leftEyeball.transform.position; //left eye pos
        Vector3 rightEyePos = rightEyeball.transform.position; //right eye pos

        mousePos.z = Camera.main.nearClipPlane; //at what pos the mouse no longer pays attention to raycasting

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos); //mouseWorldPos is the position of the mouse in relation to the game world

        Vector3 dir = mouseWorldPos - eyePosition; //direction that mouse is pointing

        dir.Normalize(); //set to a magnitude of 1

        RaycastHit hitter = new RaycastHit();

        Debug.DrawLine(eyePosition, dir * 20f, Color.red); //draw a red line where the raycast is pointing

        if (leftEyeball.transform.position.y > 13.7 || rightEyeball.transform.position.y > 13.7) //if either eyeball is totally obscuring the camera
            {
                text.SetActive(true); //show the "you are too close" text
                button.SetActive(true); //show the reload scene button
            }

         if(Physics.SphereCast(eyePosition, sphereRadius, dir, out hitter)) //if the raycast is hitting an object
            {
                    if (hitter.collider.gameObject.name == "RightEyeball") //if it's hitting the right eyeball
                    {
                        rightEyeball.transform.position = Vector3.MoveTowards(rightEyePos, eyePosition, zoomSpeed * Time.deltaTime); //move the position of the right eye towards the camera at a steady rate
                    }
              
                     if (hitter.collider.gameObject.name == "LeftEyeball") //if it's hitting the left eyeball
                         {
                          leftEyeball.transform.position = Vector3.MoveTowards(leftEyePos, eyePosition, zoomSpeed * Time.deltaTime); //move the position of the left eye towards the camera at a steady rate
                         }
                     }

                
            }

    }




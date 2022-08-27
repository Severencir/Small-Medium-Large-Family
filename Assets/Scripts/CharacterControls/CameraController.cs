using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //get player for following
    GameObject player;
    private Vector3 offset;
    public static float rotateSpeed = 20000f;
    float xRotate = 0;
    float cameraDist;
    Camera cam;

    void Start()
    {
        //get player by tag
        player = GameObject.FindGameObjectWithTag("Player");
        //set offset to current offset
        offset = transform.position - player.transform.position;

        cam = Camera.main;
        cameraDist = Vector3.Distance(transform.position, cam.transform.position);
    }
    
    void LateUpdate()
    {
        //update the position after any player movement to ensure smooth camera
        transform.position = player.transform.position + offset;

        //get vertical mousedelta and player y axis rotation, rotate to get camera to shift
        Vector2 look = Inp.inputs.Player.Look.ReadValue<Vector2>();

        look /= Screen.height;
        xRotate -= look.y * rotateSpeed * Time.deltaTime;
        if (xRotate < -89f)
            xRotate = -89f;
        if (xRotate > 89f)
            xRotate = 89f;
        if (!SpriteManager.IsDead)
            transform.eulerAngles = new Vector3(xRotate, player.transform.eulerAngles.y, transform.eulerAngles.z);

        RaycastHit hit;
        Ray ray = new(transform.position, -transform.forward);

        bool hasHit = Physics.Raycast(ray, out hit, cameraDist);

        if (!SpriteManager.IsDead)
            cam.transform.position = -transform.forward * (hasHit ? (int)(hit.distance - 0.3f) : cameraDist) + transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFire : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;
    public Vector2 minPower;
    public Vector2 maxPower;
    public Camera cam;
    private Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
    TrajectorLine trajector;
    
    void Start()
    {   
        trajector = GetComponent<TrajectorLine>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("press left button");
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
            Debug.Log(startPoint);
        }

        if(Input.GetMouseButton(0)){
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            trajector.RenderLine(startPoint, currentPoint);
        }

        if(Input.GetMouseButtonUp(0)){
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y) );
            rb.AddForce(force * power, ForceMode2D.Impulse);

            trajector.EndLine();

        }
        
    }
}

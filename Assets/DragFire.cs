using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int bounceCount;
    public Text count;
    public float timer;
    Vector3 mouseScreenPosition;
    public Transform arrow;
    public GameObject arrowPrent;
    void Start()
    {   
        trajector = GetComponent<TrajectorLine>();
        cam = Camera.main;
        bounceCount = int.Parse(count.text);
        Debug.Log(bounceCount);
        arrowPrent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        if(bounceCount < 5){
            StartCoroutine(CdTimer());
        }
         RotateArrow();
      
    }
    private IEnumerator CdTimer(){
        yield return new WaitForSeconds(5);
        ReplenishFire();
    }
    
    private void ReplenishFire(){
       bounceCount++;
       count.text = bounceCount.ToString();
       Debug.Log("bounccound " + bounceCount);
    }
    public void RotateArrow(){
        mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - arrow.transform.position.y, lookAt.x - arrow.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        arrow.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }
    void Fire(){

         if(bounceCount >= 0)  {    
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("press left button");
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                
                startPoint.z = 15;
                bounceCount--;
                count.text = bounceCount.ToString();
                Debug.Log(startPoint);
                arrowPrent.SetActive(true);
            }

            if(Input.GetMouseButton(0)){
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);

                // float dirAngle = Vector3.Angle(startPoint, currentPoint);
                // arrowPrent.transform.Rotate( 0, 0,dirAngle);
                currentPoint.z = 15;
                trajector.RenderLine(startPoint, currentPoint);
            }

            if(Input.GetMouseButtonUp(0)){
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y) );
                rb.AddForce(force * power, ForceMode2D.Impulse);

                trajector.EndLine();
                arrowPrent.SetActive(false);
            }    
        }
    }
}

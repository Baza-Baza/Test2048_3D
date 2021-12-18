using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    [SerializeField]
    private int speed = 100;

    private int sideSpeed = 10;
    private Rigidbody rb;
    private Vector3 movement;
    private bool isMovingForward;
    private Vector3 intPos;
    private float distance;
    private Vector3 rayPoint;
    private bool isMoveByX;
    private float minPosX = -2.4f;
    private float maxPosX = 2.4f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 15))
            {
                if (this)
                {
                    intPos = transform.position;

                    Vector3 rayPoint = ray.GetPoint(0);
                    distance = Vector3.Distance(transform.position, rayPoint);
                    isMoveByX = true;

                }
            }
        
        }
        else if(Input.GetMouseButtonUp(0))
            isMovingForward = true;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayPoint = ray.GetPoint(distance);

        }

        
    }
    private void FixedUpdate()
    {
        if (isMovingForward)
        {
            isMovingForward = false;
            rb.velocity = new Vector3(0, 0, speed);
            isMoveByX = false;
            StartCoroutine(SatrtMoveCube());

        }
        
        if (isMoveByX)
        {
                rb.MovePosition(intPos + new Vector3(rayPoint.x, 0, 0));           
        }


    }

    private IEnumerator SatrtMoveCube()
    {
        yield return new WaitForSeconds(.2f);
        GameManager.Instance.isMovingPrefab = true;
        gameObject.GetComponent<Cube>().isReadyCheckPosition = true;
        Destroy(GetComponent<CubeMove>());
    }
}

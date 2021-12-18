using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CubeMaterials
{ 
    Two = 0,
    Four = 1,
    Eight = 2,
    Sixteenth = 3,
    Thirty_Two = 4,
    Sixty_Four = 5,
    One_Hundred_Twenty_Eight = 6,
    Two_Hundred_Fifty_Six = 7,
    Five_hundred__Twelve = 8,
    One_Thousand_Twenty_Four = 9,
    Two_Thousand_Forty_Eight = 10
};
public class Cube : MonoBehaviour
{
    [SerializeField]
    private CubeMaterials cubeMaterials;
    
    public bool isReadyCheckPosition;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        CheckCollision(other);
    }
    private void OnCollisionStay(Collision other)
    {
        CheckCollision(other);
    }

    private void Update()
    {
        if (rb.velocity.y > 5.5f)
            rb.velocity = new Vector3(rb.velocity.x, 5.5f, rb.velocity.z);
    }
    private IEnumerator CubeImpulse()
    {
        yield return new WaitForSeconds(.15f);
        rb.AddForce(transform.up * 250);
        yield return new WaitForSeconds(.07f);
        rb.AddTorque(transform.up * 20);
    }
    private void CheckCollision(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.name == gameObject.GetComponent<MeshRenderer>().material.name)
        {
            if (gameObject.transform.position.z > other.gameObject.transform.position.z)
            {
                for (int i = 0; i < GameManager.Instance.materialsCubes.Count; i++)
                {
                    if ((int)cubeMaterials == i)
                    {
                        StartCoroutine(CubeImpulse());
                        
                        gameObject.GetComponent<MeshRenderer>().material = GameManager.Instance.materialsCubes[i + 1];
                        if (i + 1 == GameManager.Instance.materialsCubes.Count - 1)
                        {
                            GameManager.Instance.CheckGameWinOrOver("You Win!");
                        }
                        cubeMaterials = (CubeMaterials)i + 1;
                        Destroy(other.gameObject);
                        return;
                    }


                }

            }


        }
    }


}


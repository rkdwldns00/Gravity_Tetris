using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    Rigidbody rigid;
    public bool isFalling = true;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFalling)
        {
            rigid.AddForce(new Vector3(Input.GetAxisRaw("Horizontal")*5, 0));
            if (Input.GetAxisRaw("Vertical") == 1f)
            {
                rigid.AddTorque(new Vector3(0, 0, 3));
            }
            if (Input.GetAxisRaw("Vertical") == -1f)
            {
                rigid.AddForce(Vector3.down * 3);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Cell") && isFalling)
        {
            isFalling = false;
            Spawner.instance.Spawn();
            Checker.instance.CheckAll(true);
        }
    }
}

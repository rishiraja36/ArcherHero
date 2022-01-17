using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Collider collider;
    Rigidbody rb;
    public int damage;
    void Awake()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = collision.GetContact(0).point;
        transform.SetParent(collision.transform);
        collider.enabled = false;
        Debug.Log(collision.transform.name);

        if(collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<BodyPart>().TakeDamage(damage);
            collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);

        }

        Debug.Log("Collision enter");
      
      
    }
}

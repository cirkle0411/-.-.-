using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat1 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D Cat1Rigidbody;
    private bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        Cat1Rigidbody = GetComponent<Rigidbody2D>();
        Cat1Rigidbody.velocity = new Vector2(speed, Cat1Rigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            Cat1Rigidbody.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            isStopped = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D SkeletonRigidbody;
    private bool isStopped = false;
    private Animator animator;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SkeletonRigidbody = GetComponent<Rigidbody2D>();
        SkeletonRigidbody.velocity = new Vector2(-speed, SkeletonRigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            SkeletonRigidbody.velocity = Vector2.zero;

            animator.SetTrigger("Attack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStopped = true;
        }
    }
}

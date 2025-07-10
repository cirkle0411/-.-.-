using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    skeleton
}

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float max_hp;
    private Rigidbody2D MonsterRigidbody;
    private bool isStopped = false;
    private Animator animator;
    private float cur_hp;

    public float monster_atk;
    public float monster_atkspd;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        MonsterRigidbody = GetComponent<Rigidbody2D>();

        cur_hp = max_hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        MonsterRigidbody.velocity = new Vector2(-speed, MonsterRigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            MonsterRigidbody.velocity = Vector2.zero;

            animator.SetTrigger("Attack");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Cat1 cat1 = collision.gameObject.GetComponent<Cat1>();
            if (cat1 != null)
            {
                isStopped = true;
            }
        }
    }
}

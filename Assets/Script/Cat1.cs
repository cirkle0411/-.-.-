using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Cat1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float max_hp;
    private Rigidbody2D Cat1Rigidbody;
    private bool isStopped = false;
    private Animator animator;
    private float cur_hp;

    public float cat1_atk;
    public float cat1_atkspd;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Cat1Rigidbody = GetComponent<Rigidbody2D>();

        cur_hp = max_hp;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
             monster = collision.gameObject.GetComponent<Skeleton>();
            if (monster != null)
            {
                isStopped = true;
                StartCoroutine(Hit(skeleton.skeleton_atk, skeleton.skeleton_atkspd));
            }
        }
    }

    System.Collections.IEnumerator Hit(float damage, float atkspd)
    {
        while (isStopped)
        {
            animator.SetTrigger("Attack");
            cur_hp -= damage;
            yield return new WaitForSeconds(atkspd);

            if (cur_hp <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()
    {
        animator.SetTrigger("Dead");

        Destroy(gameObject, 1f);
    }
}

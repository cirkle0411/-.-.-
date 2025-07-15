using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float power;
    public float interval;

    [SerializeField] private float speed;
    [SerializeField] private float maxHP;

    private Dictionary<Monster, Coroutine> damageCoroutines = new Dictionary<Monster, Coroutine>();

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private bool isattack = false;
    private float curHP;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("Animator component is missing on the Cat GameObject.");
        }
        
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {
            Debug.Log("Rigidbody2D component is missing on the Cat GameObject.");
        }
        
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.Log("Collider2D component is missing on the Cat GameObject.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            if(!damageCoroutines.ContainsKey(monster))
            {
                rigidbody.velocity = Vector2.zero;
                animator.SetTrigger("Attack");

                Coroutine Coroutine = StartCoroutine(TakeDamage(monster));
                damageCoroutines[monster] = Coroutine;
            }
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Monster"))
        {
            rigidbody.velocity = Vector2.zero;
        }
    }*/

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            if (damageCoroutines.ContainsKey(monster))
            {
                StopCoroutine(damageCoroutines[monster]);
                damageCoroutines.Remove(monster);

                if(damageCoroutines.Count == 0)
                {
                    animator.SetTrigger("Walk");
                    rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                }
            }
        }
    }

    System.Collections.IEnumerator TakeDamage(Monster monster)
    {
        while (true)
        {
            curHP -= monster.power;
            //Debug.Log("Cat takes damage: " + monster.power + ", Current HP: " + curHP);

            if (curHP <= 0)
            {
                Die();
                yield break;
            }

            yield return new WaitForSeconds(monster.interval);
        }
    }

    /*System.Collections.IEnumerator TakeDamage(float damage, float interval)
    {
        while (curHP > 0)
        {
            curHP -= damage;
            Debug.Log("Cat takes damage: " + damage + ", Current HP: " + curHP);

            if (curHP <= 0)
            {
                Die();
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }
    }*/

    void Die()
    {
        collider.enabled = false;
        Debug.Log("Cat has died.");
        StopAllCoroutines();
        animator.SetTrigger("Die");
        rigidbody.velocity = Vector2.zero;

        Destroy(gameObject, 1f);
    }
}
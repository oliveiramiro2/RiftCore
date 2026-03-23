using Unity.Mathematics;
using UnityEngine;
using System.Collections;

enum State
{
    Chase,
    Attack
}

public class Zombie : BaseEntity
{
    public Transform player;
    public float speed = 2f;
    public float attackDistance = 1.5f;
    private Transform zombiePos;

    enum State { Chase, Attack }
    State currentState;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        zombiePos = gameObject.GetComponent<Transform>();
    }

    void OnEnable()
    {
        transform.localRotation = quaternion.identity;
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > attackDistance)
            currentState = State.Chase;
        else
            currentState = State.Attack;

        switch (currentState)
        {
            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void Chase()
    {
        float dirX = Mathf.Sign(player.position.x - transform.position.x);


        animator.Play("ZombieWalk");


        if (dirX >= 0)
            zombiePos.localScale = new Vector3(1, 1, 1);
        else
            zombiePos.localScale = new Vector3(-1, 1, 1);

        transform.position += new Vector3(dirX * speed * Time.deltaTime, 0f, 0f);
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        animator.Play("ZombieAttack");
    }
}
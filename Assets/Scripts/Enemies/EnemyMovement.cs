using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    Vector2 knockBackVelocty;
    float knockBackDuration;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        if(knockBackDuration > 0)
        {
            transform.position += (Vector3)knockBackVelocty * Time.deltaTime;
            knockBackDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
    }

    public void KnockBack(Vector2 velocity, float duration)
    {
        if (knockBackDuration > 0) return;

        knockBackVelocty = velocity;
        knockBackDuration = duration;
    }
}

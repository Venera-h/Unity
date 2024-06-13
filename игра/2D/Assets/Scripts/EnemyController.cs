using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    [SerializeField] float speed;

    [SerializeField] List<Transform> patrolPoints;
    int curPointIndex = 0;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[curPointIndex].position, speed * Time.deltaTime * 10);
        if (Vector2.Distance(transform.position, patrolPoints[curPointIndex].position) < 0.1f)
        {
            curPointIndex++;
            transform.localScale = new Vector2(-1, 1);
        }

        if (curPointIndex >= patrolPoints.Count)
        {
            curPointIndex = 0;
        }
    }

    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    public void DieEfect()
    {
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
        animator.SetTrigger("Die");
    }
}

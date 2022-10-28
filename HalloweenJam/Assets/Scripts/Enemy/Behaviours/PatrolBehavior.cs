using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviourHandler))]
public class PatrolBehavior : BehaviourBase
{
    [SerializeField] private PatrolPath patrolPath;

    private Vector3 currentPatrolTarget;
    private int currentPatrolIndex = -1;
    private int pathDirection = 1;

    private CharacterAgent characterAgent;
    private bool isActive;

    void Update()
    {
        if (!isActive) return;
        if(!characterAgent.AtDestination) return;

        GetNextPatrolPoint();
        characterAgent.MoveTo(currentPatrolTarget);
    }

    public override void StartBehaviour() {
        characterAgent = GetComponent<CharacterAgent>();
        GetClosesPatrolPoint();
        characterAgent.MoveTo(currentPatrolTarget);
        isActive = true;
    }

    public override void EndBehaviour() {
        characterAgent = null;
        isActive = false;
    }

    private void GetClosesPatrolPoint() {
        Vector3 nearest = Vector3.negativeInfinity;
        float nearestDist = float.PositiveInfinity;
        int nearestIndex = -1;

        for (int i = 0; i < patrolPath.PatrolPoints.Length; i++) {
            float dist = Vector3.Distance(transform.position, patrolPath.PatrolPoints[i]);
            if (dist < nearestDist) {
                nearest = patrolPath.PatrolPoints[i];
                nearestDist = dist;
                nearestIndex = i;
            }
        }

        currentPatrolTarget = nearest;
        currentPatrolIndex = nearestIndex;
    }

    private void GetNextPatrolPoint() {
        if(currentPatrolIndex == -1) {
            currentPatrolIndex = 0;
            currentPatrolTarget = patrolPath.PatrolPoints[currentPatrolIndex];
        }

        if(!patrolPath.IsLoop) {
            Debug.Log("wat");
            int tempIndex = currentPatrolIndex + pathDirection;
            if (tempIndex < 0 || tempIndex >= patrolPath.PatrolPoints.Length) {
                pathDirection *= -1;
            }
        }
        currentPatrolIndex = (currentPatrolIndex + pathDirection) % patrolPath.PatrolPoints.Length;
        currentPatrolTarget = patrolPath.PatrolPoints[currentPatrolIndex];
    }
}

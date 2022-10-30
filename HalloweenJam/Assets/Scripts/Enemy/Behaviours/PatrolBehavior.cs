using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviourHandler))]
[RequireComponent(typeof(CharacterAgent))]
public class PatrolBehavior : BehaviourBase
{
    [SerializeField] private PatrolPath patrolPath;

    private Vector3 currentPatrolTarget;
    private int currentPatrolIndex = -1;
    private int pathDirection = 1;

    private CharacterAgent characterAgent;

    public override void UpdateBehaviour() {
        if(!characterAgent.AtDestination) return;

        GetNextPatrolPoint();
        characterAgent.MoveTo(currentPatrolTarget);
    }

    public override void StartBehaviour() {
        characterAgent = GetComponent<CharacterAgent>();
        GetClosesPatrolPoint();
        characterAgent.MoveTo(currentPatrolTarget);
    }

    public override void EndBehaviour() {
        characterAgent.CancelCurrentCommand();
        characterAgent = null;
    }

    private void GetClosesPatrolPoint() {
        Vector3 nearest = patrolPath.PatrolPoints[0];
        float nearestDist = 0;
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
            int tempIndex = currentPatrolIndex + pathDirection;
            if (tempIndex < 0 || tempIndex >= patrolPath.PatrolPoints.Length) {
                pathDirection *= -1;
            }
        }
        currentPatrolIndex = (currentPatrolIndex + pathDirection) % patrolPath.PatrolPoints.Length;
        currentPatrolTarget = patrolPath.PatrolPoints[currentPatrolIndex];
    }
}

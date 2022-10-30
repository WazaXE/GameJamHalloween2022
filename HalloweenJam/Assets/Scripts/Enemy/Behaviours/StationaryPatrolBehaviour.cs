using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryPatrolBehaviour : BehaviourBase
{
    // ABORT FOR NOW

    [SerializeField] private float rotationCooldown;

    private CharacterAgent agent;
    private Vector3 patrolPosition;
    private bool canRotate;

    private void Start() {
        patrolPosition = transform.position;
    }

    public override void UpdateBehaviour() {
        
    }

    public override void StartBehaviour() {
        agent = GetComponent<CharacterAgent>();
        agent.MoveTo(patrolPosition);
    }

    public override void EndBehaviour() {
        agent = null;
    }

    private IEnumerator RotateCooldown() {
        canRotate = false;
        yield return new WaitForSeconds(rotationCooldown);
        canRotate = true;
    }
}

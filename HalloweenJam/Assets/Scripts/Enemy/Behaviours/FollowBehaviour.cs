using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Vision))]
[RequireComponent(typeof(EnemyBehaviourHandler))]
[RequireComponent(typeof(CharacterAgent))]
public class FollowBehaviour : BehaviourBase
{
    private CharacterAgent characterAgent;
    private Vision vision;
    private NavMeshAgent nvAgent;
    private bool isActive;

    void Update()
    {
        if (!isActive) return;

        characterAgent.MoveTo(vision.IdentifiedTarget.Position.position);
    }

    public override void StartBehaviour() {
        characterAgent = GetComponent<CharacterAgent>();
        vision = GetComponent<Vision>();
        nvAgent = GetComponent<NavMeshAgent>();
        isActive = true;
    }

    public override void EndBehaviour() {
        characterAgent.CancelCurrentCommand();

        characterAgent = null;
        vision = null;
        nvAgent = null;
        isActive = false;

    }
}

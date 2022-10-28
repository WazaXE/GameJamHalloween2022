using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EOffmeshLinkStatus {
    NotStarted,
    InProgress
}

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAgent : MonoBehaviour {
    [SerializeField] private float nearestPointSearchRange = 5f;

    public bool IsMoving => agent.velocity.magnitude > float.Epsilon;
    public bool AtDestination => reachedDestination;

    private NavMeshAgent agent;
    private bool destinationSet = false;
    private bool reachedDestination = false;
    private EOffmeshLinkStatus offmeshLinkStatus = EOffmeshLinkStatus.NotStarted;

    void OnEnable() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (!agent.pathPending && !agent.isOnOffMeshLink && destinationSet && (agent.remainingDistance <= agent.stoppingDistance)) {
            destinationSet = false;
            reachedDestination = true;
        }

        if (agent.isOnOffMeshLink) {
            if (offmeshLinkStatus == EOffmeshLinkStatus.NotStarted)
                StartCoroutine(FollowOffmeshLink());
        }
    }

    private IEnumerator FollowOffmeshLink() {
        offmeshLinkStatus = EOffmeshLinkStatus.InProgress;
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Vector3 newPosition = transform.position;
        while (!Mathf.Approximately(Vector3.Distance(newPosition, agent.currentOffMeshLinkData.endPos), 0f)) {
            newPosition = Vector3.MoveTowards(transform.position, agent.currentOffMeshLinkData.endPos, agent.speed * Time.deltaTime);
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }

        offmeshLinkStatus = EOffmeshLinkStatus.NotStarted;
        agent.CompleteOffMeshLink();

        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.updateUpAxis = true;
    }

    /// <summary>
    /// Picks a random location on the NavMesh based on the agents position
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public Vector3 PickLocationInRange(float range) {
        return PickLocationInRange(transform.position, range);
    }
    /// <summary>
    /// Picks a random location on the NavMesh based on the provided position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public Vector3 PickLocationInRange(Vector3 position, float range) {
        Vector3 searchLocation = position;
        searchLocation += UnityEngine.Random.Range(-range, range) * Vector3.forward;
        searchLocation += UnityEngine.Random.Range(-range, range) * Vector3.right;

        NavMeshHit hitResult;
        if (NavMesh.SamplePosition(searchLocation, out hitResult, nearestPointSearchRange, NavMesh.AllAreas)) {
            return hitResult.position;
        }

        return position;
    }

    public virtual void CancelCurrentCommand() {
        agent.ResetPath();

        destinationSet = false;
        reachedDestination = false;
        offmeshLinkStatus = EOffmeshLinkStatus.NotStarted;
    }

    public virtual void MoveTo(Vector3 destination) {
        CancelCurrentCommand();
        Debug.Log("Got destination" + destination);
        SetDestination(destination);
    }

    public virtual void SetDestination(Vector3 destination) {
        NavMeshHit hitResult;
        if (NavMesh.SamplePosition(destination, out hitResult, nearestPointSearchRange, NavMesh.AllAreas)) {
            agent.SetDestination(hitResult.position);
            destinationSet = true;
            reachedDestination = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviourHandler))]
public class Vision : MonoBehaviour
{
    [SerializeField] private float visionRange;
    [SerializeField] private float visionAngle;
    [SerializeField] private float attackRange;

    IVisionReport behaviourHandler;

    void Start()
    {
        behaviourHandler = GetComponent<EnemyBehaviourHandler>();
    }

    private void Update() {
        for (int i = 0; i < DetectableTargetManager.Instance.detectableTargets.Count; i++) {
            ITarget target = DetectableTargetManager.Instance.detectableTargets[i];
            if (target == behaviourHandler) continue;
            if (!IsInRange(target.Position.position, visionRange)) continue;
            if (!IsInViewArea(target.Position.position)) continue;

            behaviourHandler.ReportCanSeeTarget(target);
            if(IsInRange(target.Position.position, attackRange)) {
                behaviourHandler.ReportIsInAttackRange(target);
            }
        }
    }

    private bool IsInRange(Vector3 target, float range) {
        return Vector3.Distance(transform.position, target) <= range;
    }

    private bool IsInViewArea(Vector3 target) {
        Vector3 relativeTarget = target - transform.position;
        Vector2 relativeTarget2D = new Vector2(relativeTarget.x, relativeTarget.z);

        Vector2 leftViewEdge, rightViewEdge;
        GetViewEdges(out leftViewEdge, out rightViewEdge);

        Vector2 leftLimitNormal = new Vector2(-leftViewEdge.y, leftViewEdge.x);
        Vector2 rightLimitNormal = new Vector2(-rightViewEdge.y, rightViewEdge.x);

        return Vector2.Dot(leftLimitNormal, relativeTarget2D) < 0 && Vector2.Dot(rightLimitNormal, relativeTarget2D) > 0;
    }

    private void GetViewEdges(out Vector2 left, out Vector2 right) {
        float forwardAngle = Mathf.Atan2(transform.forward.z, transform.forward.x);

        Vector2 leftViewEdge = new Vector2(
            Mathf.Cos(forwardAngle + visionAngle * Mathf.Deg2Rad / 2f),
            Mathf.Sin(forwardAngle + visionAngle * Mathf.Deg2Rad / 2f)
        );
        Vector2 rightViewEdge = new Vector2(
            Mathf.Cos(forwardAngle - visionAngle * Mathf.Deg2Rad / 2f),
            Mathf.Sin(forwardAngle - visionAngle * Mathf.Deg2Rad / 2f)
        );

        left = leftViewEdge;
        right = rightViewEdge;
    }

    private void OnDrawGizmosSelected() {
        Vector2 leftViewEdge, rightViewEdge;
        GetViewEdges(out leftViewEdge, out rightViewEdge);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(leftViewEdge.x, 0, leftViewEdge.y) * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(rightViewEdge.x, 0, rightViewEdge.y) * visionRange);
    }
}

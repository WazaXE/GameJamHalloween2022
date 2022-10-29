using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [Header("Collection Movement")]
    [SerializeField] private float collectionSpeed;
    [SerializeField] private float archHeight;

    private bool collected = false;

    public UnityAction<Collectable> OnCollected;

    public void Collect(Transform movementTarget) {
        if(collected) return;
        collected = true;

        Collider c = GetComponent<Collider>();
        if(c != null) c.enabled = false;

        StartCoroutine(CollectionMovement(movementTarget));
    }

    private IEnumerator CollectionMovement(Transform target) {
        float t = 0;

        MovementCurve movementCurve = new MovementCurve(transform.position, target.position, archHeight);

        while (t <= 1) {
            movementCurve.UpdateCurve(target.position);

            Vector3 pos = movementCurve.Evaluate(t);
            transform.position = pos;

            t += collectionSpeed * Time.deltaTime;
            yield return null;
        }

        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }

    private struct MovementCurve {
        private Vector3 startPos, endPos;
        private Vector3 arcPoint;
        private float arcHeight;

        public MovementCurve(Vector3 startPosition, Vector3 endPosition, float arcHeight) {
            startPos = startPosition;
            endPos = endPosition;
            this.arcHeight = arcHeight;

            arcPoint = Vector3.Lerp(startPosition, endPosition, 0.5f);
            arcPoint.y += arcHeight;
        }

        public void UpdateCurve(Vector3 targetPosition) {
            endPos = targetPosition;
            arcPoint = Vector3.Lerp(startPos, endPos, 0.5f);
            arcPoint.y += arcHeight;
        }

        public Vector3 Evaluate(float t) {
            return arcPoint + (1 - t) * (1 - t) * (startPos - arcPoint) + t * t * (endPos - arcPoint);
        }
    }
}

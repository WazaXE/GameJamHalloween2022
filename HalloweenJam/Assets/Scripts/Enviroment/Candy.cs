using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [SerializeField] private int value;

    [Header("Collection Movement")]
    [SerializeField] private float collectionSpeed;
    [SerializeField] private float archHeight;

    private bool collected = false;

    private void OnTriggerEnter(Collider other) {
        if (collected) return;
        CandyHandler candyHandler = other.GetComponent<CandyHandler>();
        if (candyHandler == null) return;
        collected = true;

        StartCoroutine(CollectionMovement(candyHandler, candyHandler.transform));
    }

    private IEnumerator CollectionMovement(CandyHandler candyHandler, Transform target) {
        float t = 0;

        MovementCurve movementCurve = new MovementCurve(transform.position, target.position, archHeight);

        while(t <= 1) {
            movementCurve.UpdateCurve(target.position);

            Vector3 pos = movementCurve.Evaluate(t);
            transform.position = pos;

            t += collectionSpeed * Time.deltaTime;
            yield return null;
        }

        candyHandler.AddCandy(value);
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

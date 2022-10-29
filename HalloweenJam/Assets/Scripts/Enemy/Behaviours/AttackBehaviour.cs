using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : BehaviourBase
{
    [SerializeField] private float candyStealRate;
    [SerializeField] private float stealAmount;

    private Vision vision;
    private IAttackable target;
    private bool canAttack;
    private bool isActive;

    private void Start() {
        canAttack = true;
    }

    private void Update() {
        if (!isActive) return;
        if (!canAttack) return;
        StartCoroutine(AttackCooldown());
        target.Attack(stealAmount);
    }

    public override void StartBehaviour() {
        vision = GetComponent<Vision>();
        target = (IAttackable)vision.IdentifiedTarget;

        isActive = true;
    }

    public override void EndBehaviour() {
        vision = null;
        target = null;

        isActive = false;
    }

    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(candyStealRate);
        canAttack = true;
    }
}

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

    private void Start() {
        canAttack = true;
    }

    public override void UpdateBehaviour() {
        if (!canAttack) return;
        StartCoroutine(AttackCooldown());
        target.Attack(stealAmount);
    }

    public override void StartBehaviour() {
        vision = GetComponent<Vision>();
        target = (IAttackable)vision.IdentifiedTarget;
    }

    public override void EndBehaviour() {
        vision = null;
        target = null;
    }

    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(candyStealRate);
        canAttack = true;
    }
}

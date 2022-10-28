using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourHandler : MonoBehaviour, ITarget, IVisionReport
{
    [SerializeField] private Faction faction;
    [Space(15)]
    [SerializeField] private BehaviourBase patrolBehaviour;
    [SerializeField] private BehaviourBase followBehaviour;
    [SerializeField] private BehaviourBase attackBehaviour;

    private BehaviourBase activeBehaviour;

    public Transform Position => transform;
    public Faction Faction => faction;

    void Start()
    {
        SwitchBehaviour(patrolBehaviour);
        DetectableTargetManager.Instance.RegisterTarget(this);
    }
    private void OnDestroy() {
        DetectableTargetManager.Instance.DeregisterTarget(this);
    }

    public void ReportCanSeeTarget() {
        SwitchBehaviour(followBehaviour);
        Debug.Log("Can see target!");
    }
    public void ReportLostTarget() {
        SwitchBehaviour(patrolBehaviour);
        Debug.Log("Lost target!");
    }
    public void ReportIsInAttackRange() {
        SwitchBehaviour(attackBehaviour);
        Debug.Log("Attack target!");
    }

    private void SwitchBehaviour(BehaviourBase newBehaviour) {
        if (newBehaviour == null || activeBehaviour == newBehaviour) return;
        if(activeBehaviour != null) activeBehaviour.EndBehaviour();
        activeBehaviour = newBehaviour;
        activeBehaviour.StartBehaviour();
    }
}

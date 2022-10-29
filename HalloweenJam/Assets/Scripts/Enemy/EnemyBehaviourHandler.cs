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
        if (activeBehaviour == followBehaviour) return;
        SwitchBehaviour(followBehaviour);
        // Debug.Log("Can see target!");
    }
    public void ReportLostTarget() {
        if (activeBehaviour == patrolBehaviour) return;
        SwitchBehaviour(patrolBehaviour);
        // Debug.Log("Lost target!");
    }
    public void ReportIsInAttackRange() {
        if (activeBehaviour == attackBehaviour) return;
        SwitchBehaviour(attackBehaviour);
        // Debug.Log("Attack target!");
    }

    public void ReportLeftAttackRange() {
        if(activeBehaviour == attackBehaviour) {
            SwitchBehaviour(followBehaviour);
        }
    }

    private void SwitchBehaviour(BehaviourBase newBehaviour) {
        if (newBehaviour == null || newBehaviour == activeBehaviour) return;
        if(activeBehaviour != null) activeBehaviour.EndBehaviour();
        activeBehaviour = newBehaviour;
        activeBehaviour.StartBehaviour();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, ITarget
{
    [SerializeField] private Faction faction;

    public Transform Position => transform;
    public Faction Faction => faction;

    void Start()
    {
        DetectableTargetManager.Instance.RegisterTarget(this);
    }
    private void OnDestroy() {
        DetectableTargetManager.Instance.DeregisterTarget(this);
    }

    void Update()
    {
        
    }
}

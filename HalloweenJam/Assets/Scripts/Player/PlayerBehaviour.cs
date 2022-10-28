using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBehaviour : MonoBehaviour, ITarget
{
    [SerializeField] private Faction faction;
    [SerializeField] Animator animator;

    public Transform Position => transform;
    public Faction Faction => faction;

    void Start()
    {
        DetectableTargetManager.Instance.RegisterTarget(this);
        GetComponent<PlayerMovement>().PlayerMoving += PlayerMoving;
    }
    private void OnDestroy() {
        DetectableTargetManager.Instance.DeregisterTarget(this);
    }

    void Update()
    {
        
    }

    public void PlayerMoving(bool walking) {
        animator.SetBool("walking", walking);
    }
}

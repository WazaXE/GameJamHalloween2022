using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBehaviour : MonoBehaviour, ITarget, IAttackable
{
    [SerializeField] private Faction faction;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystem loseCandyParticle;
    [SerializeField] private CandyHandler candyHandler;

    [Header("Attacked")]
    [SerializeField] private float invulnerabilityTime;

    private bool isInvulnerable;

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

    public void PlayerMoving(bool walking) {
        animator.SetBool("walking", walking);

        if (walking)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }

    }

    public void Attack(float Damage) {
        if (isInvulnerable) return;
        StartCoroutine(InvulnerableCooldown());
        // damage
        candyHandler.RemoveCandy(Damage);
        loseCandyParticle.Play();
        Debug.Log("AAAAH HELP");
    }

    private IEnumerator InvulnerableCooldown() {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitPortal : MonoBehaviour
{
    public UnityAction OnPortalExit;

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        OnPortalExit?.Invoke();
    }
}

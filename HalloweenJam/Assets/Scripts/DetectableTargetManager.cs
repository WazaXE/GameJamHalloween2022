using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetManager : MonoBehaviour
{
    public static DetectableTargetManager Instance { get; private set; }
    public List<ITarget> detectableTargets { get; private set; } = new List<ITarget>();

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Multiple DetectableTargetManager found. Destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterTarget(ITarget target) {
        detectableTargets.Add(target);
    }
    public void DeregisterTarget(ITarget target) {
        detectableTargets.Remove(target);
    }
}

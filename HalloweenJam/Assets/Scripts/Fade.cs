using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Color endColor;
    [SerializeField] private Image image;

    public UnityEvent OnFadeDone;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(Fader());
    }

    private IEnumerator Fader() {
        float t = 0;
        while (t <= 1) {
            t += Time.deltaTime / fadeTime;
            Color c = image.color;
            c.a = t;
            image.color = c;
            yield return null;
        }

        OnFadeDone?.Invoke();
    }
}

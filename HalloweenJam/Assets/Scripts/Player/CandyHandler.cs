using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class CandyHandler : MonoBehaviour
{
    [SerializeField] private int maxCandy;
    [SerializeField] private int startCandy;

    private float candyAmount;

    public int CandyCount {
        get {
            return Mathf.RoundToInt(candyAmount); 
        }
    }
    public int MaxCandyAmount => maxCandy;

    public UnityAction OnCandyAmountChange;
    public UnityAction OnNoRemainingCandy;

    private void Start() {
        candyAmount = 0;

        AddCandy(startCandy);
    }

    public void AddCandy(float amount) {
        if (candyAmount >= maxCandy) return;

        if (candyAmount + amount > maxCandy) {
            candyAmount = maxCandy;
        }
        else {
            candyAmount += amount;
        }

        OnCandyAmountChange?.Invoke();
    }

    public void RemoveCandy(float amount) {
        candyAmount -= amount;
        OnCandyAmountChange?.Invoke();

        if (candyAmount <= 0) {
            OnNoRemainingCandy?.Invoke();

            if(candyAmount < 0) {
                candyAmount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Candy candy = other.GetComponent<Candy>();
        if (candy == null) return;
        candy.Collect(transform);
        candy.OnCollected += OnCandyCollected;
    }

    private void OnCandyCollected(Collectable collectable) {
        Candy c = (Candy)collectable;
        AddCandy(c.Value);
    }
}

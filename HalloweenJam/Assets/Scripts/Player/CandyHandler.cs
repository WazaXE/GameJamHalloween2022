using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CandyHandler : MonoBehaviour
{
    [SerializeField] private int maxCandy;

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

        AddCandy(0);
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
}

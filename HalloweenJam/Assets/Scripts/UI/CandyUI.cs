using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CandyUI : MonoBehaviour
{
    [SerializeField] private CandyHandler handler;
    [SerializeField] private TMP_Text candyUiText;

    void Awake()
    {
        handler.OnCandyAmountChange += UpdateUI;
    }

    private void UpdateUI() {
        candyUiText.text = $"Candy {handler.CandyCount}/{handler.MaxCandyAmount}";
    }
}

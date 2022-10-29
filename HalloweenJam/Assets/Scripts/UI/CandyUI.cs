using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CandyUI : MonoBehaviour
{
    [SerializeField] private CandyHandler playerCandyHandler;
    [SerializeField] private TMP_Text candyUiText;

    void Awake()
    {
        playerCandyHandler.OnCandyAmountChange += UpdateUI;
    }

    private void UpdateUI() {
        candyUiText.text = $"Candy {playerCandyHandler.CandyCount}/{playerCandyHandler.MaxCandyAmount}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEnder : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fadeoutImage;
    [SerializeField] private GameObject textParent;
    [SerializeField] private GameObject levelCompleteText;
    [SerializeField] private GameObject gameOvertext;
    [SerializeField] private TMP_Text candyCollectedText;
    [Space(15)]
    [SerializeField] private float fadeoutTime;
    [Space(15)]
    [SerializeField] private CandyHandler candyHandler;

    void Start()
    {
        candyHandler.OnNoRemainingCandy += GameOver;
    }

    public void LevelComplete() {
        levelCompleteText.SetActive(true);
        StartCoroutine(Fadeout());
    }
    public void GameOver() {
        gameOvertext.SetActive(true);
        StartCoroutine(Fadeout());
    }


    private void EndLevel() {
        GameStateManager.Instance.SetState(GameState.Paused);

        textParent.SetActive(true);

        string collectedCandyResult = $"{candyHandler.CandyCount}/{candyHandler.MaxCandyAmount} Candy";
        candyCollectedText.text = collectedCandyResult;
    }

    private IEnumerator Fadeout() {
        float t = 0;
        while (t <= 1) {
            t += Time.deltaTime / fadeoutTime;
            Color c = fadeoutImage.color;
            c.a = t;
            fadeoutImage.color = c;
            yield return null;
        }
        EndLevel();
    }
}

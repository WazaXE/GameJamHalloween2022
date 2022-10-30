using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelEnder : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fadeoutImage;
    [SerializeField] private GameObject textParent;
    [SerializeField] private GameObject levelCompleteText;
    [SerializeField] private GameObject gameOvertext;
    [SerializeField] private TMP_Text candyCollectedText;
    [Space(15)]
    [SerializeField] private GameObject buttons;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button quitButton;
    [Space(15)]
    [SerializeField] private float fadeoutTime;
    [Space(15)]
    [SerializeField] private CandyHandler candyHandler;
    [SerializeField] private ExitPortal exitPortal;

    void Start()
    {
        candyHandler.OnNoRemainingCandy += GameOver;
        exitPortal.OnPortalExit += LevelComplete;
    }

    public void LevelComplete() {
        levelCompleteText.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
        SetupNavigation(nextLevelButton);
        
        StartCoroutine(Fadeout());
    }
    public void GameOver() {
        gameOvertext.SetActive(true);
        restartButton.gameObject.SetActive(true);
        SetupNavigation(restartButton);

        StartCoroutine(Fadeout());
    }

    private void SetupNavigation(Button target) {
        Navigation menuNavigation = menuButton.navigation;
        menuNavigation.selectOnUp = target;
        menuButton.navigation = menuNavigation;
        Navigation quitNavigation = quitButton.navigation;
        quitNavigation.selectOnDown = target;
        quitButton.navigation = quitNavigation;

        EventSystem.current.SetSelectedGameObject(target.gameObject);
    }

    private void EndLevel() {
        GameStateManager.Instance.SetState(GameState.Paused);

        textParent.SetActive(true);
        buttons.SetActive(true);

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

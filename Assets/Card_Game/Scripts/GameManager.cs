using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TMP_Text finalScoreText;
    [SerializeField] private ScorePopup scorePopupPrefab;
    [SerializeField] private RectTransform popupParent;

    private int comboCount = 0;

    private List<Card> flippedCards = new List<Card>();
    private List<Card> allCards;

    private int score;
    private int rows = 4;
    private int cols = 4;
    public int Rows => rows;
    public int Cols => cols;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        if (SaveSystem.HasSave())
        {
            LoadGame();
        }
        else
        {
            StartNewGame(4, 4);
        }
    }

    public void StartNewGame(int r, int c)
    {
        rows = r;
        cols = c;

        // Debug.Log($" Starting new game {rows}x{cols}");

        score = 0;
        flippedCards.Clear();

        allCards = gridManager.SpawnGrid(rows, cols);

        foreach (Card card in allCards)
            card.OnCardFlipped += HandleCardFlip;
        UpdateScoreUI();
        SaveSystem.Clear();
    }

    private void HandleCardFlip(Card card)
    {
        // Debug.Log($" Card flipped → ID: {card.cardId}");

        if (flippedCards.Count >= 2)
        {
            Debug.LogWarning("Ignored flip: already 2 cards flipped");
            return;
        }

        flippedCards.Add(card);

        if (flippedCards.Count == 2)
            CheckMatch();
    }

    private void CheckMatch()
    {
        Card a = flippedCards[0];
        Card b = flippedCards[1];

        // Debug.Log($" Checking match: {a.cardId} vs {b.cardId}");

        if (a.cardId == b.cardId)
        {
            comboCount++;

            int comboBonus = comboCount * 5;  
            int points = 10 + comboBonus;

            // Debug.Log($"Match! Combo x{comboCount} | +{points}");

            a.SetMatched();
            b.SetMatched();
            score += points;
            UpdateScoreUI();
            ShowScorePopup(points);
            CheckGameOver();
        }
        else
        {
            // Debug.Log(" Mismatch → combo reset");

            comboCount = 0;
            Invoke(nameof(FlipBackCards), 0.6f);
        }

        flippedCards.Clear();
        SaveSystem.Save(allCards, rows, cols, score);
    }


    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
    private void FlipBackCards()
    {
        // Debug.Log("Flipping back unmatched cards");

        foreach (Card card in allCards)
        {
            if (card.IsFlipped && !card.IsMatched)
                card.FlipBack();
        }
    }

    private void LoadGame()
    {
        // Debug.Log(" Loading saved game");

        SaveData data = SaveSystem.Load();

        rows = data.rows;
        cols = data.cols;
        score = data.score;

        allCards = gridManager.SpawnGrid(rows, cols);

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].cardId = data.cardIds[i];

            if (data.matched[i])
                allCards[i].SetMatched();

            allCards[i].OnCardFlipped += HandleCardFlip;
        }

        // Debug.Log($"Game loaded | Score: {score}");
    }
    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {score}";
    }
    private void ShowScorePopup(int amount)
    {
        if (scorePopupPrefab == null || popupParent == null)
            return;

        ScorePopup popup = Instantiate(scorePopupPrefab, popupParent);
        RectTransform popupRect = popup.GetComponent<RectTransform>();
        popupRect.anchoredPosition = scoreText.rectTransform.anchoredPosition;

        popup.Play(amount);
    }

    private void CheckGameOver()
    {
        foreach (Card card in allCards)
        {
            if (!card.IsMatched)
                return;
        }

        // Debug.Log("GAME OVER");

        Invoke(nameof(ShowGameOver), 2f);
    }

}

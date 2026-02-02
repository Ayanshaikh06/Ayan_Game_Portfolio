using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GridManager gridManager;

    private List<Card> flippedCards = new List<Card>();
    private List<Card> allCards;

    private int score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Start fresh game for now
        StartNewGame(4, 4);
    }

    public void StartNewGame(int rows, int cols)
    {
        score = 0;
        flippedCards.Clear();

        allCards = gridManager.SpawnGrid(rows, cols);

        foreach (Card card in allCards)
            card.OnCardFlipped += HandleCardFlip;
    }

    private void HandleCardFlip(Card card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count >= 2)
            CheckMatch();
    }

    private void CheckMatch()
    {
        Card a = flippedCards[0];
        Card b = flippedCards[1];

        if (a.cardId == b.cardId)
        {
            a.SetMatched();
            b.SetMatched();
            score += 10;
        }
        else
        {
            Invoke(nameof(FlipBackCards), 0.6f);
        }

        flippedCards.Clear();
    }

    private void FlipBackCards()
    {
        foreach (Card card in allCards)
        {
            if (card.IsFlipped && !card.IsMatched)
                card.FlipBack();
        }
    }
}

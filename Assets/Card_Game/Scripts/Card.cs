using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{
    public int cardId;

    public bool IsFlipped { get; private set; }
    public bool IsMatched { get; private set; }

    public event Action<Card> OnCardFlipped;

    [SerializeField] private Button button;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (IsMatched || IsFlipped)
        {
            Debug.Log($"[Card] Click ignored | ID: {cardId}");
            return;
        }

        Debug.Log($"[Card] Clicked | ID: {cardId}");

        FlipFront();
        IsFlipped = true;

        OnCardFlipped?.Invoke(this);
    }

    public void SetMatched()
    {
        IsMatched = true;
        IsFlipped = true; // explicitly keep it flipped
        Debug.Log($"[Card] Card matched | ID: {cardId}");
        AudioManager.Instance.PlayMatch();
    }



    public void FlipFront()
    {
        animator.SetTrigger("FlipFront");
        AudioManager.Instance.PlayFlip();
    }

    public void FlipBack()
    {
        animator.SetTrigger("FlipBack");
        IsFlipped = false;
        AudioManager.Instance.PlayMismatch();
    }

}

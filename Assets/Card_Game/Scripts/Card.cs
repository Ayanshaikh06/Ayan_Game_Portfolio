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
            return;

        FlipFront();
        IsFlipped = true;

        OnCardFlipped?.Invoke(this);
    }

    public void FlipFront()
    {
        animator.SetTrigger("FlipFront");
    }

    public void FlipBack()
    {
        animator.SetTrigger("FlipBack");
        IsFlipped = false;
        
    }

    public void SetMatched()
    {
        IsMatched = true;
    }
}

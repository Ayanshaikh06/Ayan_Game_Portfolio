using UnityEngine;
using TMPro;
using System.Collections;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float moveUpDistance = 60f;
    [SerializeField] private float duration = 1f;

    private CanvasGroup canvasGroup;
    private Vector3 startPos;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = transform.localPosition;
    }

    public void Play(int amount)
    {
        text.text = $"+{amount}";
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.localPosition = startPos + Vector3.up * moveUpDistance * t;
            canvasGroup.alpha = 1 - t;

            yield return null;
        }

        Destroy(gameObject);
    }
}

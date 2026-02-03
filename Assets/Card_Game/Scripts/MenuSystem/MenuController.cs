using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverPanel;

    public void OnStartClicked()
    {
        // Debug.Log("Start clicked");

        startPanel.SetActive(false);
        selectPanel.SetActive(true);
        gameUI.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void StartGame_2x2()
    {
        StartGame(2, 2);
    }

    public void StartGame_3x3()
    {
        StartGame(3, 3);
    }

    public void StartGame_5x6()
    {
        StartGame(5, 6);
    }

    private void StartGame(int rows, int cols)
    {
        // Debug.Log($"Starting game {rows}x{cols}");

        startPanel.SetActive(false);
        selectPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);

        GameManager.Instance.StartNewGame(rows, cols);
    }

    public void RestartSameGrid()
    {
        // Debug.Log("Restart same grid");

        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);

        GameManager.Instance.StartNewGame(
            GameManager.Instance.Rows,
            GameManager.Instance.Cols
        );
    }

    public void BackToMenuFromGameOver()
    {
        // Debug.Log("Back to menu from Game Over");

        gameOverPanel.SetActive(false);
        gameUI.SetActive(false);
        selectPanel.SetActive(false);
        startPanel.SetActive(true);
    }


}

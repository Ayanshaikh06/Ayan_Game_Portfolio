using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverPanel;

    // -------------------------
    // START FLOW
    // -------------------------

    // Start → Select grid size
    public void OnStartClicked()
    {
        Debug.Log("[Menu] Start clicked");

        startPanel.SetActive(false);
        selectPanel.SetActive(true);
        gameUI.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    // -------------------------
    // GRID SELECTION
    // -------------------------

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
        Debug.Log($"[Menu] Starting game {rows}x{cols}");

        startPanel.SetActive(false);
        selectPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);

        GameManager.Instance.StartNewGame(rows, cols);
    }

    // -------------------------
    // GAME OVER ACTIONS
    // -------------------------

    public void RestartSameGrid()
    {
        Debug.Log("[Menu] Restart same grid");

        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);

        GameManager.Instance.StartNewGame(
            GameManager.Instance.Rows,
            GameManager.Instance.Cols
        );
    }

    public void BackToMenuFromGameOver()
    {
        Debug.Log("[Menu] Back to menu from Game Over");

        gameOverPanel.SetActive(false);
        gameUI.SetActive(false);
        selectPanel.SetActive(false);
        startPanel.SetActive(true);
    }


}

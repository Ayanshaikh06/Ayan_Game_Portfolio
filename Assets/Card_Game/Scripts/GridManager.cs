using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform container;

    void Start()
    {

    }
    public List<Card> SpawnGrid(int rows, int cols)
    {
        Debug.Log($"[GridManager] Spawning grid {rows}x{cols}");
        ClearGrid();

        int total = rows * cols;
        List<int> ids = GenerateIds(total);

        ScaleGrid(rows, cols);

        List<Card> cards = new List<Card>();

        foreach (int id in ids)
        {
            Card card = Instantiate(cardPrefab, grid.transform);
            card.cardId = id;
            cards.Add(card);
        }
        Debug.Log($"[GridManager] Spawned {cards.Count} cards");
        return cards;
    }

    private List<int> GenerateIds(int total)
    {
        Debug.Log($"[GridManager] Generating IDs for {total} cards");
        List<int> ids = new List<int>();
        for (int i = 0; i < total / 2; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        for (int i = 0; i < ids.Count; i++)
        {
            int rnd = Random.Range(0, ids.Count);
            (ids[i], ids[rnd]) = (ids[rnd], ids[i]);
        }
        Debug.Log("[GridManager] IDs shuffled");
        return ids;
    }

    private void ScaleGrid(int rows, int cols)
    {
        float spacingX = grid.spacing.x;
        float spacingY = grid.spacing.y;

        float width = container.rect.width - spacingX * (cols - 1);
        float height = container.rect.height - spacingY * (rows - 1);

        float size = Mathf.Floor(Mathf.Min(width / cols, height / rows));

        grid.cellSize = new Vector2(size, size);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;
    }


    private void ClearGrid()
    {
        foreach (Transform child in grid.transform)
            Destroy(child.gameObject);
    }
}

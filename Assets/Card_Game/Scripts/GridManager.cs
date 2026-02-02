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

        return cards;
    }

    private List<int> GenerateIds(int total)
    {
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

        return ids;
    }

    private void ScaleGrid(int rows, int cols)
    {
        float size = Mathf.Min(
            container.rect.width / cols,
            container.rect.height / rows
        );

        grid.cellSize = new Vector2(size, size);
    }

    private void ClearGrid()
    {
        foreach (Transform child in grid.transform)
            Destroy(child.gameObject);
    }
}

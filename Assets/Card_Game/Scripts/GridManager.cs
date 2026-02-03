using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private List<Sprite> cardSprites;
    private List<Sprite> activeSprites;
    void Start()
    {

    }
    public List<Card> SpawnGrid(int rows, int cols)
{
    ClearGrid();

    int maxSlots = rows * cols;

    // 🔒 FORCE EVEN CARD COUNT
    int totalCards = (maxSlots % 2 == 0) ? maxSlots : maxSlots - 1;
    int pairCount = totalCards / 2;

    if (cardSprites.Count < pairCount)
    {
        Debug.LogError(
            $"[GridManager] Not enough sprites. Needed {pairCount}, Found {cardSprites.Count}"
        );
        return null;
    }

    // Pick random sprites for this game
    PrepareSpritesForGame(pairCount);

    // Generate IDs ONLY for even count
    List<int> ids = GenerateIds(totalCards);

    ScaleGrid(rows, cols);

    List<Card> cards = new List<Card>();

    for (int i = 0; i < totalCards; i++)
    {
        Card card = Instantiate(cardPrefab, grid.transform);

        int id = ids[i];
        card.cardId = id;

        // SAFE: id always in range 0..pairCount-1
        card.SetFrontSprite(activeSprites[id]);

        cards.Add(card);
    }

    Debug.Log($"[GridManager] Spawned {totalCards} cards for {rows}x{cols}");
    return cards;
}




    private List<int> GenerateIds(int totalCards)
    {
        List<int> ids = new List<int>();

        int pairCount = totalCards / 2;

        // Normal pairs
        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        // 🔥 HANDLE ODD GRID (3x3)
        if (totalCards % 2 != 0)
        {
            ids.Add(-1); // special "free card"
            Debug.Log("[GridManager] Odd grid detected, adding free card");
        }

        // Shuffle
        for (int i = 0; i < ids.Count; i++)
        {
            int rnd = Random.Range(i, ids.Count);
            (ids[i], ids[rnd]) = (ids[rnd], ids[i]);
        }

        return ids;
    }

    private void PrepareSpritesForGame(int pairCount)
    {
        // Copy all available sprites
        List<Sprite> pool = new List<Sprite>(cardSprites);

        // Shuffle pool
        for (int i = 0; i < pool.Count; i++)
        {
            int rnd = Random.Range(i, pool.Count);
            (pool[i], pool[rnd]) = (pool[rnd], pool[i]);
        }

        // Take only what we need for this game
        activeSprites = pool.GetRange(0, pairCount);

        Debug.Log($"[GridManager] Selected {pairCount} random sprites for this game");
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

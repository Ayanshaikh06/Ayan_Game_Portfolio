using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    private const string KEY = "CARD_MATCH_SAVE";

    public static void Save(List<Card> cards, int rows, int cols, int score)
    {
        SaveData data = new SaveData
        {
            rows = rows,
            cols = cols,
            score = score,
            cardIds = new int[cards.Count],
            matched = new bool[cards.Count]
        };

        for (int i = 0; i < cards.Count; i++)
        {
            data.cardIds[i] = cards[i].cardId;
            data.matched[i] = cards[i].IsMatched;
        }

        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey(KEY);
    }

    public static SaveData Load()
    {
        return JsonUtility.FromJson<SaveData>(
            PlayerPrefs.GetString(KEY)
        );
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey(KEY);
    }
}

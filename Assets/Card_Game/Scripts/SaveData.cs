using System;

[Serializable]
public class SaveData
{
    public int rows;
    public int cols;
    public int score;

    public int[] cardIds;
    public bool[] matched;
}

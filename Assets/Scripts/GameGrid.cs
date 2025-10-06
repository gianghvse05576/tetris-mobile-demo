using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 20;
    public Transform[,] grid;

    void Awake()
    {
        grid = new Transform[width, height];
    }

    public bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < width &&
                (int)pos.y >= 0);
    }
}

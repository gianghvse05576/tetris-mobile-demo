using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public static Tetromino Instance;
    public RandomTetromino spawner;
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    private static int height = 20;
    private static int width = 10;
    private static Transform[,] grid = new Transform[width, height];

    void Start()
    {
        fallTime = GameManager.Instance.upLevel();
    }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if (!ValidMove()) transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if (!ValidMove()) transform.position += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 originalPosition = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.Rotate(0, 0, 90);

            if (ValidMove())
            {
                return;
            }

            bool adjusted = false;

            for (int i = 1; i <= 2; i++)
            {
                transform.position = originalPosition + new Vector3(-i, 0, 0);
                if (ValidMove())
                {
                    adjusted = true;
                    break;
                }

                transform.position = originalPosition + new Vector3(i, 0, 0);
                if (ValidMove())
                {
                    adjusted = true;
                    break;
                }
            }

            if (!adjusted)
            {
                transform.rotation = originalRotation;
                transform.position = originalPosition;
            }
        }


        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += Vector3.down;

            if (!ValidMove())
            {
                transform.position += Vector3.up;
                AddToGrid();
                this.enabled = false;
                DeleteGrid();
                fallTime = GameManager.Instance.upLevel();
                CheckGameOver();
            }

            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            if (x >= 0 && x < width && y >= 0 && y < height)
                grid[x, y] = child;
        }
    }

    void DeleteGrid()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsFullRow(y))
            {
                DeleteRow(y);
                MoveRowsDown(y + 1);
                y--;
                GameManager.Instance.AddScore(100);
            }
        }
    }

    bool IsFullRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void MoveRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.position += Vector3.down;
                }
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x < 0 || x >= width || y < 0)
                return false;

            if (y >= height)
                continue;

            if (grid[x, y] != null && grid[x, y].parent != transform)
                return false;
        }

        return true;
    }


    void CheckGameOver()
    {
        bool checkFull = false;

        for (int x = 0; x < width; x++)
        {
            if (grid[x, height - 1] != null)
            {
                checkFull = true;
                break;
            }
        }

        if (checkFull)
        {
            GameManager.Instance.GameOver();
            spawner.enabled = false;
        }
        else
        {
            spawner?.SpawnFromNext();
        }
    }
}

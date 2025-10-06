using UnityEngine;

public class RandomTetromino : MonoBehaviour
{
    [Header("Danh sách các khối Tetromino")]
    public GameObject[] tetrominoes; 

    [Header("Cài đặt spawn")]
    public float spawnInterval = 5f; 
    public Vector3 spawnPosition = new Vector3(5, 18, 0); 

    private float timer;

    void Start()
    {
        SpawnRandomTetromino(); 
        timer = spawnInterval;  
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomTetromino();
            timer = spawnInterval; 
        }
    }

    void SpawnRandomTetromino()
    {
        int index = Random.Range(0, tetrominoes.Length);
        Instantiate(tetrominoes[index], spawnPosition, Quaternion.identity);
    }
}
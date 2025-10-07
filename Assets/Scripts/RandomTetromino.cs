using UnityEngine;

public class RandomTetromino : MonoBehaviour
{
    public GameObject[] tetrominoes;

    public Vector3 spawnPosition = new Vector3(5, 18, 0);

    private GameObject currentTetromino;

    void Start()
    {
        SpawnRandomTetromino();
    }

    public void SpawnRandomTetromino()
    {
        int index = Random.Range(0, tetrominoes.Length);
        currentTetromino = Instantiate(tetrominoes[index], spawnPosition, Quaternion.identity);

        currentTetromino.GetComponent<Tetromino>().spawner = this;
    }
}

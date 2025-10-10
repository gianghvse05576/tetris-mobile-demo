using UnityEngine;

public class RandomTetromino : MonoBehaviour
{
    public GameObject[] tetrominoes;

    public Vector3 spawnPosition = new Vector3(5, 21, 0);

    public Transform nextSpawnPoint;

    private GameObject currentTetromino;
    private GameObject nextTetromino;

    void Start()
    {
        CreateNextTetromino();
        SpawnFromNext();
    }

    private void CreateNextTetromino()
    {
        int randomIndex = Random.Range(0, tetrominoes.Length);
        nextTetromino = Instantiate(tetrominoes[randomIndex], nextSpawnPoint.position, Quaternion.identity);
        nextTetromino.transform.localScale = Vector3.one * 0.5f;
        SetPreviewMode(nextTetromino, true);
    }

    public void SpawnFromNext()
    {
        currentTetromino = Instantiate(nextTetromino, spawnPosition, Quaternion.identity);
        currentTetromino.transform.localScale = Vector3.one;
        currentTetromino.GetComponent<Tetromino>().spawner = this;
        SetPreviewMode(currentTetromino, false);

        Destroy(nextTetromino);

        CreateNextTetromino();
    }

    private void SetPreviewMode(GameObject obj, bool isPreview)
    {
        var script = obj.GetComponent<Tetromino>();
        if (script != null) script.enabled = !isPreview;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;   // This should be set inside the Canvas
    public Canvas canvas;          // Reference to the UI Canvas
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 2f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnNote();
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnNote()
    {
        // Instantiate the note and set its parent to the Canvas
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, canvas.transform);

        // Adjust RectTransform position (localPosition for UI elements)
        RectTransform noteRectTransform = note.GetComponent<RectTransform>();
        noteRectTransform.anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;

        // Optionally, randomize the Y position to simulate notes on different lines of the pentagram
        float randomY = Random.Range(-50f, 50f);  // Adjust according to your pentagram height
        noteRectTransform.anchoredPosition += new Vector2(0, randomY);
    }
}

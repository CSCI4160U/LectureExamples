using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : MonoBehaviour {
    [Header("Generation Area")]
    [SerializeField] private float minimumX = -4f;
    [SerializeField] private float maximumX =  4f;
    [SerializeField] private float minimumY = -4f;
    [SerializeField] private float maximumY =  4f;

    [Header("Generation Params")]
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private int numBirdsToGenerate = 20;

    [ContextMenu("Generate")]
    public void Generate() {
        Debug.Log("Generating birds: " + numBirdsToGenerate);
        for (int i = 0; i < numBirdsToGenerate; i++) {
            // find a position for our new bird
            float x = Random.Range(minimumX, maximumX);
            float y = Random.Range(minimumY, maximumY);
            Vector3 position = new Vector3(x, y, 0f);

            // instantiate the bird prefab
            GameObject newBird = Instantiate(birdPrefab, position, Quaternion.identity);

            // re-parent the bird object
            newBird.transform.SetParent(transform);
        }
    }
}

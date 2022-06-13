using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Brick brick;

    public List<Material> materials;

    public Brick[,] brickMatrix;

    int bricksPerUnit = 5;

    private void Awake()
    {
        int bricksOnZAxis = (int)(bricksPerUnit * this.transform.localScale.z);
        int bricksOnXAxis = (int)(bricksPerUnit * this.transform.localScale.x);

        brickMatrix = new Brick[bricksOnZAxis, bricksOnXAxis];
        SpawnBricks(bricksOnZAxis, bricksOnXAxis);
    }

    private void SpawnBricks(int bricksOnZAxis, int bricksOnXAxis)
    {
        float width = this.transform.localScale.x * 10;
        float height = this.transform.localScale.z * 10;

        float left = this.transform.position.x - width / 2 + 1;
        float up = this.transform.position.z + height / 2 - 1;

        int[] colorsCount = new int[5];

        Transform bricksStashTransform = GameObject.Find("Bricks").transform;

        for (int i = 0; i < bricksOnZAxis; i++)
        {
            for (int j = 0; j < bricksOnXAxis; j++)
            {
                brickMatrix[i, j] = Instantiate(brick, 
                    new Vector3(left + j * width / bricksOnXAxis,
                        this.transform.position.y + 0.05f, 
                        up - i * height / bricksOnZAxis),
                    Quaternion.identity);
                brickMatrix[i, j].GetComponent<Renderer>().material.color = GetColor(colorsCount);
                brickMatrix[i, j].transform.parent = bricksStashTransform;
                brickMatrix[i, j].gameObject.SetActive(false);
            }
        }
    }

    private Color GetColor(int[] colorsCount)
    {
        int min = Mathf.Min(colorsCount);
        int rnd = Random.Range(0, materials.Count);
        while (colorsCount[rnd] != min)
        {
            rnd = Random.Range(0, materials.Count);
        }
        colorsCount[rnd]++;
        return materials[rnd].color;
    }
}

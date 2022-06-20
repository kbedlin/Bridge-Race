using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    //Prefab cegie³ki
    public Brick brick;
    //Lista materia³ów - kolorów graczy
    public List<Material> materials;
    //Macierz przechowuj¹ca cegie³ki
    public Brick[,] brickMatrix;
    //Iloœæ generowanych cegie³ek w jednym wierszu i w jednej kolumnie dla pod³ogi w skali (1, 1, 1) - 5 x 5
    int bricksPerUnit = 5;

    private void Awake()
    {
        //Ustala rozmiar macierzy i generuje cegie³ki
        int bricksOnZAxis = (int)(bricksPerUnit * this.transform.localScale.z);
        int bricksOnXAxis = (int)(bricksPerUnit * this.transform.localScale.x);

        brickMatrix = new Brick[bricksOnZAxis, bricksOnXAxis];
        SpawnBricks(bricksOnZAxis, bricksOnXAxis);
    }

    private void SpawnBricks(int bricksOnZAxis, int bricksOnXAxis)
    {
        //szerokoœæ i wysokoœæ pod³ogi
        float width = this.transform.localScale.x * 10;
        float height = this.transform.localScale.z * 10;

        //Lewy górny punkt, w którym zacznie siê generowanie cegie³ek
        float left = this.transform.position.x - width / 2 + 1;
        float up = this.transform.position.z + height / 2 - 1;
        //Tabela do zliczania iloœci wyst¹pieñ kolorów, ¿eby ka¿demu graczowi wylosowaæ tê sam¹ liczbê cegie³ek
        int[] colorsCount = new int[5];
        //Transformata pustego obiektu, który przechowuje wszystkie cegie³ki
        Transform bricksStashTransform = GameObject.Find("Bricks").transform;
        //Instancjonowanie cegie³ek na pod³odze, ich kolorów, ich rodzica i wy³¹czanie ich a¿ nie bêd¹ potrzebne
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
        //Losuje kolor na podstawie tabeli licz¹cej wyst¹pienia kolorów
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

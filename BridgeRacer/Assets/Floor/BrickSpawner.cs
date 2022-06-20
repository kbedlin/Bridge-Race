using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    //Prefab cegie�ki
    public Brick brick;
    //Lista materia��w - kolor�w graczy
    public List<Material> materials;
    //Macierz przechowuj�ca cegie�ki
    public Brick[,] brickMatrix;
    //Ilo�� generowanych cegie�ek w jednym wierszu i w jednej kolumnie dla pod�ogi w skali (1, 1, 1) - 5 x 5
    int bricksPerUnit = 5;

    private void Awake()
    {
        //Ustala rozmiar macierzy i generuje cegie�ki
        int bricksOnZAxis = (int)(bricksPerUnit * this.transform.localScale.z);
        int bricksOnXAxis = (int)(bricksPerUnit * this.transform.localScale.x);

        brickMatrix = new Brick[bricksOnZAxis, bricksOnXAxis];
        SpawnBricks(bricksOnZAxis, bricksOnXAxis);
    }

    private void SpawnBricks(int bricksOnZAxis, int bricksOnXAxis)
    {
        //szeroko�� i wysoko�� pod�ogi
        float width = this.transform.localScale.x * 10;
        float height = this.transform.localScale.z * 10;

        //Lewy g�rny punkt, w kt�rym zacznie si� generowanie cegie�ek
        float left = this.transform.position.x - width / 2 + 1;
        float up = this.transform.position.z + height / 2 - 1;
        //Tabela do zliczania ilo�ci wyst�pie� kolor�w, �eby ka�demu graczowi wylosowa� t� sam� liczb� cegie�ek
        int[] colorsCount = new int[5];
        //Transformata pustego obiektu, kt�ry przechowuje wszystkie cegie�ki
        Transform bricksStashTransform = GameObject.Find("Bricks").transform;
        //Instancjonowanie cegie�ek na pod�odze, ich kolor�w, ich rodzica i wy��czanie ich a� nie b�d� potrzebne
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
        //Losuje kolor na podstawie tabeli licz�cej wyst�pienia kolor�w
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

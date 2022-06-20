using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodiesCatcher : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Jeœli wypadniesz z planszy gameManager zakoñczy grê jako przegran¹
        if (other.gameObject.name == "You")
        {
            gameManager.loseGame();
            return;
        }
        //Jeœli inny gracz wypadnie z planszy zostanie zniszczony, a cegie³ki,
        //które mia³ przy sobie wróc¹ na swoje pierwotne miejsce
        if (other.gameObject.tag == "Player")
        {
            Stack<Brick> bricks = other.gameObject.GetComponent<BrickHolder>().bricks;
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks.Pop().GoOrigin();
            }
            Destroy(other.gameObject);
            return;
        }
        //Jeœli cegie³ka wypadnie z planszy, przywróæ j¹ na pierwotne miejsce
        Brick brick;
        if (other.gameObject.TryGetComponent<Brick>(out brick))
        {
            brick.GoOrigin();
        }
    }
}

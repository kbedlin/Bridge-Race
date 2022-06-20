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
        //Je�li wypadniesz z planszy gameManager zako�czy gr� jako przegran�
        if (other.gameObject.name == "You")
        {
            gameManager.loseGame();
            return;
        }
        //Je�li inny gracz wypadnie z planszy zostanie zniszczony, a cegie�ki,
        //kt�re mia� przy sobie wr�c� na swoje pierwotne miejsce
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
        //Je�li cegie�ka wypadnie z planszy, przywr�� j� na pierwotne miejsce
        Brick brick;
        if (other.gameObject.TryGetComponent<Brick>(out brick))
        {
            brick.GoOrigin();
        }
    }
}

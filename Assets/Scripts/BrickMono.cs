using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMono : MonoBehaviour
{
    public Blocks blocks;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        blocks.deleteBlock(gameObject);
    }


}

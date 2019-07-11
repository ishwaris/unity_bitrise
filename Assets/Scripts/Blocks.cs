using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks
{
    List<GameObject> _blockList;
    GameObject _blockHolder;
    PhysicsMaterial2D _physicsMaterial2d;

    public Blocks(Vector2 position, int xCount, int yCount, PhysicsMaterial2D physicsMaterial2d)
    {
        _physicsMaterial2d = physicsMaterial2d;
        _blockList = new List<GameObject>();
        createBlocks(position, xCount, yCount);
    }

    void createBlocks(Vector2 position, int xCount, int yCount)
    {
        // Limit amount of blocks
        if (xCount < 1)
            xCount = 1;

        if (xCount > 10)
            xCount = 10;

        if (yCount < 1)
            yCount = 1;

        if (yCount > 4)
            yCount = 4;

        _blockHolder = new GameObject("block holder");

        Vector2 stride = new Vector2(1.1f, 1.1f);

        Vector2 offset = new Vector2(-stride.x * (xCount - 1) * 0.5f, -stride.y * (yCount - 1) * 0.5f) + position;

        for (int x = 0; x < xCount; ++x)
        {
            for (int y = 0; y < yCount; ++y)
            {
                GameObject gameObject = createBlock(new Vector2(x * stride.x, y * stride.y) + offset);
                _blockList.Add(gameObject);
            }
        }
    }

    public void resetBlocks(Vector2 position, int xCount, int yCount)
    {
        deleteAll();
        createBlocks(position, xCount, yCount);
    }

    GameObject createBlock(Vector2 position)
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.DestroyImmediate(block.GetComponent<Rigidbody>());
        GameObject.DestroyImmediate(block.GetComponent<Collider>());

        block.AddComponent<BoxCollider2D>().sharedMaterial = _physicsMaterial2d;
        block.transform.parent = _blockHolder.transform;
        block.transform.position = position;

        BrickMono brickMono = block.AddComponent<BrickMono>();
        brickMono.blocks = this;

        return block;
    }

    public void deleteBlock(GameObject brick)
    {
        _blockList.Remove(brick);
        GameObject.Destroy(brick);
    }

    void deleteAll()
    {
        foreach (GameObject gameObject in _blockList)
        {
            GameObject.Destroy(gameObject);
        }

        _blockList.Clear();
    }

}

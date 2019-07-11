using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Boarders
{

    
    public static void make(PhysicsMaterial2D physicsMaterial2D)
    {
        makeBoarder(new Vector2(-6, 0), new Vector2(0.5f, 12), physicsMaterial2D);
        makeBoarder(new Vector2(6, 0), new Vector2(0.5f, 12), physicsMaterial2D);
        makeBoarder(new Vector2(0, -6), new Vector2(12, 0.5f), physicsMaterial2D);
        makeBoarder(new Vector2(0, 6), new Vector2(12, 0.5f), physicsMaterial2D);
    }

    static void makeBoarder(Vector2 pos, Vector2 size, PhysicsMaterial2D physicsMaterial2D)
    {
        GameObject boader = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.DestroyImmediate(boader.GetComponent<Rigidbody>());
        GameObject.DestroyImmediate(boader.GetComponent<Collider>());

        boader.AddComponent<BoxCollider2D>().sharedMaterial = physicsMaterial2D;
        boader.transform.position = pos;
        boader.transform.localScale = new Vector3(size.x, size.y, 1);
    }

}

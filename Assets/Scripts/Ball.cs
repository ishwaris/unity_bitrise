using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    GameObject _gameObject;
    Rigidbody2D _rigidBody;

    public Ball(Vector2 position, PhysicsMaterial2D physicsMaterial2D, BallGameColor ballGameColor)
    {
        _gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.DestroyImmediate(_gameObject.GetComponent<Rigidbody>());
        GameObject.DestroyImmediate(_gameObject.GetComponent<Collider>());

        _gameObject.AddComponent<CircleCollider2D>();
        _rigidBody = _gameObject.AddComponent<Rigidbody2D>();
        _rigidBody.sharedMaterial = physicsMaterial2D;


        _gameObject.transform.position = position;

        _rigidBody.velocity = new Vector2(0, 5);

        setBallColor(ballGameColor);
    }

    public void setBallColor(BallGameColor ballGameColor)
    {
        if (ballGameColor == BallGameColor.Blue)
            _gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        if (ballGameColor == BallGameColor.Green)
            _gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        if (ballGameColor == BallGameColor.White)
            _gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        if (ballGameColor == BallGameColor.Red)
            _gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void reset()
    {
        _gameObject.transform.position = Vector3.zero;
        _rigidBody.velocity = new Vector2(0, 5);
    }
}

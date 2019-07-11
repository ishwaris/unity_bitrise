using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle
{

    GameObject _gameObject;
    Rigidbody2D _rigidbody;

    void createPaddle(PhysicsMaterial2D physicMaterial)
    {

        _gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.DestroyImmediate(_gameObject.GetComponent<Rigidbody>());
        GameObject.DestroyImmediate(_gameObject.GetComponent<Collider>());

        _gameObject.AddComponent<BoxCollider2D>().sharedMaterial = physicMaterial;
        _rigidbody = _gameObject.AddComponent<Rigidbody2D>();



        _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        _gameObject.transform.localScale = new Vector3(2, 0.5f, 1);
    }

    public Paddle(Vector2 position, PhysicsMaterial2D physicMaterial)
    {

        createPaddle(physicMaterial);

        _gameObject.transform.position = position;
    }

    public void setPosition(Vector2 position)
    {
        _gameObject.transform.position = position;
    }

    public void update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            direction += new Vector2(-3, 0);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            direction += new Vector2(3, 0);


        _rigidbody.velocity = direction * 3;

    }
}

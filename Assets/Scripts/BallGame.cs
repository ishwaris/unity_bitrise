using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum BallGameColor
{
    White,
    Green,
    Red,
    Blue
}

public class ExposedData
{
    public SimCapi.SimCapiNumber xCount;
    public SimCapi.SimCapiNumber yCount;
    public SimCapi.SimCapiEnum<BallGameColor> color;

    public ExposedData()
    {
        // Default values are defined in the constructor
        xCount = new SimCapi.SimCapiNumber(8);
        yCount = new SimCapi.SimCapiNumber(3);
        color = new SimCapi.SimCapiEnum<BallGameColor>(BallGameColor.White);
    }

    public void exposeAll()
    {
        xCount.expose("Brick colum count", false, false);
        yCount.expose("Brick row count", false, false);
        color.expose("Color", false, false);
    }
}

public class BallGame : MonoBehaviour
{
    SimCapi.Transporter _transporter;
    ExposedData _exposedData;

    Blocks _blocks;
    Ball _ball;
    Paddle _paddle;

    bool _started = false;

    void init()
    {
        // Disable gravity for ball
        Physics2D.gravity = Vector2.zero;

        // 2d physics material so the ball bounces on collisions
        PhysicsMaterial2D bouncePhysicsMaterial = new PhysicsMaterial2D();
        bouncePhysicsMaterial.bounciness = 1;
        bouncePhysicsMaterial.friction = 0;

        // Create all the objects for the game, use the _exposedData to initalise the values
        _blocks = new Blocks(new Vector2(0, 3), (int)_exposedData.xCount.getValue(), (int)_exposedData.yCount.getValue(), bouncePhysicsMaterial);
        _ball = new Ball(Vector2.zero, bouncePhysicsMaterial, _exposedData.color.getValue());
        _paddle = new Paddle(new Vector2(0,-3.0f), bouncePhysicsMaterial);
        Boarders.make(bouncePhysicsMaterial);

        _started = true;
    }


    void restartGame()
    {
        if (_started == false)
            return;

        _ball.reset();
        _blocks.resetBlocks(new Vector2(0, 3), (int)_exposedData.xCount.getValue(), (int)_exposedData.yCount.getValue());
        _paddle.setPosition(new Vector2(0, -3.0f));
    }

    void Start()
    {
        // Get the singleton Transporter
        _transporter = SimCapi.Transporter.getInstance();

        // Create the data that will be exposed to SimCapi
        _exposedData = new ExposedData();

        // expose the data
        _exposedData.exposeAll();

        // Add call backs for data change
        _transporter.addInitialSetupCompleteListener(
            delegate()
            {
                init();
            });

        _exposedData.xCount.setChangeDelegate(
            delegate(float value, SimCapi.ChangedBy changedBy)
            {
                restartGame();
            });

        _exposedData.yCount.setChangeDelegate(
            delegate(float value, SimCapi.ChangedBy changedBy)
            {
                restartGame();
            });

        _exposedData.color.setChangeDelegate(
            delegate (BallGameColor ballGameColor, SimCapi.ChangedBy changedBy)
            {
                if (_started == false)
                    return;

                _ball.setBallColor(ballGameColor);
            });

        // Tell SimCapi we are ready, creates the connection
        _transporter.notifyOnReady();
    }


	void Update ()
    {
        if (_started == false)
            return;

        _paddle.update();
	}
}

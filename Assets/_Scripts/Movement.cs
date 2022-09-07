using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject[] mask;
    Vector3 startPosition;
    float maxFoward;
    int layer=0;
    public static event Action LevelCompleted;
    private void Start()
    {
        startPosition = transform.position;
        maxFoward = transform.position.z;
        SwipeDetector.TouchMovement += SwipeDetector_TouchMovement;
    }

    private void SwipeDetector_TouchMovement(bool obj)
    {
            MoveFoward(obj);
    }

    void MoveFoward(bool foward)
    {
        if (foward)
        {
            transform.Translate(0, 0, -0.01f);

            if (maxFoward > transform.position.z)
            {
                maxFoward = transform.position.z;
                mask[layer].transform.Translate(0, 0, -0.01f);
                if (mask[layer].transform.localPosition.z <= 0)
                {
                    LevelUp();
                }
            }
            
        }
        else
            transform?.Translate( 0,0,0.01f);
    }
    void LevelUp()
    {
        if (layer < mask.Length-1)
        {
            layer++;
            maxFoward = startPosition.z;
            transform.position = startPosition;
                transform.Translate(0, -0.05f, 0);
        }
        else
            GameEnd();
    }
    void GameEnd()
    {
        SwipeDetector.TouchMovement -= SwipeDetector_TouchMovement;
        LevelCompleted?.Invoke();
    }
}

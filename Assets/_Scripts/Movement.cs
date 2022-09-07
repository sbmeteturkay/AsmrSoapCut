using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject[] mask;
    public ParticleSystem cutParticle;
    Vector3 startPosition;
    float maxFoward;
    int layer=0;
    int pariclePos;
    public static event Action LevelCompleted;
    private void Start()
    {
        startPosition = transform.position;
        maxFoward = transform.position.z;
        pariclePos = (int)((10 * (maxFoward % 5f)) % 10);
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

                //this is just random math, i dont know either, but its basically works as if blade on soap position, i will look up later for better solution
                if((int) ((10 * (maxFoward % 500f)) % 10)>=1&&( int)((10 * (maxFoward % 500f)) % 10) <= 4){
                    print((int)((10 * (maxFoward % 500f)) % 10));
                    if (pariclePos > (int)((10 * (maxFoward % 500f)) % 10))
                    {
                        cutParticle.Play();
                        pariclePos = (int)((10 * (maxFoward % 500f)) % 10);
                    }
                }

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
            cutParticle.Stop();
            maxFoward = startPosition.z;
            pariclePos = (int)((10 * (maxFoward % 5f)) % 10);
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

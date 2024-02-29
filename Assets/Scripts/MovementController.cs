using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] GameObject world;

    private float worldRadius;
    
    //This is angle per second
    [SerializeField] private float speed;

    private float angle = 0;

    //In addition to the radius of the world, usually the radius
    private float height;

    private float radius;

    

    // Start is called before the first frame update
    void Start()
    {
        worldRadius = world.GetComponent<WorldBehavior>().radius;

        height = gameObject.transform.localScale.x / 2;
        radius = height;
    }

    // Update is called once per frame
    void Update()
    {
        worldRadius = world.GetComponent<WorldBehavior>().radius;
        angle = (angle + speed);

        if(angle > 2 * math.PI)
        {
            angle = 0;
            world.GetComponent<WorldBehavior>().despawn();
            world.GetComponent<WorldBehavior>().spawn();

            speed += .0005f;
        }
        

        move();

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(Jump());
        }
    }

    void move()
    {
        float rad = worldRadius + height;
        
        gameObject.transform.position = new Vector3(rad * math.sin(angle), rad * math.cos(angle),0);
    }

    IEnumerator Jump()
    {
        
        for(int i = 0; i < 50; i++)
        {
            height = Mathf.Lerp(height, 3, 0.01f);

            if(math.abs(height - 3) < .1f)
                yield return StartCoroutine(Fall());;

            yield return null;
        }

       yield return StartCoroutine(Fall());;

    }

    IEnumerator Fall()
    {
        
        for(int i = 0; i < 500; i++)
        {
            height = Mathf.Lerp(height, radius, 0.01f);

            if(math.abs(height - radius) < .005f)
                yield break;

            yield return null;
        }

       yield break;

    }


    

}

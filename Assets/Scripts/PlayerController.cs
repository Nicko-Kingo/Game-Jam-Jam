using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject world;

    private float worldRadius;

    private float height;

    private float radius;

    private float angle = 0;

    private float speed;

    [SerializeField] private Text Score;
    private int score;


    // Start is called before the first frame update
    void Start()
    {
        worldRadius = world.GetComponent<WorldController>().radius;
        height = gameObject.transform.localScale.x / 2;
        radius = height;
        speed = .005f;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Agony: " + score;
        worldRadius = world.GetComponent<WorldController>().radius;
        angle = (angle + speed);

        if(angle > 2 * math.PI)
        {   
            score+=1;
            angle = 0;
            world.GetComponent<WorldController>().despawn();
            world.GetComponent<WorldController>().spawn();



            speed *= 1.03f;
        }

        move();

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(jump());
        }
        
    }

    private void move()
    {
        float rad = worldRadius + height;
        gameObject.transform.position = new Vector3(rad * math.sin(angle), rad * math.cos(angle), 0);
    }


    IEnumerator jump()
    {
         for(int i = 0; i < 50; i++)
        {
            height = Mathf.Lerp(height, 3, 0.01f);

            if(math.abs(height - 3) < .1f)
                yield return StartCoroutine(fall());

            yield return null;
        }

       yield return StartCoroutine(fall());
    }

    IEnumerator fall()
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

    void OnTriggerEnter2D(Collider2D ob)
    {
        die();
    }

    void die()
    {
        SceneManager.LoadScene("DieScreen");
    }
}

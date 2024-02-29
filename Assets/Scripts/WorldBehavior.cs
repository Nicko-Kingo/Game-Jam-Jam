using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBehavior : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject Obby;
    public float radius{get; private set;}
    private System.Random randy;

    public Stack<GameObject> obstacles;



    // Start is called before the first frame update
    void Awake()
    {
        randy = new System.Random();
        obstacles = new Stack<GameObject>();
        radius = gameObject.transform.localScale.x / 2;
        
    }

    // Update is called once per frame
    void Update()
    {

        float scale = Mathf.Lerp(5, 50, Time.time / 100);
        gameObject.transform.localScale = new Vector3(scale, scale, 0);

        radius = gameObject.transform.localScale.x / 2;

        
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 500, Time.time/50);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, 0.0001f);


    }

    public void despawn()
    {
        while(obstacles.Count != 0)
        {
            GameObject ob = obstacles.Pop();

            Destroy(ob);
        }
    }

    public void spawn()
    {
        int spawns = (int)(2 * math.PI * radius / 6);
        for(int i = 0; i < spawns; i++)
        {
            float loc = 1 + (float)(randy.NextDouble() * 2 * Math.PI - 1);

            GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ob.transform.position = new Vector3(radius * math.sin(loc), radius * math.cos(loc),0);
            ob.transform.rotation = quaternion.Euler(0,0,(float)(loc * 180 / Math.PI)m);

            obstacles.Push(ob);
        }


        return;
    }






}

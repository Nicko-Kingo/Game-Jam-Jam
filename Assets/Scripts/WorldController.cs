using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject obby;
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
        float scale = Mathf.Lerp(5, 50, Time.time/100);
        gameObject.transform.localScale = new Vector3(scale,scale,0);

        
        
        

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, Time.time/900);
        radius = gameObject.transform.localScale.x / 2; 

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
        int spawns = (int)(2 * math.PI * radius/ 8);

        for(int i = 0; i < spawns; i++)
        {
            float loc = 1 + (float)(randy.NextDouble() * 2 * math.PI - 1);

            Vector3 Location = new Vector3(radius * math.sin(loc), radius * math.cos(loc), 0);

            GameObject ob = Instantiate(obby, Location, quaternion.identity);

            obstacles.Push(ob);

            ob.transform.rotation = quaternion.Euler(0,0,(float)(loc * 180/Math.PI));


        }
    }

    private void move(GameObject ob)
    {
        float angle = math.atan2(ob.transform.position.y, ob.transform.position.x);
        
        ob.transform.position = new Vector3(radius * math.sin(angle), radius * math.cos(angle), 0);

    }
}

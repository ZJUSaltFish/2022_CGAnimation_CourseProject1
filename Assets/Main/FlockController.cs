using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    public ParaCollection paras;
    public GameObject boid;
    public GameObject selectPoint;
    public GameObject line;
    // Start is called before the first frame update
    void Start()
    {
        
        Spawn();

        //Initialize Line
        paras.destination = transform.position;
    }

    void Update()
    {
        //Update Marker position
        Vector3 averageP = new Vector3(0,0,0);
        for(int i = 0; i < paras.boidNumber; i++)
        {
            averageP += paras.boidList[i].transform.position;
        }
        averageP /= paras.boidNumber;
        transform.position = averageP;

        //Ray Cast
        if(Input.GetMouseButtonDown(1))
        {
            ReTarget();
        }

        //Update Line
        line.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        line.GetComponent<LineRenderer>().SetPosition(1, paras.destination - transform.position);

        //Boid Action
        BoidMove();
    }

    
    void Spawn()
    {
        paras.boidList = new GameObject[paras.boidNumber];
        Vector3 offset;
        for(int i=0; i<paras.boidNumber; i++)
        {
            offset = Random.insideUnitSphere * Random.Range(1,paras.boidSpawnRange);
            GameObject newBoid = GameObject.Instantiate(boid, transform.position + offset, transform.rotation);
            newBoid.GetComponent<Boid>().index = i;
            paras.boidList[i] = newBoid;
        }
    }

    void ReTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 200, 1 << 3))
        {
            Vector3 hitPoint = ray.origin + ray.direction * hit.distance;
            GameObject.Instantiate(selectPoint, hitPoint, transform.rotation);
            paras.destination = hitPoint;
        }
    }
/*
    void ReCalculate()
    {
        Vector3 d;
        for(int i = 0; i < paras.boidNumber; i++)
        {
            for(int j = i+1; j < paras.boidNumber; j++)
            {
                d = paras.boidList[j].transform.position - paras.boidList[i].transform.position;
                
            }
        }
    }
*/
    void BoidMove()
    {
        for(int i = 0; i < paras.boidNumber; i++)
        {
            paras.boidList[i].GetComponent<Boid>().UpdateMovement();
            //Debug.Log("HI");
        }
    }
}

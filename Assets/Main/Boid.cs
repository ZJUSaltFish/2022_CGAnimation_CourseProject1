using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public ParaCollection paras;
    #region parameters
    public int index;
    public Vector3 v;
    Vector3 a_Accumulator;
    Vector3 a;
    Vector3 dist;//remaining distance to arrive destiantion
    Vector3 distDir;// = dist.normalize
    float s;// =dist.length
    List<Vector3> neighborDist;
    Vector3 neighborCenter;
    Vector3 neighborV;
    #endregion

    #region methods
    void Awake()
    {
        neighborDist = new List<Vector3>();
    }

    public void UpdateMovement()
    {
        Initialize();//initialize;
        LookAround();// find all available neighbors

        ObstacleAvoid();//try to avoid emergent incoming obstacles
        CollisionAvoid();//try to avoid being hit by other boids
        VelocityMatch();//try to match the velocity with available flock
        FlockCentering();//try to get close to the flock

        Approach();//try to get close to the destination

        ApplyMovement();//update the boid movement
    }

    void Initialize()
    {
        a_Accumulator = Vector3.zero;
        a = Vector3.zero;

        dist = paras.destination - transform.position;
        distDir = dist.normalized;
        s = dist.magnitude;

        neighborCenter = Vector3.zero;
        neighborV = Vector3.zero;
        neighborDist.Clear();
    }
    void LookAround()
    {     
        Vector3 boidDist;        
        for(int i = 0; i < paras.boidNumber; i++)
        {
            if(i != index){
                boidDist = paras.boidList[i].transform.position - transform.position;
                if(boidDist.magnitude < paras.DoV){//if close enough
                    if(Vector3.Dot(boidDist.normalized, transform.forward) > Mathf.Cos(Mathf.Deg2Rad * paras.FoV) ){//if in field
                        neighborDist.Add(boidDist);
                        neighborV += paras.boidList[i].GetComponent<Boid>().v;
                        neighborCenter += paras.boidList[i].transform.position;               
                    }
                }
            }
        }
        neighborV /= neighborDist.Count;
        neighborCenter /= neighborDist.Count;
    }

    void ObstacleAvoid()
    {
        RaycastHit hit;//use ray cast to detect obstacles.
        Ray ray = new Ray(transform.position, transform.forward);
        Ray rayRight = new Ray(transform.position, Vector3.Cross(transform.forward, Vector3.up).normalized);
        Ray rayLeft = new Ray(transform.position, Vector3.Cross(Vector3.up, transform.forward).normalized);
        if(Physics.Raycast(ray, out hit, paras.avoidDistance, 1 << 6)){ //if there are obstacles in avoidDistance
            Vector3 avoidingDir = (hit.point - hit.collider.gameObject.transform.position);//get away radially
            avoidingDir.y = 0;//to simplify, no flying up/down
            if((transform.position - hit.point).magnitude < 0.5){//if hit, bounce back
                v = Vector3.Reflect(v,avoidingDir.normalized);
            }               
            a_Accumulator += avoidingDir.normalized * paras.aMentalMax * paras.actionDict["ObstacleAvoid"];  
        }else if(Physics.Raycast(rayLeft, out hit, 2, 1<<6)|| Physics.Raycast(rayRight, out hit, 2, 1<<6)){  //a boid should keep away from the obstacle on right/left side,
            Vector3 avoidingDir = (hit.point - hit.collider.gameObject.transform.position);
            avoidingDir.y = 0;
            a_Accumulator += avoidingDir.normalized * paras.aMentalMax * paras.actionDict["ObstacleAvoid"];  
        }
        
    }

    void CollisionAvoid()
    {
        if(a_Accumulator.magnitude > paras.aPhysicalMax){//if the boid is too tired to accelerate
            a_Accumulator = a_Accumulator * paras.aPhysicalMax / a_Accumulator.magnitude;
            return;
        }
        Vector3 avoidingDir = new Vector3(0,0,0);
        for(int i = 0; i < neighborDist.Count; i++)
        {
            if(neighborDist[i].magnitude < paras.spaceMin)//for each neighbor boid that should avoid:
            {
                avoidingDir += (-neighborDist[i]) / Mathf.Pow((neighborDist[i].magnitude),2);//The closer, the bigger acceleration.
            }
        }
        avoidingDir = avoidingDir.normalized;
        a_Accumulator += avoidingDir * paras.aMentalMax * paras.actionDict["CollisionAvoid"];
    }

    void VelocityMatch()
    {
        if(a_Accumulator.magnitude > paras.aPhysicalMax){//if the boid is too tired to accelerate
            a_Accumulator = a_Accumulator * paras.aPhysicalMax / a_Accumulator.magnitude;
            return;
        }
        Vector3 vDiff = neighborV - v;
        float vDiffMag = vDiff.magnitude;
        if(vDiffMag > paras.vToleranceMax){//if the difference is large enough that needs to be matched
            a_Accumulator += vDiff.normalized * paras.aMentalMax * Mathf.Clamp(vDiffMag / paras.vUpLimit, 0, 1) * paras.actionDict["VelocityMatch"];
        }
    }

    void FlockCentering()
    {
        if(a_Accumulator.magnitude > paras.aPhysicalMax){
            a_Accumulator = a_Accumulator * paras.aPhysicalMax / a_Accumulator.magnitude;
            return;
        }
        Vector3 offset = neighborCenter - transform.position;
        float offMag = offset.magnitude;
        if(offMag > paras.dToleranceMax){
            a_Accumulator += offset.normalized * paras.aMentalMax * Mathf.Clamp(offMag / paras.dUpLimit, 0, 1) * paras.actionDict["FlockCentering"];
        }
    }

    void Approach()
    {
        if(a_Accumulator.magnitude > paras.aPhysicalMax){
            a_Accumulator = a_Accumulator * paras.aPhysicalMax / a_Accumulator.magnitude;
            return;
        }
        if(s < paras.slowDownDist){
            //ideal velocity model: linear decrease when approaching destination, max to 0.
            //Thus acceleration is going to achieve this v.
            Vector3 idealV = (dist-v*Time.deltaTime).normalized * paras.vMax * s/paras.slowDownDist;//the boid will try to predict next frame
            Vector3 vDiff = idealV - v;
            a_Accumulator += vDiff.normalized * paras.aMentalMax * paras.actionDict["Approach"] * Mathf.Clamp(vDiff.magnitude/paras.aMentalMax, 0,1);//the boid want to back to idealV at next second.
        }else{
            //ideal velocity model: max v
            //thus accleration is always aMax, till vMax.
            Vector3 idealV = (dist-v*Time.deltaTime).normalized * paras.vMax * s/paras.slowDownDist;//the boid will try to predict next frame
            Vector3 vDiff = idealV - v;
            a_Accumulator += vDiff.normalized * paras.aMentalMax * paras.actionDict["Approach"];
        }
    }

    void ApplyMovement()
    {
        if(a_Accumulator.magnitude > paras.aPhysicalMax){
            a_Accumulator = a_Accumulator * paras.aPhysicalMax / a_Accumulator.magnitude;
        }
        a = a_Accumulator;
        v += a*Time.deltaTime;
        if(v.magnitude > paras.vMax){
            v = v * paras.vMax / v.magnitude;
        }
        //align to v
        transform.LookAt(transform.position + v);

        transform.position += v*Time.deltaTime;

    }
    #endregion
}

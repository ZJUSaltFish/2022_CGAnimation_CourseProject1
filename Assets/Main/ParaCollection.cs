using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Collection", menuName = "ParaCollection")]
public class ParaCollection : ScriptableObject
{
    #region Predefined Settings
    public float FoV;//field of view, in degrees
    public float DoV;//depth of view, in meters.
    public float vMax;
    public float aPhysicalMax;//the maximum a that a boid can actually achieve.
    public float aMentalMax;//the maximum a that a boid think it can achieve. >= aMax(but usually not realistic)

    public float slowDownDist;//if distance to destination is smaller, begin slow down.

    public float spaceMin;//min tolerable space between two boids. If smaller, boids will try to avoid collision
   
    public float vToleranceMax;//max tolerable v diff of a boid. If larger, it will try to match flock v
    public float vUpLimit;//if v diff is smaller than it, get an linear increasing a.Otherwise get aMax.
   
    public float dToleranceMax;//max tolerable decentration of a boid. If larger, try to get close to the flock center
    public float dUpLimit;//if decentreation is smaller, linear a. Otherwise aMax.

    public float avoidDistance;//the distance to start avoiding obstacles

    public Dictionary<string, float> actionDict;//a dict containing all actions and their weights.

    #endregion

    #region Static Parameters
    public int boidNumber;  
    [Range(1,10)]
    public float boidSpawnRange;
    public GameObject[] boidList;// An lookup table of boids
    #endregion

    #region Dynamic Variables
    public Vector3 destination;//The destination that the flock is going to 
    //public Vector3[,] relationList;//A buffer for relative distance between any two boids
    #endregion
}

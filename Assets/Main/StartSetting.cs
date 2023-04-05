using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StartSetting : MonoBehaviour
{
    public GameObject camera1;
    public GameObject flock;
    public ParaCollection paras;
    public GameObject rangeSetting;
    public GameObject numberSetting;
    void Awake()
    {
        InitializeActions();//Initialize paras.actionDIct
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeActions()
    {
        paras.actionDict = new Dictionary<string, float>();
        paras.actionDict.Add("Approach", 0.30f);
        paras.actionDict.Add("ObstacleAvoid", 1.0f);
        paras.actionDict.Add("CollisionAvoid", 0.50f);
        paras.actionDict.Add("VelocityMatch", 0.4f);
        paras.actionDict.Add("FlockCentering", 0.3f);
    }

    public void StartFlock()
    {
        GameObject.Destroy(camera1);
        GameObject newFlock = GameObject.Instantiate(flock, Vector3.zero, transform.rotation);
        newFlock.transform.GetChild(3).GetComponent<Camera>().enabled = true;

        gameObject.SetActive(false);
    }

    public void SetRange(float value)
    {
        paras.boidSpawnRange = value;
        rangeSetting.transform.GetChild(1).GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetNumber(float value)
    {
        paras.boidNumber = (int)value;
        numberSetting.transform.GetChild(1).GetComponent<TMP_Text>().text = paras.boidNumber.ToString();
    }
}

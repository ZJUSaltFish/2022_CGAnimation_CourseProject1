using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeightController : MonoBehaviour
{
    public ParaCollection paras;
    public GameObject ObstacleAvoid;
    public GameObject CollisionAvoid;
    public GameObject VelocityMatch;
    public GameObject FlockCentering;
    public GameObject Approaching;
    void Start()
    {
        ObstacleAvoid.transform.GetChild(0).gameObject.GetComponent<Slider>().value =  paras.actionDict["ObstacleAvoid"];
        CollisionAvoid.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.actionDict["CollisionAvoid"];
        VelocityMatch.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.actionDict["VelocityMatch"];
        FlockCentering.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.actionDict["FlockCentering"];
        Approaching.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.actionDict["Approach"];
    }

    public void SetOAWeight(float value)
    {
        paras.actionDict["ObstacleAvoid"] = value;
        ObstacleAvoid.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetCAWeight(float value)
    {
        paras.actionDict["CollisionAvoid"] = value;
        CollisionAvoid.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetVMWeight(float value)
    {
        paras.actionDict["VelocityMatch"] = value;
        VelocityMatch.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetFCWeight(float value)
    {
        paras.actionDict["FlockCentering"] = value;
        FlockCentering.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetApWeight(float value)
    {
        paras.actionDict["Approach"] = value;
        Approaching.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
}

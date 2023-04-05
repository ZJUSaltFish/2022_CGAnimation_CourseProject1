using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ParaController : MonoBehaviour
{
    public ParaCollection paras;
    public GameObject FOVController;
    public GameObject DOVController;
    public GameObject VMaxController;
    public GameObject AMentalController;
    public GameObject APhysicalController;
    public GameObject SlowDistController;
    public GameObject SpaceMinController;
    public GameObject VTolerableController;
    public GameObject VThresholdController;
    public GameObject DTolerableController;
    public GameObject DThresholdController;
    public GameObject ObstacleAvoidController;

    void Start()
    {
        FOVController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.FoV;
        DOVController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.DoV;
        VMaxController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.vMax;
        AMentalController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.aMentalMax;
        APhysicalController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.aPhysicalMax;
        SlowDistController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.slowDownDist;
        SpaceMinController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.spaceMin;
        VTolerableController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.vToleranceMax;
        VThresholdController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.vUpLimit;
        DTolerableController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.dToleranceMax;
        DThresholdController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.dUpLimit;
        ObstacleAvoidController.transform.GetChild(0).gameObject.GetComponent<Slider>().value = paras.avoidDistance;
    }
    
    public void SetFOV(float value)
    {
        paras.FoV = value;
        FOVController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetDOV(float value)
    {
        paras.DoV = value;
        DOVController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
    public void SetVMax(float value)
    {   
        paras.vMax = value;
        VMaxController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetAMental(float value)
    {   
        paras.aMentalMax = value;
        AMentalController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetAPhysical(float value)
    {   
        paras.aPhysicalMax = value;
        APhysicalController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetSlowDownDist(float value)
    {   
        paras.slowDownDist = value;
        SlowDistController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetSpaceMin(float value)
    {   
        paras.spaceMin = value;
        SpaceMinController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetVTolerable(float value)
    {   
        paras.vToleranceMax = value;
        VTolerableController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetVThreshold(float value)
    {   
        paras.vUpLimit = value;
        VThresholdController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetDTolerable(float value)
    {   
        paras.dToleranceMax = value;
        DTolerableController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetDThreshold(float value)
    {   
        paras.dUpLimit = value;
        DThresholdController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }

    public void SetObstacleDetect(float value)
    {   
        paras.avoidDistance = value;
        ObstacleAvoidController.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = value.ToString("f2");
    }
}

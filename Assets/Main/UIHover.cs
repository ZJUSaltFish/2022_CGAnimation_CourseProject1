using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIHover : MonoBehaviour
{

    CameraMove moveScript;
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())        {
            Camera.main.TryGetComponent<CameraMove>(out moveScript);
            if(moveScript != null){
                moveScript.enableMove = false;
            }
        }else{
            if(moveScript != null){
                moveScript.enableMove = true;
            }
        }
        
    }
}

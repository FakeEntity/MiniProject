using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unselect : MonoBehaviour
{
    void Update()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}

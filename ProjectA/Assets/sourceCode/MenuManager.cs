using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Dropdpwn;

    // Start is called before the first frame update
    void Start()
    {
        Dropdpwn.GetComponent<Dropdown>().value = (int)DataManager.Instance.currentLevel - 1;
    }

}

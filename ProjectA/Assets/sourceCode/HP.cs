using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public GameObject bossObj;
    private Boss bossScrpit;
    private RectTransform rect;

    private void Awake()
    {
        bossScrpit = bossObj.GetComponent<Boss>();
        rect = gameObject.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        rect.localScale = new Vector3(bossScrpit.hpFloat, rect.localScale.y, rect.localScale.z);
    }

}

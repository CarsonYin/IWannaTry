using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCtrl1 : MonoBehaviour
{

    //public GameObject Partner;
    public List<GameObject> Partners;

    public bool isDisposable;
    private bool alreadyActiv;

    void Awake()
    {
        // initialize partner
        //  Partner = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("sdadasd");
        if (other.gameObject.tag == "Player")
        {
            if (alreadyActiv)
            {
                if (!isDisposable)
                {
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<TrapReact1>().React();

                    }

                }
            }
            else
            {
                for (int i = 0; i < Partners.Count; i++)
                {
                    Partners[i].GetComponent<TrapReact1>().React();

                }
                // Partner.GetComponent<TrapReact>().React();
                alreadyActiv = true;
            }
        }
    }

}

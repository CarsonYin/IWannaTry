using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEngine : MonoBehaviour
{
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
        if (other.gameObject.tag == "Player")
        {
            if (!alreadyActiv)
            {
                if (!isDisposable)
                {
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<TrapEngine>().Reset();
                        Partners[i].GetComponent<TrapEngine>().React();
                    }

                }

                else
                {
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<TrapEngine>().React();
                    }
                    // Partner.GetComponent<TrapReact>().React();
                    alreadyActiv = true;
                }
            }
        }
    }
}

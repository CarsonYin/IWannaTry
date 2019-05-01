using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEngine : MonoBehaviour
{
    public List<GameObject> Partners;

    public bool useSpecialReact;
    public TrapEngine.SpecialReact specialReact;

    public bool isDisposable;
    private bool alreadyActiv;

    public bool triggerToUseGravity;

    void Awake()
    {
        // initialize partner
        //  Partner = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (useSpecialReact)
        {
            switch (specialReact)
            {
                case TrapEngine.SpecialReact.Destroy:
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].SetActive(false);
                    }
                    break;
                case TrapEngine.SpecialReact.Special01:
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<SpecialTR01>().S01React();
                    }
                    break;
                case TrapEngine.SpecialReact.Special02:
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<SpecialTR02>().S02React();
                    }
                    break;
                default:
                    break;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (!alreadyActiv)
            {
                if (triggerToUseGravity)
                {
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<TrapEngine>().TriggerToUseGravity();
                    }
                }
                if (!isDisposable)
                {
                    for (int i = 0; i < Partners.Count; i++)
                    {
                        Partners[i].GetComponent<TrapEngine>().Reset(false);
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

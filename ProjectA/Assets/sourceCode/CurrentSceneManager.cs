using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{

    public List<GameObject> SavePoints;

    public int currentSavePoint;
    

    void Awake()
    {
        currentSavePoint = -1;  // Means Did not reach any savepoint at current level

        //    for (int i = 0; i < transform.ChildCount; i++)
        //    {
        //        transform.Child(i).gameObject.GetComponent<SavePointManager>().Number = i;
        //    }

    }

    public void ResetSavePointSprite(int newSavePointNumber)
    {
        Debug.Log(currentSavePoint);
        Debug.Log(newSavePointNumber);

        if (currentSavePoint > -1)
        {
            transform.GetChild(currentSavePoint).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PNGs/SavePoint");
            transform.GetChild(currentSavePoint).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        transform.GetChild(newSavePointNumber).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PNGs/SavePointHighlighted");
        transform.GetChild(newSavePointNumber).gameObject.GetComponent<BoxCollider2D>().enabled = false;

        currentSavePoint = newSavePointNumber;

    }

}

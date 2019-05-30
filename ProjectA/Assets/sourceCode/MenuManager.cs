using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject dropdown;
    private Dropdown dp;

    // Start is called before the first frame update
    void Start()
    {

        dp = dropdown.GetComponent<Dropdown>();
        foreach (DataManager.LevelNames e in System.Enum.GetValues(typeof(DataManager.LevelNames)))
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = e.ToString();
            dp.options.Add(data);
        }
        dp.options.RemoveAt(0);
        dp.options.RemoveAt(dp.options.Count - 1);

        dp.value = (int)DataManager.Instance.currentLevel - 1;

    }

}

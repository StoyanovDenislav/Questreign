using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectMistakes : MonoBehaviour
{
    private string mainString;

    List<int> ints = new List<int>();

    Dictionary<int, int> counts = new Dictionary<int, int>();

    public IOrderedEnumerable<KeyValuePair<int, int>> sortedCounts;


    // Start is called before the first frame update
    void Awake()
    {
        //PlayerPrefs.DeleteAll();

       // PlayerPrefs.SetString("MainString", "11111100022");


        if (PlayerPrefs.GetString("MainString") != null)
        {
            GetMostMistakes();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetStringAndParse()
    {
        for (int i = 0; i < mainString.Length; i++)
        {
            int num = int.Parse(mainString[i].ToString());

            ints.Add(num);
        }
    }

    public void CountIntsAndFindMaxNum()
    {
        int[] nums = ints.ToArray();

        for (int i = 0; i < nums.Length; i++)
        {
            if (counts.ContainsKey(nums[i]))
            {
                counts[nums[i]]++;
            }
            else
            {
                counts[nums[i]] = 1;
            }
        }

        sortedCounts = counts.OrderByDescending(pair => pair.Value);

        foreach (var pair in sortedCounts)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }
    }

    public void GetMostMistakes()
    {
        ints.Clear();
        counts.Clear();
        sortedCounts = null;
        mainString = PlayerPrefs.GetString("MainString");
        Debug.Log(mainString);
        GetStringAndParse();
        CountIntsAndFindMaxNum();
    }
}
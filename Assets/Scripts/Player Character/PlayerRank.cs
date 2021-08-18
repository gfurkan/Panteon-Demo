using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRank : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private Text rankingText;

    private int lengthSizeContoller = 1;
    private bool _sortRanks = true;

    public bool sortRanks
    {
        get
        {
            return _sortRanks;
        }
        set
        {
            _sortRanks = value;
        }
    }

    private void Start()
    {
        characters.Add(transform.gameObject);
    }
    void Update()
    {
        if (_sortRanks)
        {
            BubbleSort();
        }
        rankingText.text = (System.Array.IndexOf(characters.ToArray(), transform.gameObject) + lengthSizeContoller).ToString() + ".";
    }
    void BubbleSort()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            for (int j = 1; j < characters.Count; j++)
            {
                if (characters[j - 1].transform.position.z < characters[j].transform.position.z)
                {
                    GameObject temp = characters[j - 1];
                    characters[j - 1] = characters[j];
                    characters[j] = temp;
                }
            }
        }
    }
    public void DecreaseListLength(GameObject character)
    {
        if (_sortRanks)
        {
            characters.Remove(character);
            lengthSizeContoller++;
        }
    }
}

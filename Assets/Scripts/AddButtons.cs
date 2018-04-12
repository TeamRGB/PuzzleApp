using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    public Transform puzzleScreen;

    [SerializeField]
    public Transform complete;

    [SerializeField]
    public GameObject btn;

    [SerializeField]
    public GameObject solved;

	//Creating the squares and name them 0-15
    public void Awake()
    {
        for (int counter = 0; counter < 16; counter++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + counter;
            button.transform.SetParent(puzzleScreen, false);
        }
        
        for (int counter = 0; counter < 16; counter++)
        {
            GameObject result = Instantiate(solved);
            result.name = "" + counter;
            result.transform.SetParent(complete, false);
        }
        
    }
}

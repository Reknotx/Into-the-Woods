using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTest : MonoBehaviour
{
    public int seed;
    public int fiddleNumberA;
    public int fiddleNumberB;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        ButtonPress();
    }

    void ButtonPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            seed = Random.Range(0, 9999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Random.InitState(seed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            fiddleNumberA = Random.Range(1, 10);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            fiddleNumberB = Random.Range(0, 9999);
        }
    }

    void GenerateSeed()
    {
    }
}

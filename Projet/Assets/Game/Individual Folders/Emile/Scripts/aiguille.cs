using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aiguille : MonoBehaviour
{
    private Map map;
    public GameObject[] crack;

    private void Start()
    {
        map = Map.Instance;
    }

    private void Update()
    {
        if(map.depth >= 250)
        {
            crack[0].SetActive(true);
        }
        else if(map.depth >= 500)
        {
            crack[1].SetActive(true);
        }
        else if(map.depth >= 750)
        {
            crack[2].SetActive(true);
        }
        transform.rotation = Quaternion.Euler(0, 0, -map.depth / 1000 * 180);
    }
}

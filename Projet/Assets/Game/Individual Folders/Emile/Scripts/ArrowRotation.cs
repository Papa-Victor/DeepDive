using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC;

public class ArrowRotation : MonoBehaviour
{
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;

    // Start is called before the first frame update
    void Start()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(ARotation());
    }

    IEnumerator ARotation()
    {
        if(arrow1.activeSelf == true)
        {
            yield return new WaitForSeconds(0.3f);
            arrow1.SetActive(false);
            arrow2.SetActive(true);
            arrow3.SetActive(false);
        }
        else if(arrow2.activeSelf == true)
        {
            yield return new WaitForSeconds(0.3f);
            arrow1.SetActive(false);
            arrow2.SetActive(false);
            arrow3.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            arrow1.SetActive(true);
            arrow2.SetActive(false);
            arrow3.SetActive(false);
        }
    }
}

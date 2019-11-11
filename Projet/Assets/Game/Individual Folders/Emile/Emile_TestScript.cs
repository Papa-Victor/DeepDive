using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emile_TestScript : MonoBehaviour
{
    [SerializeField]
    private SceneInfo UI_sceneInfo;
    // Start is called before the first frame update
    void Start()
    {
        Scenes.Load(UI_sceneInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

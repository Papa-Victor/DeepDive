using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Station
{
    [SerializeField] private GameObject Destination;
    private AudioSource Audio;
    private GameObject Current;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GameObject.FindGameObjectWithTag("Audio Door").GetComponent<AudioSource>();
        Current = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void Change_Level();
    private event Change_Level Level_Loaded;
    public void Subscribe_Level_Loaded(Change_Level del)
    {
        Level_Loaded += del;
    }
    public void Unsubscribe_Level_Loaded(Change_Level del)
    {
        Level_Loaded -= del;
    }

    public override void Interact(PlayerController pc)
    {
        pc.ChangeState(UsingDoorPlayerState.GetInstance(this));
        Audio.Play();
        Destination.SetActive(true);
        Current.SetActive(false);
        Map.Instance.UpdatePlayerLocation(Destination.GetComponentInChildren<Door>().GetLocation());
        FindObjectOfType<EventManager>().GetComponent<EventManager>().UpdateStationSprites();
        Level_Loaded();

        if (sH == null)
            sH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStationHandler>();
        sH.ResetHighlightStation();
        interactible = false;
    }

    public Room GetLocation(){return location;}
}

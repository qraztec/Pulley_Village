using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Teleport2 : MonoBehaviour
{
    public Button button1;
   


    public GameObject player;
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(OnButton1Click);
        
    }

    // Update is called once per frame
    void OnButton1Click()
    {
        player.transform.position = game.transform.position;
        player.transform.rotation = game.transform.rotation;
        //Tele(floorclass);
    }

   
}

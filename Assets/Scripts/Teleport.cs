using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Button button1;
    public Button button2;

   
    public GameObject player;

   // player = transform.Find("Pulley");
  //  targetObject = pulleyTransform.Find("Right")?.GetComponent<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(OnButton1Click);
        button2.onClick.AddListener(OnButton2Click);
    }

    // Update is called once per frame
    void OnButton1Click()
    {
        player.transform.position = new Vector3(69.38f, 0.5f, -7f);
        //Tele(floorclass);
    }

    void OnButton2Click()
    {
        player.transform.position = new Vector3(48.78f, 0.5f, -10f);
    }

   
}

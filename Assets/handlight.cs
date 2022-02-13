using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handlight : MonoBehaviour
{
    public Transform Player;
    Camera MainCam;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Player.position;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, MainCam.transform.rotation, 0.03f);
    }
}

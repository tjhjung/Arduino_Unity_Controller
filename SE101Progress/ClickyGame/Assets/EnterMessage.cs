using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMessage : MonoBehaviour {
    public GameObject startMessage;
	// Use this for initialization
	void Start () {
        startMessage.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
        {
            startMessage.SetActive(false);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachCircleScript : MonoBehaviour {
    int count = 0;
    // Use this for initialization
    void Start () {

    }

	// Update is called once per frame
	void Update () {

        if (count < 60)
        {
            transform.localScale -= new Vector3(0.04F, 0.04F, 0);
        } else
        {
            GameHandler.end_button = true;
        }
        count++;
	}

}

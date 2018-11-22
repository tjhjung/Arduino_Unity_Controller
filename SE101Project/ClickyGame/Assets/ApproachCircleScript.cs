using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachCircleScript : MonoBehaviour {
    public double count;
    private double finish;
    // Use this for initialization
    void Start () {
        count = 0;
        finish = 1.3;
    }

	// Update is called once per frame
	void Update () {

        if (count < finish)
        {
            transform.localScale -= new Vector3(0.04F, 0.04F, 0);
        } else
        {
            GameHandler.end_button = true;
        }
        count += Time.deltaTime;
	}

}

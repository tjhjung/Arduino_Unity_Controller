using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour {

    public static int score = 0;
    Text score_text;

	// Use this for initialization
	void Start () {
        score_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        score_text.text = "Points: " + score;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    protected int songPosition;
    public MathSongScript mathSongScript;
    protected int clipFrequency;
    public GameObject redButton;
    public GameObject greenButton;
    public GameObject yellowButton;
    public GameObject redApproach;
    public GameObject greenApproach;
    public GameObject yellowApproach;
    protected GameObject temp;
    protected GameObject tempApproach;
    protected Queue<GameObject> buttonsList = new Queue<GameObject>();
    protected Queue<GameObject> approachCircleList = new Queue<GameObject>();

    // Use this for initialization
    void Start () {
        clipFrequency = (mathSongScript.getAudioClipFrequency());
        songPosition = 0;
        redButton.SetActive(false);
        yellowButton.SetActive(false);
        greenButton.SetActive(false);
        redApproach.SetActive(false);
        yellowApproach.SetActive(false);
        greenApproach.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        songPosition = mathSongScript.getSongPosition();
        print(1.0 * songPosition / clipFrequency);
        if (Input.GetKeyDown(KeyCode.R))
        {
            temp = Instantiate(redButton);
            temp.SetActive(true);
            buttonsList.Enqueue(temp);
            tempApproach = Instantiate(redApproach);
            tempApproach.SetActive(true);
            approachCircleList.Enqueue(tempApproach);
        } else if (Input.GetKeyDown(KeyCode.G)) {
            temp = Instantiate(greenButton);
            temp.SetActive(true);
            buttonsList.Enqueue(temp);
            tempApproach = Instantiate(greenApproach);
            tempApproach.SetActive(true);
            approachCircleList.Enqueue(tempApproach);
        } else if (Input.GetKeyDown(KeyCode.Y))
        {
            temp = Instantiate(yellowButton);
            temp.SetActive(true);
            buttonsList.Enqueue(temp);
            tempApproach = Instantiate(yellowApproach);
            tempApproach.SetActive(true);
            approachCircleList.Enqueue(tempApproach);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(buttonsList.Dequeue());
            Destroy(approachCircleList.Dequeue());
        }
	}
}

using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GameHandler : MonoBehaviour {
    const string MAP_NAME = "KeepMeCrazyMapAverage.txt";
    const double MAXLENGTH = 191;
    public static bool end_button = false;
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
    protected ArrayList buttonColour = new ArrayList();
    protected ArrayList buttonTiming = new ArrayList();
    protected int arraySize;
    protected int indexPosition;


    void ReadString()
    {
        string path = MAP_NAME;
        string input;
        int count = 0;
        StreamReader readFile = new StreamReader(path);
        input = readFile.ReadLine();
        arraySize = int.Parse(input);
        while ((input = readFile.ReadLine()) != null) {
            if (count % 2 == 0) {
                buttonColour.Add(input);
            } else {
                buttonTiming.Add(double.Parse(input));
            }
            count++;
        }
        /*
        for (int i = 0; i < arraySize; i++)
        {
            print((string) buttonColour[i]);
            print((double) buttonTiming[i]);
        }
        print("read the stuff");
        */
    }

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
        indexPosition = 0;
        ReadString();
    }

    // Update is called once per frame
    void Update() {
        songPosition = mathSongScript.getSongPosition();
        print(Math.Round(1.0 * songPosition / clipFrequency, 3) + "Song position");
        print(Math.Abs((double)buttonTiming[indexPosition] - Math.Round(1.0 * songPosition / clipFrequency, 3)));
        print("timing: " + (double)buttonTiming[indexPosition]);
        //if (1.0 * songPosition / clipFrequency > MAXLENGTH) {
            if (Math.Abs((double)buttonTiming[indexPosition] - 0.5 - Math.Round(1.0 * songPosition / clipFrequency, 3)) <= 0.03 &&
                indexPosition < arraySize)
            {  /// encompass everything
                print("enter");
                if (Equals((string)buttonColour[indexPosition], "Q"))
                {
                    temp = Instantiate(redButton);
                    temp.SetActive(true);
                    buttonsList.Enqueue(temp);
                    tempApproach = Instantiate(redApproach);
                    tempApproach.SetActive(true);
                    approachCircleList.Enqueue(tempApproach);
                }
                else if (Equals((string)buttonColour[indexPosition], "W"))
                {
                    temp = Instantiate(greenButton);
                    temp.SetActive(true);
                    buttonsList.Enqueue(temp);
                    tempApproach = Instantiate(greenApproach);
                    tempApproach.SetActive(true);
                    approachCircleList.Enqueue(tempApproach);
                }
                else if (Equals((string)buttonColour[indexPosition], "E"))
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
                    button_destroy();
                }
                indexPosition++;
                print("removed");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                print(Math.Round(1.0 * songPosition / clipFrequency, 3) + " Q");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                print(Math.Round(1.0 * songPosition / clipFrequency, 3) + " W");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                print(Math.Round(1.0 * songPosition / clipFrequency, 3) + " E");
            }

            if (end_button == true) {
                button_destroy();
                end_button = false;
            }
        //}
	}


    void button_destroy()
    {
        Destroy(buttonsList.Dequeue());
        Destroy(approachCircleList.Dequeue());
    }
}

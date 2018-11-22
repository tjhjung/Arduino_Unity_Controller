using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GameHandler : MonoBehaviour {
    const string MAP_NAME = "KeepMeCrazyMapAverage.txt";
    const double MAXLENGTH = 191;
    const int RED_B = 1;
    const int GREEN_B = 2;
    const int YELLOW_B = 3;

    protected double time_now = 0;
    protected double curr_time = 0;
    protected int latest_button = 0;
    protected int curr_button = 0;

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



    SerialPort sp = new SerialPort("COM3", 9600);


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
        sp.Open();
        sp.ReadTimeout = 1;

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
        time_now = 1.0 * songPosition / clipFrequency;
        //print(Math.Round(time_now, 3) + "Song position");
        //print(Math.Abs((double)buttonTiming[indexPosition] - Math.Round(time_now, 3)));
        //print("timing: " + (double)buttonTiming[indexPosition]);

        //if (time > MAXLENGTH) {
        if (Math.Abs((double)buttonTiming[indexPosition] - 0.5 - Math.Round(time_now, 3)) <= 0.04 &&
            indexPosition < arraySize)
        {  /// encompass everything
            //print("enter");
            if (Equals((string)buttonColour[indexPosition], "Q"))
            {
                curr_time = time_now;
                temp = Instantiate(redButton);
                temp.SetActive(true);
                buttonsList.Enqueue(temp);
                tempApproach = Instantiate(redApproach);
                tempApproach.SetActive(true);
                latest_button++;
                approachCircleList.Enqueue(tempApproach);
            }
            else if (Equals((string)buttonColour[indexPosition], "W"))
            {
                curr_time = time_now;   
                temp = Instantiate(greenButton);
                temp.SetActive(true);
                buttonsList.Enqueue(temp);
                tempApproach = Instantiate(greenApproach);
                tempApproach.SetActive(true);
                latest_button++;
                approachCircleList.Enqueue(tempApproach);
            }
            else if (Equals((string)buttonColour[indexPosition], "E"))
            {
                curr_time = time_now;
                temp = Instantiate(yellowButton);
                temp.SetActive(true);
                buttonsList.Enqueue(temp);
                tempApproach = Instantiate(yellowApproach);
                tempApproach.SetActive(true);
                latest_button++;
                approachCircleList.Enqueue(tempApproach);
            }
            /*
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                button_destroy();
            }
            */
            indexPosition++;
            //print("removed");
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //print(Math.Round(time_now, 3) + " Q");
        }
        
        else if (Input.GetKeyDown(KeyCode.W))
        {
            print(Math.Round(time_now, 3) + " W");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            print(Math.Round(time_now, 3) + " E");
        }
        */

        if (curr_button < latest_button)
        {
            ButtonPress((string)buttonColour[curr_button]);
        }

        
        if (CheckButtonPress())
        {
            curr_button++;
            button_destroy();
            //sp.DiscardInBuffer();
        }

        if (end_button == true)
        {
            button_destroy();
            end_button = false;
            curr_button++;
            ScoreScript.score -= 100;
        }
        //}
    }


    void button_destroy()
    {
        try
        {
            Destroy(buttonsList.Dequeue());
            Destroy(approachCircleList.Dequeue());
        }
        catch (System.Exception)
        {

        }
    }


    void ButtonPress(int button)
    {
        if (button == RED_B)
        {
            sp.WriteLine("C1");
        }
        if (button == GREEN_B)
        {
            sp.WriteLine("C2");
        }
        if (button == YELLOW_B)
        {
            sp.WriteLine("C3");
        }
    }

    void ButtonPress(string button)
    {
        if (string.Equals(button, "Q"))
        {
            ButtonPress(RED_B);
        } else if (string.Equals(button, "W"))
        {
            ButtonPress(GREEN_B);
        } else if (string.Equals(button, "E"))
        {
            ButtonPress(YELLOW_B);
        }
    }

    bool CheckButtonPress()
    {
        if (curr_button < latest_button)
        {
            try
            {
                string response = sp.ReadLine();
                if ((response.Equals("BP1") && string.Equals((string)buttonColour[curr_button], "Q")) ||
                     (response.Equals("BP2") && string.Equals((string)buttonColour[curr_button], "W")) ||
                     (response.Equals("BP3") && string.Equals((string)buttonColour[curr_button], "E")))
                {
                    GameObject next_up = approachCircleList.Peek();
                    ApproachCircleScript counter_script = next_up.GetComponent<ApproachCircleScript>();
                    TimePoints(counter_script.count);
                    // Check Recieved
                    sp.WriteLine("C4");
                    //sp.DiscardInBuffer();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    void TimePoints(double count)
    {
        //print(count);
        //print("Goal Time: " + goal_time);
        //print("Your Time: " + curr_time);
        //double diff = Math.Abs(curr_time - goal_time);
        //print("Diff: " + diff);
        if (count < 1.05 && count > 0.95)
        {
            ScoreScript.score += 300;
        }
        else if (count < 1.2 && count > 0.8)
        {
            ScoreScript.score += 100;
        }
        else if (count < 1.3 && count > 0.7)
        {
            ScoreScript.score += 50;
        }
        else
        {
            ScoreScript.score += 0;
        }
    }
}

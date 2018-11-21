using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Button_Press_Time : MonoBehaviour {
    const int RED_B = 0;
    const int GREEN_B = 1;
    const int YELLOW_B = 2;
    public bool button_wait = false;
    public int button_counter = 0;
    public int b_time = 0;
    SerialPort sp = new SerialPort("COM3", 9600);

	// Use this for initialization
	void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F) && button_wait == false)
        {
            ButtonStart(RED_B);
        }
        if (Input.GetKeyDown(KeyCode.D) && button_wait == false)
        {
            ButtonStart(GREEN_B);
        }
        if (Input.GetKeyDown(KeyCode.S) && button_wait == false)
        {
            ButtonStart(YELLOW_B);
        }
        if (button_wait == true)
        {
            try
            {
                b_time = int.Parse(sp.ReadLine());
                button_wait = false;
                print(b_time);
            }
            catch (System.Exception)
            {

            }
        }
	}

    void ButtonStart(int button)
    {
        if (button == RED_B)
        {
            sp.WriteLine("B0");
            button_wait = true;
        } else if (button == YELLOW_B)
        {
            sp.WriteLine("B2");
            button_wait = true;
        } else if (button == GREEN_B)
        {
            sp.WriteLine("B1");
            button_wait = true;
        }
    }
}

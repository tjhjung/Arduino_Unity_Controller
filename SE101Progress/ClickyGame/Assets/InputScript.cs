using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class InputScript : MonoBehaviour {
    public GameObject input_obj;
    const int RED_B = 1;
    const int GREEN_B = 2;
    const int YELLOW_B = 3;
    protected bool button_wait = false;
    protected bool button_check_started = false;
    SerialPort sp = new SerialPort("COM3", 9600);

    // Use this for initialization
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonPress();
        //if (Input.GetKeyDown(KeyCode.V) && button_check_started == false)
        //{
            ButtonPress(RED_B);
        //}
        if (Input.GetKeyDown(KeyCode.C) && button_check_started == false)
        {
            ButtonPress(GREEN_B);
        }
        if (Input.GetKeyDown(KeyCode.X) && button_check_started == false)
        {
            ButtonPress(YELLOW_B);
        }
        /*
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
        */
        /*
        if (button_wait == true)
        {
            try
            {
                int b_time = int.Parse(sp.ReadLine());
                button_wait = false;
                print(b_time);
            }
            catch (System.Exception)
            {

            }
        }*/
    }
    /*
    void ButtonStart(int button)
    {
        if (button == RED_B)
        {
            sp.WriteLine("B1");
            print("Sent B1");
            button_wait = true;
        } else if (button == YELLOW_B)
        {
            sp.WriteLine("B3");
            print("Sent B3");
            button_wait = true;
        } else if (button == GREEN_B)
        {
            sp.WriteLine("B2");
            print("Sent B2");
            button_wait = true;
        }
    }
    */
    void ButtonPress(int button)
    {
        if (button_check_started == false)
        {
            if (button == RED_B)
            {
                sp.WriteLine("C1");
                print("Sent C1");
            }
            if (button == GREEN_B)
            {
                sp.WriteLine("C2");
                print("Sent C2");
            }
            if (button == YELLOW_B)
            {
                sp.WriteLine("C3");
                print("Sent C3");
            }
            button_check_started = true;
        }
    }

    bool CheckButtonPress()
    {
        try
        {
            string response = sp.ReadLine();
            if (response.Equals("BP"))
            {
                // Check Recieved
                sp.WriteLine("C4");
                //print("Sent C4");
                button_check_started = false;
                //print("Button Pressed");
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
}

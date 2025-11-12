// using the Fun With Unity guide: https://github.com/NickyDaB/IGME-470/blob/main/exercises/fun-with-unity/fun-with-unity.md
// using C# - Serial Read Write Using Threading (Part 3): https://www.youtube.com/watch?v=VIFqPVVqUOQ

using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.Events;
using System.IO;
using System.Threading;

public class DataStreamIn : MonoBehaviour
{
    private SerialPort dataStream;
    public string serialPort = "/dev/tty.debug-console";
    public int baud = 9600;
    private float receivedValue;
    private bool newData = false;
    public UnityEvent<float> NewDataAvailable = new UnityEvent<float>();
    private static DataStreamIn instance;
    public static DataStreamIn Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataStream = new SerialPort(serialPort, baud);
        dataStream.Open();
        // ReadSerialData();

        Thread ReadSerialThread = new Thread(ReadSerial);
        ReadSerialThread.Start();
    }

    public void SendMessage()
    {
        dataStream.Write(new byte[] { 0x00 }, 0, 1);
    }

    private void Update()
    {
        if (!newData) return;
        OnNewDataAvailable(receivedValue);
        newData = false;
    }

    private void ReadSerial()
    {
        while (dataStream.IsOpen)
        {
            try
            {
                string receivedString = dataStream.ReadLine();
                receivedValue = float.Parse(receivedString.Substring(0, 4));
                newData = true;
            }
            catch (IOException)
            {
                AttemptToReconnect();
            }
            catch (InvalidOperationException)
            {
                AttemptToReconnect();
            }
            catch (TimeoutException)
            {
                AttemptToReconnect();
            }
        }
    }

    private void OnNewDataAvailable(float data)
    {
        NewDataAvailable.Invoke(data);
    }

    private void AttemptToReconnect()
    {
        dataStream = new SerialPort(serialPort, baud);
        dataStream.Open();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recorder;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text clockText;
    [SerializeField] private Text pts;

    [SerializeField] private int points = 100;



    public RecordManager recordManager;

    private void Start()
    {
        pts.text = "Points: "+ points.ToString();
    }
    private void Update()
    {
        clockText.text = System.DateTime.Now.ToString();
    }

    public void DeductPoints (int amount)
    {
        points -= amount;
        pts.text = "Points: " + points.ToString();
    }
    //public void StartVid()
    //{
    //    recordManager.StartRecord();
    //}

    //public void SaveVid()
    //{
    //    recordManager.StopRecord();
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class UserLogin : MonoBehaviour
{
    public InputField userNameinput;
    public InputField gmailInput;
    public Text ScoreTxt;
    public string csvFileName = "UserData.csv";

    public Text[] UserNames;
    public Text[] Scores;

    private void Start()
    {
        LoadCSV();
    }


    public void AddData()
    {
        AddDataToCSV(userNameinput.text, ScoreTxt.text, gmailInput.text);
    }


    void AddDataToCSV(string userName, string Score, string gmailInput)
    {
        // Specify the path to the CSV file
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        try
        {
            // Read existing lines from the CSV file
            List<string> lines = new List<string>();
            if (File.Exists(filePath))
            {
                lines.AddRange(File.ReadAllLines(filePath));
            }

            // Format the new line of data
            string newLine = $"{gmailInput},{userName},{Score}";

            // Add the new line to the list of lines
            lines.Add(newLine);

            // Write all lines back to the CSV file
            File.WriteAllLines(filePath, lines.ToArray());

            Debug.Log("Data added to CSV: " + newLine);
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing to CSV file: " + e.Message);
        }
    }

    public void LoadCSV()
    {
        List<Tuple<string, int>> dataList = new List<Tuple<string, int>>();
        // List<Tuple<string, int>> List = new List<Tuple<string, int>>();

        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/UserData.csv");
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            dataList.Add(new Tuple<string, int>(values[1], int.Parse(values[2])));
            // List.Add(new Tuple<string, int>(values[0], int.Parse(values[1])));

        }

        dataList.Sort((a, b) => b.Item2.CompareTo(a.Item2));

        UserNames[0].text = dataList[0].Item1;
        UserNames[1].text = dataList[1].Item1;
        UserNames[2].text = dataList[2].Item1;
        UserNames[3].text = dataList[3].Item1;
        UserNames[4].text = dataList[4].Item1;

        Scores[0].text = dataList[0].Item2.ToString();
        Scores[1].text = dataList[1].Item2.ToString();
        Scores[2].text = dataList[2].Item2.ToString();
        Scores[3].text = dataList[3].Item2.ToString();
        Scores[4].text = dataList[4].Item2.ToString();
    }


    //// Check if the value is a valid integer before parsing
    //if (int.TryParse(values[2], out int score))
    //{

    //}
    //else
    //{
    //    Debug.LogError("Invalid score format in CSV: " + values[2]);
    //}
}

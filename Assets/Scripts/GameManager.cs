using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public TMP_InputField playerNameInput;

    public string playerWithHighscore;
    public int highestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }


    public void SetPlayerName()
    {
        playerName = playerNameInput.text;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerWithHighscore = playerWithHighscore;
        data.highestScore = highestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerWithHighscore = data.playerWithHighscore;
            highestScore = data.highestScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string playerWithHighscore;
        public int highestScore;

    }
}

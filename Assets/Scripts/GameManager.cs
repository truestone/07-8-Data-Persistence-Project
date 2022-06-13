using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI playerNameInput;
    public string highScorePlayerName;
    public string playerName;
    public int highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadHighScore();
        if (highScore > 0)
        {
            highScoreText.text = "Best Score : " +
                highScorePlayerName + " : " + highScore;
        }
    }

    public void StartGame()
    {
        playerName = playerNameInput.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class HighScoreData
    {
        public string highScorePlayerName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        HighScoreData data = new HighScoreData();
        data.highScorePlayerName = playerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath +
                "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
        }
    }
}

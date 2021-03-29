using System.Collections.Generic; 
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    #region Parameters

    private List<PlayerScores> leaderboard;
    private List<LevelScores> levelScores;
    public GameObject ScrollContent;
    public GameObject RowString; 

    private Levels levelStatsIn; 
    private List<Transform> highscoreEntryTransformList; 
    public GameObject starBar;

    private string level1_Stars;
    private string level2_Stars;
    private string level3_Stars;
    private string level4_Stars;
    private string level5_Stars;

    #endregion

    public void SaveLeaderBoard(int _id, string _name, float _time, string _stars, string _levelName)
    {
        var jsonStringLoaded = PlayerPrefs.GetString("highScoreTable");
        levelStatsIn = JsonUtility.FromJson<Levels>(jsonStringLoaded);

        switch (_levelName)
        {
            case "Apple":
                levelStatsIn.level_1.leaderboard = CheckRecord(_id, _time, _name, levelStatsIn.level_1.leaderboard);
                levelStatsIn.level_1.stars = _stars;
                break;
            case "Grapes":
                levelStatsIn.level_2.leaderboard = CheckRecord(_id, _time, _name, levelStatsIn.level_2.leaderboard);
                levelStatsIn.level_2.stars = _stars;
                break;
            case "PinApple":
                levelStatsIn.level_3.leaderboard = CheckRecord(_id, _time, _name, levelStatsIn.level_3.leaderboard);
                levelStatsIn.level_3.stars = _stars;
                break;
            case "Cherry":
                levelStatsIn.level_4.leaderboard = CheckRecord(_id, _time, _name, levelStatsIn.level_4.leaderboard);
                levelStatsIn.level_4.stars = _stars;
                break;
            case "Strawberry":
                levelStatsIn.level_5.leaderboard = CheckRecord(_id, _time, _name, levelStatsIn.level_5.leaderboard);
                levelStatsIn.level_5.stars = _stars;
                break;
        }

        string json = JsonUtility.ToJson(levelStatsIn);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();

        jsonStringLoaded = PlayerPrefs.GetString("highScoreTable");
        levelStatsIn = JsonUtility.FromJson<Levels>(jsonStringLoaded); 
    }

    private List<PlayerScores> CheckRecord(int _id, float _time, string _name, List<PlayerScores> leaderBoard)
    {
        for (var i = 0; i < leaderBoard.Count; i++)
        {
            var playerScores = leaderBoard[i];

            if (playerScores.id == _id)
            {
                if (playerScores.time < _time)
                {
                    leaderBoard.Add(new PlayerScores {id = _id, name = _name, time = _time});
                }
            }
            else
            {
                leaderBoard.Add(new PlayerScores {id = _id, name = _name, time = _time});
            }
        }

        return leaderBoard;
    }

    public void LoadLevelLeadersStats()
    {
        #region BaseSaveSrtring 
        string firstJsonSavesString = "{\"level_1\":{\"stars\":\"3\",\"leaderboard\":[{\"id\":1,\"name\":\"Derek\",\"time\":6.1},{\"id\":6,\"name\":\"Kor" +
                               "k21\",\"time\":12.1},{\"id\":64,\"name\":\"Kitty\",\"time\":18},{\"id\":3,\"name\":\"Lord1\",\"time\":23.4}]},\"level" +
                               "_2\":{\"stars\":\"4\",\"leaderboard\":[{\"id\":64,\"name\":\"Kitty\",\"time\":2.3},{\"id\":3,\"name\":\"Lord1\",\"tim" +
                               "e\":8.5},{\"id\":102,\"name\":\"Adam\",\"time\":12.1},{\"id\":3,\"name\":\"Lord1\",\"time\":23.4},{\"id\":31,\"name\":\"M" +
                               "ama\",\"time\":48.2},{\"id\":74,\"name\":\"AAAAAAAAAAAAAAAAAA\",\"time\":51.8},{\"id\":51,\"name\":\"Nag12Gan\",\"time\":60" +
                               ".1},{\"id\":52,\"name\":\"Killer\",\"time\":300.2}]},\"level_3\":{\"stars\":\"5\",\"leaderboard\":[{\"id\":60,\"name\":\"N" +
                               "ord\",\"time\":26.3},{\"id\":3,\"name\":\"Lord1\",\"time\":26.4},{\"id\":242,\"name\":\"Lizzy\",\"time\":30.3},{\"id\":80" +
                               "04,\"name\":\"TomTom\",\"time\":44.3},{\"id\":5,\"name\":\"Test\",\"time\":120.3}]},   \"level_4\":{\"stars\":\"3\",\"leade" +
                               "rboard\":[{\"id\":60,\"name\":\"Ned\",\"time\":12.3},{\"id\":3,\"name\":\"Lord1\",\"time\":14.4},{\"id\":242,\"name\":\"Li" +
                               "zzy\",\"time\":22.5},{\"id\":8004,\"name\":\"TomTom\",\"time\":40.1},{\"id\":5,\"name\":\"Test\",\"time\":102.2}]},\"leve" +
                               "l_5\":{\"stars\":\"5\",\"leaderboard\":[{\"id\":1111,\"name\":\"Korny\",\"time\":30}]}}";

        #endregion

        Transform entryContainer = transform.Find("HighscoreEntryContainer");
        Transform entryTemplate = entryContainer.Find("HighscoreEntryTemplate"); 

        var jsonStringLoaded = PlayerPrefs.GetString("highScoreTable");

        if (jsonStringLoaded.Length > 0)
        {
            levelStatsIn = JsonUtility.FromJson<Levels>(jsonStringLoaded);
        }
        else
        {
            jsonStringLoaded = firstJsonSavesString;
            levelStatsIn = JsonUtility.FromJson<Levels>(jsonStringLoaded);
            PlayerPrefs.SetString("highScoreTable", jsonStringLoaded);
        }

        //===================================================================
        var levelName = PlayerPrefs.GetString("LevelName");
        List<PlayerScores> playerScores = new List<PlayerScores>();
        string starCount = "";

        switch (levelName)
        {
            case "Apple":
                playerScores = levelStatsIn.level_1.leaderboard; 
                break;
            case "Grapes":
                playerScores = levelStatsIn.level_2.leaderboard; 
                break;
            case "PinApple":
                playerScores = levelStatsIn.level_3.leaderboard; 
                break;
            case "Cherry":
                playerScores = levelStatsIn.level_4.leaderboard; 
                break;
            case "Strawberry":
                playerScores = levelStatsIn.level_5.leaderboard; 
                break;
        }   

        //Sorting
        for (int i = 0; i < playerScores.Count; i++)
        {
            for (int j = 0; j < playerScores.Count; j++)
            {
                if (playerScores[j].time > playerScores[i].time)
                {
                    //Swap
                    PlayerScores tempo = playerScores[i];
                    playerScores[i] = playerScores[j];
                    playerScores[j] = tempo;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>(); 

        foreach (var item in playerScores)
        {
            LoadScores(item);
        }

        level1_Stars = levelStatsIn.level_1.stars;
        level2_Stars = levelStatsIn.level_2.stars;
        level3_Stars = levelStatsIn.level_3.stars;
        level4_Stars = levelStatsIn.level_4.stars;
        level5_Stars = levelStatsIn.level_5.stars; 
    }
     
    public void LoadScores(PlayerScores highScoreEntry)
    { 
        //Loading scores for leaderBoard
        var rowString = Instantiate(RowString);
        rowString.transform.parent = ScrollContent.transform;
        rowString.transform.localScale = new Vector3(1f, 1f);

        int rank = ScrollContent.transform.childCount - 1;
        string rankString;

        //defining ranking
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
        }

        rowString.gameObject.GetComponent<RowStringContent>().place.text = rankString;
        rowString.gameObject.GetComponent<RowStringContent>().time.text = highScoreEntry.time.ToString("00.0");
        rowString.gameObject.GetComponent<RowStringContent>().name.text = highScoreEntry.name;
        rowString.SetActive(true);  
    }

    public void ClearList()
    {
        //clear highScore table
        for (int i = 0; i < ScrollContent.transform.childCount; i++)
        {
            if (ScrollContent.transform.GetChild(i).name != "RowString")
            {
                Destroy(ScrollContent.transform.GetChild(i).gameObject); 
            }
        }
    }

    public void GetAllStars(string levelName)
    {
        string value = "0";

        switch (levelName)
        {
            case "Apple":
                value = level1_Stars;
                break;
            case "Grapes":
                value = level2_Stars;
                break;
            case "PinApple":
                value = level3_Stars;
                break;
            case "Cherry":
                value = level4_Stars;
                break;
            case "Strawberry":
                value = level5_Stars; 
                break;
        }

        starBar.gameObject.GetComponent<RatingStars>().StarsCount = int.Parse(value);
        //print("levelName: " + levelName + " stars: " + value);
    } 

    //Classes to serialize/deserialize

    [System.Serializable]
    public class PlayerScores
    {
        public int id;
        public string name;
        public float time;
    }

    [System.Serializable]
    public class LevelScores
    {
        public string stars;
        public List<PlayerScores> leaderboard;
    }

    [System.Serializable]
    public class Levels
    {
        public LevelScores level_1;
        public LevelScores level_2;
        public LevelScores level_3;
        public LevelScores level_4;
        public LevelScores level_5;
    }
}

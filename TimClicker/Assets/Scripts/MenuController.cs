using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Parameters

    public GameObject LevelX;
    public GameObject LevelReview;
    public GameObject[] Levels; 
    public GameObject ScrollContent;
     
    public Image ReviewFront;
    public Image ReviewIcon;
    public TextMeshProUGUI LevelName; 
    public GameObject LeaderBoardContent; 

    [Space]
    [SerializeField] private Sprite AppleReview;
    [SerializeField] private Sprite PinAppleReview;
    [SerializeField] private Sprite GrapesReview;
    [SerializeField] private Sprite CherryReview;
    [SerializeField] private Sprite StrawberryReview;
    [Space]
    [SerializeField] private Sprite AppleReviewBack;
    [SerializeField] private Sprite PinAppleReviewBack;
    [SerializeField] private Sprite GrapesReviewBack;
    [SerializeField] private Sprite CherryReviewBack;
    [SerializeField] private Sprite StrawberryReviewBack; 

    public GameObject settingsPanel;
    #endregion

    void Start()
    {
        AddNewLevel(true);

        if (PlayerPrefs.GetString("Music").Length > 0)
        {
            if (PlayerPrefs.GetString("Music") == "ON")
            {
                this.gameObject.GetComponent<MusicController>().MusicSwitcher(true);
            }
        }
        else
        {
            PlayerPrefs.SetString("Music", "ON");
            PlayerPrefs.SetString("Sounds", "ON");
            this.gameObject.GetComponent<MusicController>().MusicSwitcher(true);
        }
    } 

    public void AddNewLevel(bool standardLevel)
    {
        if (!standardLevel)
        {
            var levelX = Instantiate(LevelX);
            levelX.transform.parent = ScrollContent.transform;
            levelX.transform.localScale = new Vector3(1f, 1f);
        }
        else
        {
            foreach (var item in Levels)
            {
                var stLevel = Instantiate(item);
                stLevel.transform.parent = ScrollContent.transform;
                stLevel.transform.localScale = new Vector3(1f, 1f);
            }
        }
    }

    public void LevelReviewSwitcher(bool On_Off)
    { 
        var lvlName = PlayerPrefs.GetString("LevelName");

        //Changing sprites depend on level
        switch (lvlName)
        {
            case "Apple":
                ReviewFront.sprite = AppleReviewBack;
                ReviewIcon.sprite = AppleReview;
                break;
            case "PinApple":
                ReviewFront.sprite = PinAppleReviewBack;
                ReviewIcon.sprite = PinAppleReview;
                break;
            case "Grapes":
                ReviewFront.sprite = GrapesReviewBack;
                ReviewIcon.sprite = GrapesReview;
                break;
            case "Cherry":
                ReviewFront.sprite = CherryReviewBack;
                ReviewIcon.sprite = CherryReview;
                break;
            case "Strawberry":
                ReviewFront.sprite = StrawberryReviewBack;
                ReviewIcon.sprite = StrawberryReview;
                break;
        } 

        LevelName.text = lvlName;

        LeaderBoardContent.gameObject.GetComponent<Leaderboard>().LoadLevelLeadersStats();
        LeaderBoardContent.gameObject.GetComponent<Leaderboard>().GetAllStars(lvlName);
        LevelReview.SetActive(On_Off);
    }
      
    public void LoadLevel()
    {
        SceneManager.LoadScene("LevelX");
    }

    public void ShowSettings()
    {
        if (settingsPanel != null)
        {
            Animator animator = settingsPanel.GetComponent<Animator>();

            if (animator != null)
            {
                bool isOpen = animator.GetBool("Open");
                animator.SetBool("Open", !isOpen); 
            } 
        }
    }
}

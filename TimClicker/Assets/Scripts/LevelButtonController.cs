using TMPro;
using UnityEngine; 
using UnityEngine.UI;

[ExecuteInEditMode()]
public class LevelButtonController : MonoBehaviour
{
    #region Parameters  
     
    public string LevelNameText; 
    public Sprite HeaderStripeSprite; 
    public Sprite BodySprite; 
    public Sprite LevelLogoSprite;  
    public int InitialStarsCount;
    public GameObject LevelRating;
    private GameObject MainMenu;

    public TextMeshProUGUI LevelName;
    [Space]
    public Image Header;
    public Image Body;
    public Image LevelIcon;  
    #endregion

    private void Start()
    { 
        LevelName.text = LevelNameText;
        Header.sprite = HeaderStripeSprite;
        Body.sprite = BodySprite;
        LevelIcon.sprite = LevelLogoSprite;
        LevelRating.GetComponent<RatingStars>().StarsCount = InitialStarsCount;
        MainMenu = GameObject.FindWithTag("UI_MainMenu");
    } 
     
    public void LoadGame(string levelName)
    {
        PlayerPrefs.SetString("LevelName", levelName);

        if (MainMenu != null)
        {
            MainMenu.GetComponent<MenuController>().LevelReviewSwitcher(true);
        }
        else
        {
            Debug.Log("Error MainMenu is null");
        }
    }
}

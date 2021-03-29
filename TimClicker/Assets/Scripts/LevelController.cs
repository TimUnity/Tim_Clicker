using System;
using System.Collections; 
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    #region Parameters

    private string LevelName; 

    [SerializeField]
    private int fruitLimit;
    private int fruitTapedCount;
    public TextMeshProUGUI ScoreBoardLevelName;

    #region Sprites
    [SerializeField]
    private Image backGround;
    [SerializeField]
    private Image Icon;


    [SerializeField]
    private Image LevelFront;
    [SerializeField]
    private Image LevelIcon;

    private Sprite Splash;
    private Sprite fruitButtonSprite;

    [Space]
    [Header("Sprites")]
    [SerializeField] private Sprite AppleBack;
    [SerializeField] private Sprite PinAppleBack;
    [SerializeField] private Sprite GrapesBack;
    [SerializeField] private Sprite CherryBack;
    [SerializeField] private Sprite StrawberryBack;

    [Space]
    [SerializeField] private Sprite Apple;
    [SerializeField] private Sprite PinApple;
    [SerializeField] private Sprite Grapes;
    [SerializeField] private Sprite Cherry;
    [SerializeField] private Sprite Strawberry;

    [Space]
    [SerializeField] private Sprite AppleSplash;
    [SerializeField] private Sprite PinAppleSplash;
    [SerializeField] private Sprite GrapesSplash;
    [SerializeField] private Sprite CherrySplash;
    [SerializeField] private Sprite StrawberrySplash;


    [Space]
    [SerializeField] private Sprite bonuSpriteLime;
    [SerializeField] private Sprite bonuSpriteQuivi;
    [SerializeField] private Sprite bonusSpriteBanana;
    [SerializeField] private Sprite bonuSpriteSplash;
    #endregion

    #region Audio  
    public AudioClip[] FruitSplashSounds;
    #endregion

    #region GameObjects
     
    public GameObject GetReadyPanel;
    public GameObject GetReadyText;
    public GameObject ScoreBoardPanel;
    public GameObject settingsPanel; 
    public GameObject ProgressBar;
    public GameObject HighScoreTable;
    [Space] public GameObject InteractButton;
    [Space] public GameObject[] BonusButtons; 

    #endregion 

    private bool MusicON;
    private bool SoundsON;  
    private float minutes;
    private float seconds;
    private int secondsToRec;
    private bool timerON = false;
    private int blinkerCount;
    private bool SettingsOn;
    private bool DelayTimerOn;
    private bool ZoomOn;
    private bool DoubleScoreOn;

    private float startTime;
    private float stopTime;
    private float timerTime;

    public float spawnFruitDelay;
    public float hidefruitDelay;

    [Space] public TextMeshProUGUI timerCounterText;
    [Space] public TextMeshProUGUI fruitCounterText;
    [Space] public TextMeshProUGUI fruitLimitText;

    public Image fruitSplashImage;
    private float SplashX;
    private float SplashY;
    private float BonusSplashX;
    private float BonusSplashY;
    [SerializeField] private Color splashColorStart;
    [SerializeField] private Color splashColorEnd;

    Coroutine SpawnFruitRoutine = null;
    Coroutine HideFruitRoutine = null;
    Coroutine SpawnBonusFruitRoutine = null;
    Coroutine HideBonusFruitRoutine = null;

    private bool BonusModeDoubleScore;
    private bool BonusModeLongDelay;
    private bool BonusModeBigtarget;

    private int bonusButtonIndex;

    [SerializeField]
    private float FruitSpawnInterval;
    [SerializeField]
    private float BonusFruitSpawnInterval;

    private BonusButtonsControl BonusControl;

    #endregion

    private void Start()
    {
        fruitTapedCount = 0;
        blinkerCount = 0;
        BonusModeDoubleScore = false; 
        BonusModeLongDelay = false; 
        BonusModeBigtarget = false; 

        //Random choosing bonus
        bonusButtonIndex = Random.Range(0, 3);

        LevelName = PlayerPrefs.GetString("LevelName");

        if (PlayerPrefs.GetString("Music") == "ON")
        {
            MusicON = true;
            this.gameObject.GetComponent<MusicController>().MusicSwitcher(true);
        }
        else { MusicON = false; }

        if (PlayerPrefs.GetString("Sounds") == "ON") { SoundsON = true; }
        else { SoundsON = false; }

        switch (LevelName)
        { 
            case "Apple":
                 fruitLimit = 10; backGround.sprite = AppleBack; Icon.sprite = Apple; Splash = AppleSplash; fruitButtonSprite = Apple;
                 LevelFront.sprite = AppleBack;
                 LevelIcon.sprite = Apple;
                 break;
             case "PinApple":
                 fruitLimit = 15; backGround.sprite = PinAppleBack; Icon.sprite = PinApple; Splash = PinAppleSplash; fruitButtonSprite = PinApple;
                 LevelFront.sprite = PinAppleBack;
                 LevelIcon.sprite = PinApple;
                 break;
             case "Grapes":
                 fruitLimit = 20; backGround.sprite = GrapesBack; Icon.sprite = Grapes; Splash = GrapesSplash; fruitButtonSprite = Grapes;
                 LevelFront.sprite = GrapesBack;
                 LevelIcon.sprite = Grapes;
                break;
             case "Cherry":
                 fruitLimit = 25; backGround.sprite = CherryBack; Icon.sprite = Cherry; Splash = CherrySplash; fruitButtonSprite = Cherry;
                 LevelFront.sprite = CherryBack;
                 LevelIcon.sprite = Cherry;
                break;
             case "Strawberry":
                 fruitLimit = 30; backGround.sprite = StrawberryBack; Icon.sprite = Strawberry; Splash = StrawberrySplash; fruitButtonSprite = Strawberry;
                 LevelFront.sprite = StrawberryBack;
                 LevelIcon.sprite = Strawberry;
                break;
             case "NotStandart": //Newly added levels
                 fruitLimit = 30; backGround.sprite = StrawberryBack; Icon.sprite = Strawberry; Splash = StrawberrySplash; fruitButtonSprite = Strawberry;
                 LevelFront.sprite = StrawberryBack;
                 LevelIcon.sprite = Strawberry;
                break;
        }

        ScoreBoardLevelName.text = LevelName;
        InteractButton.GetComponent<Image>().sprite = fruitButtonSprite;
        fruitSplashImage.sprite = Splash;
        fruitLimitText.text = fruitLimit.ToString();

        ProgressBar.GetComponent<ProgressBar>().maximum = fruitLimit;
        ProgressBar.GetComponent<ProgressBar>().current = 0; 

        TimerReset();
        GetReadyPanel.SetActive(true);

        StartCoroutine(ObjectBlinker(0.5f)); 
    }

    public void TimerStart()
    {
        if (!timerON)
        {
            timerON = true;
            startTime = Time.time;
            //spawning fruitButton with FruitSpawnInterval + random
            SpawnFruitRoutine = StartCoroutine(FruitSpawn(FruitSpawnInterval));
            //spawning BonusfruitButton with FruitSpawnInterval + random
            SpawnBonusFruitRoutine = StartCoroutine(BonusFruitSpawn(BonusFruitSpawnInterval));
        }
    }

    public void TimerReset()
    { 
        stopTime = 0;
        timerON = false; 
        timerCounterText.text = "00:00";
    }

    public void TimerStop()
    {
        if (timerON)
        {
            timerON = false;
            stopTime = timerTime;
            try
            {
                StopCoroutine(SpawnFruitRoutine);
                StopCoroutine(SpawnBonusFruitRoutine);
                StopCoroutine(HideFruitRoutine);
                StopCoroutine(HideBonusFruitRoutine);
            }
            catch (Exception)
            {
                return;
            }
        }
    }

    private IEnumerator ObjectBlinker(float delay)
    { 
        yield return new WaitForSeconds(delay);
        blinkerCount++;
        GetReadyText.SetActive(false);
        StartCoroutine(Blink(0.5f));
    }

    private IEnumerator Blink(float delay)
    {
        yield return new WaitForSeconds(delay);
         
        if (blinkerCount < 4)
        {
            GetReadyText.SetActive(true);
            StartCoroutine(ObjectBlinker(0.5f));
        }
        else
        {
            GetReadyText.SetActive(false);
            GetReadyPanel.SetActive(false);
            TimerStart();
        }
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
                SettingsOn = isOpen;
            }

            if (!SettingsOn)
            {
                TimerStop();
            }
            else
            {
                TimerStart();
            }
        } 
    }

    private void Update()
    {  
        timerTime = stopTime + (Time.time - startTime);
        minutes = (int)timerTime / 60;
        seconds = (int)timerTime % 60; 

        if (timerON)
        {
            timerCounterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            secondsToRec = (int)timerTime % 60;
            secondsToRec += Convert.ToInt32(60 * minutes);
        } 
    }

    private IEnumerator BonusFruitSpawn(float delay)
    {
        if (timerON)
        { 
            yield return new WaitForSeconds(delay + Random.Range(5f, 10f));

            //Random selection of bonusButtons
            bonusButtonIndex = Random.Range(0, 3);  
            //setting random position of bonus button
            BonusButtons[bonusButtonIndex].transform.position = new Vector3(Random.Range(-2.1f, 2.1f), Random.Range(-3.1f, 3.1f), 90f);
            //using that position for splash sprite
            BonusSplashX = BonusButtons[bonusButtonIndex].transform.position.x;
            BonusSplashY = BonusButtons[bonusButtonIndex].transform.position.y;
            //Activating the bonus button
            BonusButtons[bonusButtonIndex].SetActive(true);
            //Starting co routine to deactivate bonusButton
            HideBonusFruitRoutine = StartCoroutine(HideBonusFruit(0.5f));
        }
    }

    private IEnumerator FruitSpawn(float delay)
    {  
        if (timerON)
        {
            // spawning fruits with different (randomized) delay
            yield return new WaitForSeconds(delay + spawnFruitDelay + Random.Range(0.1f, 0.4f));
            InteractButton.transform.position = new Vector3(Random.Range(-2.1f, 2.1f), Random.Range(-3.1f, 3.1f), 90f);

            //check for Bonus "BigTarget"
            if (BonusModeBigtarget) { InteractButton.transform.localScale = new Vector3(1.5f, 1.5f, 0f); }
            else { InteractButton.transform.localScale = new Vector3(1f, 1f, 0f); }


            SplashX = InteractButton.transform.position.x;
            SplashY = InteractButton.transform.position.y;

            InteractButton.SetActive(true); 
            HideFruitRoutine = StartCoroutine(HideFruit(0.5f));
        }
    }

    private IEnumerator HideFruit(float delay)
    {
        // checking Delay bonus
        if (BonusModeLongDelay) { delay += delay * 2; } 

        yield return new WaitForSeconds(delay + hidefruitDelay);
        InteractButton.SetActive(false);
        StartCoroutine(FruitSpawn(0.5f));
    }

    private IEnumerator HideBonusFruit(float delay)
    {  
        //Hiding bonus button with delay
        yield return new WaitForSeconds(delay + hidefruitDelay);
        BonusButtons[bonusButtonIndex].SetActive(false);
        //Starting bonusButton spawning again
        StartCoroutine(BonusFruitSpawn(0.5f));
    }

    public void FruitTap()
    {
        //Stop co routine of "hiding with delay" and hide fruit immediately
        StopCoroutine(SpawnFruitRoutine);
        StopCoroutine(HideFruitRoutine); 
        InteractButton.SetActive(false);

        ShowSplash(false);

        //spawn fruit usual way   
        SpawnFruitRoutine = StartCoroutine(FruitSpawn(0.5f)); 

        //Count taped fruits
        //check for Bonus "DoubleScore"
        if (BonusModeDoubleScore) { fruitTapedCount += 2; }
        else { fruitTapedCount++; }

        if (fruitTapedCount < fruitLimit)
        { 
            fruitCounterText.text = fruitTapedCount.ToString();
            ProgressBar.GetComponent<ProgressBar>().current = fruitTapedCount;
        }
        else
        {
            if (fruitTapedCount > fruitLimit)
            {
                fruitTapedCount = fruitLimit;
            }

            fruitCounterText.text = fruitTapedCount.ToString(); 
            ProgressBar.GetComponent<ProgressBar>().current = fruitTapedCount;
            StopCoroutine(SpawnFruitRoutine);
            StopCoroutine(HideFruitRoutine);
            InteractButton.SetActive(false);
            fruitSplashImage.enabled = false;

            FinishLevel();
        }

        AudioSource.PlayClipAtPoint(FruitSplashSounds[Random.Range(0, 5)], gameObject.transform.position, 0.1f);
    }

    public void BonusFruitTap(string bonusType)
    { 
        switch (bonusType)
        {
            case "BigTarget":
                CheckBonuses("BigTarget"); 
                break;
            case "LongDelay":
                CheckBonuses("LongDelay");
                break;
            case "DoubleScore": 
                CheckBonuses("DoubleScore");
                break;
        }

        AudioSource.PlayClipAtPoint(FruitSplashSounds[Random.Range(0, 5)], gameObject.transform.position, 0.1f);

        //Stop co routine of "hiding with delay" and hide bonus fruit immediately
        StopCoroutine(SpawnBonusFruitRoutine);
        StopCoroutine(HideBonusFruitRoutine);
        foreach (var item in BonusButtons)
        { 
            item.SetActive(false);
        }

        ShowSplash(true); 

        //Starting co routine to shutdown enabled bonuses;
        StartCoroutine(BonusKill(6f, bonusType));

        //spawn bonus fruit usual way   
        SpawnBonusFruitRoutine = StartCoroutine(BonusFruitSpawn(0.5f));
         

    }

    private void ShowSplash(bool bonusSplash)
    {
        //Juicy splash appearing and disappearing
        fruitSplashImage.DOColor(splashColorStart, 0f);

        if (bonusSplash)
        {
            fruitSplashImage.sprite = bonuSpriteSplash;
            fruitSplashImage.transform.position = new Vector3(BonusSplashX, BonusSplashY, 0);
        }
        else
        { 
            fruitSplashImage.sprite = Splash;
            fruitSplashImage.transform.position = new Vector3(SplashX, SplashY, 0);
        }

        fruitSplashImage.enabled = true;
        fruitSplashImage.DOColor(splashColorEnd, 2f);
    }

    public void FinishLevel()
    {
        if (settingsPanel != null)
        { 
            TimerStop();

            string stars = "4";

            if (secondsToRec <= 44) { stars = "5"; }
            if (secondsToRec >= 45) { stars = "4"; }
            if (secondsToRec >= 55) { stars = "3"; }

            HighScoreTable.GetComponent<Leaderboard>().SaveLeaderBoard(777, "NINJA", secondsToRec, stars, LevelName);

            HighScoreTable.GetComponent<Leaderboard>().LoadLevelLeadersStats();
            HighScoreTable.GetComponent<Leaderboard>().GetAllStars(LevelName);
            Animator animator = ScoreBoardPanel.GetComponent<Animator>();

            if (animator != null)
            {
                bool isOpen = animator.GetBool("Open");
                animator.SetBool("Open", !isOpen);
            }
        } 
    }

    private IEnumerator BonusKill(float delay, string bonusName)
    { 
        yield return new WaitForSeconds(delay);
        
        switch (bonusName)
        {
            case "LongDelay":
                BonusModeLongDelay = false; 
                break;
            case "DoubleScore":
                BonusModeDoubleScore = false; 
                break;
            case "BigTarget":
                BonusModeBigtarget = false; 
                break;
        }

        this.gameObject.GetComponent<BonusButtonsControl>().SwitchBonusStatus(bonusName, false);
    }

    private void CheckBonuses(string bonusName)
    {
        switch (bonusName)
        {
            case "LongDelay":
            {
                if (!BonusModeLongDelay)
                {
                    BonusModeLongDelay = true;
                    this.gameObject.GetComponent<BonusButtonsControl>().SwitchBonusStatus(bonusName, true);
                }

                break;
            }
            case "DoubleScore":
            {
                if (!BonusModeDoubleScore)
                {
                    BonusModeDoubleScore = true;
                    this.gameObject.GetComponent<BonusButtonsControl>().SwitchBonusStatus(bonusName, true);
                }

                break;
            }
            case "BigTarget":
            {
                if (!BonusModeBigtarget)
                {
                    BonusModeBigtarget = true;
                    this.gameObject.GetComponent<BonusButtonsControl>().SwitchBonusStatus(bonusName, true);
                }

                break;
            }
        }
    } 

    public void RestartLevel()
    {
        SceneManager.LoadScene("LevelX");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    } 
}

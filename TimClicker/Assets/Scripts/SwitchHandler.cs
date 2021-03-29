using System; 
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHandler : MonoBehaviour
{
    #region Parameters

    public AudioSource audioSource;
    private int switchState = 1;
    public GameObject switchBtn;
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    [SerializeField] private float tweenTime = 0.25f;
    [SerializeField] private string target;
    public GameObject mainMusicAudioSource;

    #endregion

    private void Start()
    { 
        audioSource = this.GetComponent<AudioSource>();
        DefineToggleOnLoad();
    }

    public void OnSwitchButtonClicked()
    {
        ToggleSwitchButton();
    }

    private void DefineTargetAndToggle(bool value)
    {
        if (target == "Music")
        {
            ToggleMusic(value);
        }

        if (target == "Sounds")
        {
            ToggleSounds(value);
        }
    }

    private void ToggleSounds(bool On)
    {
        PlayerPrefs.SetString("Sounds", On ? "ON" : "OFF");
    }

    private void ToggleMusic(bool On)
    {  
        if (On)
        {
            PlayerPrefs.SetString("Music", "ON");
            mainMusicAudioSource.gameObject.GetComponent<MusicController>().MusicSwitcher(true);
        }
        else
        { 
            PlayerPrefs.SetString("Music", "OFF");
            mainMusicAudioSource.gameObject.GetComponent<MusicController>().MusicSwitcher(false);
        }
    }

    private void ToggleColor(bool value)
    {
        backgroundImage.DOColor(value ? onColor : offColor, tweenTime);
    }

    private void DefineToggleOnLoad()
    {
        if (target == "Music")
        {
            if (PlayerPrefs.GetString("Music") == "ON")
            {
                switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, tweenTime);
                ToggleColor(true);
            }
            else
            {
                switchBtn.transform.DOLocalMoveX(switchBtn.transform.localPosition.x, tweenTime);
                ToggleColor(false);
            }

            switchState = Math.Sign(-switchBtn.transform.localPosition.x);
        }

        if (target == "Sounds")
        {
            if (PlayerPrefs.GetString("Sounds") == "ON")
            {
                switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, tweenTime);
                ToggleColor(true);
            }
            else
            {
                switchBtn.transform.DOLocalMoveX(switchBtn.transform.localPosition.x, tweenTime);
                ToggleColor(false);
            }

            switchState = Math.Sign(-switchBtn.transform.localPosition.x);
        }
    }

    private void ToggleSwitchButton()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, tweenTime);
        switchState = Math.Sign(-switchBtn.transform.localPosition.x); 

        if (switchState > 0)
        {
            DefineTargetAndToggle(true);
            ToggleColor(true);
        }
        else
        {
            DefineTargetAndToggle(false);
            ToggleColor(false);
        }

        //Play button sound
        if (PlayerPrefs.GetString("Sounds") == "ON")
        {
            try
            {
                audioSource.Play();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}

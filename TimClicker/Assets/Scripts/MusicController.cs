using UnityEngine;

public class MusicController : MonoBehaviour
{
    public void MusicSwitcher(bool On)
    {
        if (On)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}

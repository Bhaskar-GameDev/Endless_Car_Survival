using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private bool isSoundOn = true;

    public void ToggleSounds()
    {
        isSoundOn = !isSoundOn;

        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    
}

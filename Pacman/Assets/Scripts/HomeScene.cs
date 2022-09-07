using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeScene : MonoBehaviour
{
    public AudioSource bgSound;
    private int numberOfTimesClicked = 0;
    public TextMeshProUGUI muteText;

    private void Awake()
    {
        this.bgSound = GetComponent<AudioSource>();
    }

    public void MuteButton()
    {
        this.numberOfTimesClicked += 1;

        if (numberOfTimesClicked % 2 == 0)
        {
            this.bgSound.mute = false;
            this.muteText.text = "Mute sound";
        }
        else if (numberOfTimesClicked % 2 == 1)
        {
            this.bgSound.mute = true;
            this.muteText.text = "Unmute sound";
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject menuCanvas;

    public GameObject muteButton;
    public GameObject muteButton2;
    public Sprite mutedButtonSprite;
    public Sprite unmutedButtonSprite;
    public Slider volumeSlider;
    public bool isMuted;

    private void Start()
    {
        isMuted = false;
       // AudioListener.pause = false;
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            AudioListener.volume = 0.5f;
            volumeSlider.value = 0.5f;
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas.activeSelf)
            {
                menuCanvas.SetActive(false);
            }
            else
            {
                menuCanvas.SetActive(true);
                if (isMuted)
                {
                    muteButton.GetComponent<Image>().sprite = mutedButtonSprite;
                }
                else
                {
                    muteButton.GetComponent<Image>().sprite = unmutedButtonSprite;
                }
            }
        }
    }

    public void ToggleMute()
    {
        if (isMuted)
        {
            muteButton.GetComponent<Image>().sprite = unmutedButtonSprite;
            muteButton2.GetComponent<Image>().sprite = unmutedButtonSprite;
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            isMuted = false;
        }
        else{
            muteButton.GetComponent<Image>().sprite = mutedButtonSprite;
            muteButton2.GetComponent<Image>().sprite = mutedButtonSprite;
            AudioListener.volume = 0.0f;
            isMuted = true;
        }
    }

    public void Continue()
    {
        menuCanvas.SetActive(false);
    }

    public void UpdateSoundVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        muteButton.GetComponent<Image>().sprite = unmutedButtonSprite;
        isMuted = false;
    }

    public void ReturnToLoby()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}

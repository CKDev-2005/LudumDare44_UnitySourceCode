using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public Slider volume;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        scoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        volume.value = PlayerPrefs.GetFloat("Volume");
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Volume", volume.value);

        Camera.main.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");

    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

}

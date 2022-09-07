using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI muteText;

    public AudioSource bgSound;
    private int numberOfTimesClicked = 0;

    public int ghostMultiplier { get; private set; }
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Awake()
    {
        this.bgSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        } 
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        this.gameOverText.enabled = false;
        this.restartText.enabled = false;
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();

        ResetGhostMultiplier();
    }

    private void GameOver()
    {
        this.gameOverText.enabled = true;
        this.restartText.enabled = true;

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        this.livesText.text = "x" + lives.ToString();
    }

    //following functions are going to be triggered from other scripts

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.ghostEatenPoints * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.pelletEatenPoints);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }

    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

    public void MuteButton()
    {
        this.numberOfTimesClicked += 1;

        if (numberOfTimesClicked % 2 == 0)
        {
            this.bgSound.mute = false;
            this.muteText.text = "Mute";
        }
        else if (numberOfTimesClicked % 2 == 1)
        {
            this.bgSound.mute = true;
            this.muteText.text = "Unmute";
        }
    }
}

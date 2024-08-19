using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RPS : MonoBehaviour
{
    public Dropdown dropdown;
    public Text text;
    public Button startButton;
    public Button restartButton;
    public bool isStart = false;

    public float hareketHizi = 2f;
    public int minX, minY = 10;
    public int maxX, maxY = 10;
    public bool isRock = true;
    public bool isPaper = false;
    public bool isScissors = false;

    private SpriteRenderer spriteRenderer;
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    private int paperCount = 10;
    private int scissorsCount = 10;
    private int rockCount = 10;

    public string winner = "";
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        restartButton.onClick.AddListener(RestartGame);
        startButton.onClick.AddListener(StartGame);
        restartButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isStart)
        {
            UpdateCounts();
            Vector2 hareketYonu = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Vector2 yeniPozisyon = (Vector2)transform.position + hareketYonu.normalized * hareketHizi * Time.deltaTime;

            // Nesneyi sınırlar içinde tut
            yeniPozisyon.x = Mathf.Clamp(yeniPozisyon.x, minX, maxX); // minX ve maxX değerlerini ayarlayın
            yeniPozisyon.y = Mathf.Clamp(yeniPozisyon.y, minY, maxY); // minY ve maxY değerlerini ayarlayın

            // Yeni pozisyonu atama
            transform.position = yeniPozisyon;

            if (isRock)
            {
                spriteRenderer.sprite = rockSprite;
            }
            else if (isPaper)
            {
                spriteRenderer.sprite = paperSprite;
            }
            else if (isScissors)
            {
                spriteRenderer.sprite = scissorsSprite;
            }

            

            if (paperCount == 30)
            {
                winner = "paper";
                restartButton.interactable = true;
            }
            else if (scissorsCount == 30)
            {
                winner = "scissor";
                restartButton.interactable = true;
            }
            else if (rockCount == 30)
            {
                winner = "rock";
                restartButton.interactable = true;
            }

            if (rockCount == 0 || scissorsCount == 0 || paperCount == 0)
            {
                if (rockCount == 30 || scissorsCount == 30 || paperCount == 30)
                {
                    hareketHizi = 0;
                }
                else
                {
                    hareketHizi = 10;
                }
            }


            if (!string.IsNullOrEmpty(winner))
            {
                if (dropdown.captionText.text.ToLower() == winner.ToLower())
                {

                    text.text = "You Win! :)";
                }
                else
                {

                    text.text = "You Lose :(";
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isPaper)
        {
            if (other.gameObject.CompareTag("rock"))
            {
                other.gameObject.GetComponent<RPS>().isRock = false;
                other.gameObject.GetComponent<RPS>().isPaper = true;
                other.gameObject.tag = "paper";
                paperCount++;
                rockCount--;
            }
            else if (other.gameObject.CompareTag("scissors"))
            {
                isPaper = false;
                isScissors = true;
                gameObject.tag = "scissors";
                scissorsCount++;
                paperCount--;
            }
        }
        if (isScissors)
        {
            if (other.gameObject.CompareTag("paper"))
            {
                other.gameObject.GetComponent<RPS>().isPaper = false;
                other.gameObject.GetComponent<RPS>().isScissors = true;
                other.gameObject.tag = "scissors";
                scissorsCount++;
                paperCount--;
            }
            else if (other.gameObject.CompareTag("rock"))
            {
                isScissors = false;
                isRock = true;
                gameObject.tag = "rock";
                rockCount++;
                scissorsCount--;
            }
        }
        if (isRock)
        {
            if (other.gameObject.CompareTag("scissors"))
            {
                other.gameObject.GetComponent<RPS>().isScissors = false;
                other.gameObject.GetComponent<RPS>().isRock = true;
                other.gameObject.tag = "rock";
                rockCount++;
                scissorsCount--;
            }
            else if (other.gameObject.CompareTag("paper"))
            {
                isRock = false;
                isPaper = true;
                gameObject.tag = "paper";
                paperCount++;
                rockCount--;
            }
        }
        UpdateCounts();
    }
    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        return objectsWithTag.Length;
    }
    private void UpdateCounts()
    {
        paperCount = CountObjectsWithTag("paper");
        scissorsCount = CountObjectsWithTag("scissors");
        rockCount = CountObjectsWithTag("rock");
    }
    public void StartGame()
    {
        isStart = true;
        dropdown.interactable = false;
        text.text = "";
    }
    void RestartGame()
    {
        // Oyunu yeniden başlatmak için sahneyi yeniden yükle
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }



}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace Car{
public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 20f;
    public float speedIncreaseRate = 0.1f;
    private float acceleration;

    public float distanceTraveled = 0f;
    public AudioSource audioSource;
    public AudioSource gop;
    private bool isHolding;
    public float HoldingAcceleration = 0.5f;
    public TMP_Text speedText;
    public TMP_Text distanceText;
    public GameObject gameOverPanel;
    public GameObject GameBoard;
    public TMP_Text distanceTextOnPanel;
    public TMP_Text highScoreTextonGop;
    public TMP_Text HighScoreTextinGame;
    public TMP_Text MessageTMP;
    public TMP_Text messageText;
    public TMP_Text pinkDiamonds;
    public GameObject respawnTransform;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
        gameOverPanel.SetActive(false);
          if(PlayerPrefs.GetInt("SoundOn")==1)
        {
            audioSource.Play();
        }
        GameBoard.SetActive(true);
        MessageTMP.text = "Tap and Hold Screen To Increase Speed";
        Invoke("RemoveText",3f);
    }
    private void RemoveText()
    {
        MessageTMP.text = "";
    }

    private void Update()
    {
        HandleMovement();
        UpdateUI();
        SetPitchDynamic();
        DyanmicAcceleration();
        fixedRotation();
        transform.rotation = Quaternion.identity;

    }

    private void HandleMovement()
    {

        float horizontalInput;

        horizontalInput=Input.acceleration.x;

        Vector3 horizontalMoveDirection = new Vector3(horizontalInput, 0, 0);
        Vector3 forwardMoveDirection = transform.forward;

        float horizontalDistance = horizontalMoveDirection.magnitude* 3f* speed * Time.deltaTime;
        float forwardDistance = forwardMoveDirection.magnitude * speed * Time.deltaTime;

        transform.Translate(horizontalMoveDirection * horizontalDistance);
        transform.Translate(forwardMoveDirection * forwardDistance);

        distanceTraveled += forwardDistance;
        acceleration=isHolding?HoldingAcceleration:speedIncreaseRate;
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    private void UpdateUI()
    {
        speedText.text = "" + speed.ToString("0");
        distanceText.text = "" + distanceTraveled.ToString("0");
        HighScoreTextinGame.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void SetPitchDynamic()
    {
      
        if (speed > 0)
        {
           float pitch = Mathf.Lerp(0.05f, 0.5f, speed / maxSpeed);
           audioSource.pitch = pitch;
           audioSource.volume=10f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("O"))
        {
            
            gameOverPanel.SetActive(true);
       
            GameBoard.SetActive(false);
            Time.timeScale = 0;
            audioSource.Stop();
            distanceTextOnPanel.text = "" + distanceTraveled.ToString("0");
            int score = (int)distanceTraveled;
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            highScoreTextonGop.text = PlayerPrefs.GetInt("HighScore").ToString("0");
            if(PlayerPrefs.GetInt("SoundOn")==1)
            {
                gop.Play(); 
            }
        }
            
        pinkDiamonds.text = PlayerPrefs.GetInt("PinkDiamonds").ToString();
    }

  public void Retry()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1;
    GameBoard.SetActive(true);
  }
  public void ReturnMainMenu()
  {
    SceneManager.LoadScene("Lobby");
    Time.timeScale=1f;
  }
  private void DyanmicAcceleration()
  {
    if(Input.touchCount>0)
    {
        Touch touch = Input.GetTouch(0);
        if(touch.phase==TouchPhase.Began)
        {
            isHolding=true;
        }
        else if(touch.phase==TouchPhase.Ended)
        {
            isHolding = false;
        }
        
    }
  }
  public void Respawn()
  {
    Debug.Log("Respawn called");
    transform.position = new Vector3(0f,respawnTransform.transform.position.y,respawnTransform.transform.position.z);
    speed = 30;
    Time.timeScale=1f;
    gameOverPanel.SetActive(false);
    GameBoard.SetActive(true);
    transform.rotation = respawnTransform.transform.rotation;
 
  }
  private void fixedRotation()
  {
    respawnTransform.transform.rotation = transform.rotation;
  }
 

    public void OnRespawn()
    {
        if(PlayerPrefs.GetInt("PinkDiamonds")>0)
        {
            PlayerPrefs.SetInt("PinkDiamonds", PlayerPrefs.GetInt("PinkDiamonds") - 1);
            Respawn();
            if(PlayerPrefs.GetInt("SoundOn")==1)
            {
                audioSource.Play();
            }
        }
        else
        {
            messageText.text = "Insufficient PinkDiamonds";
        }
    }
}}

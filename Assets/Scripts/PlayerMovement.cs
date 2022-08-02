using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text initialcountDownText;
    [SerializeField] private TMP_Text winFailText;
    public GameObject pauseBtn;
    public GameObject playBtn;
    public GameObject gameEndText;
    public GameObject countDownTimer;
    public GameObject panel;
    public AudioSource btnAudio;
    public AudioSource gameMusic;
    //movement is a 3d vector that is decleared to store the movement vector input from the user
    Vector3 movement;
    //horizontal is used to store the horizontal input from the user
    float horizontal;
    //vertical is used to store the vertical input from the user.in our game player body always move vertically and so its value is initially
    //set as 1 
    float vertical=1;
    float startTimer=3f;
    float timeCounter;
    int seconds;
    float highscore;
    bool gameEnded;
    //move speed is used to control the speed of player and can be adjusted in the inspector tab.
    public float movespeed=5f;
    //Below boolean variables are used to store the true/false values depending on the button pressing action from the user.
    // bool isQuit=false;
    // bool isRestart=false;
    //bool isLeft=false;
    //bool isRight=false;

    //start function is called automatically in the first frame update 
    private void Start() 
    {
        
        timeCounter=0f;
        gameEnded=false;
        highscore=PlayerPrefs.GetInt("highscore");
        playBtn.SetActive(true);
        gameEndText.SetActive(false);
        Time.timeScale=1;
        
    }

    //automatically gets called whenever a collision occures with our player

    private void OnCollisionEnter(Collision other) 
    {
        //here checks wheather the collision is happened with any traps ,which have given with a tag "spike" in the inspector.
        if(other.gameObject.CompareTag("spike"))
        {
            winFailText.text="Oops!!";
            gameEnded=true;

        }
    }

    //Automatically gets called whenever player enters an istrigger collider and here in the game it is the finish region
    private void OnTriggerEnter(Collider other) 
    {
        //checks the region has a "finishing point" tag name 
        if(other.gameObject.CompareTag("finishing point"))
        {
            winFailText.text="You win!!";
            
            if(seconds<highscore)
            {
                PlayerPrefs.SetInt("highscore",seconds);
                winFailText.text="New highscore!!";

            }
            gameEnded=true;
            
        }
        
    }
    //Update function is automatically called in every single frame

    void Update()
    {
        startTimer-=Time.deltaTime;
        if(startTimer>=0)
        {
            initialcountDownText.text=startTimer.ToString("0");
        }
        else if(startTimer<=-1 && startTimer>-2)
        {
            initialcountDownText.text="GO";
        }
        else if(startTimer<=-2)
        {
            countDownTimer.SetActive(false);
            //here the horizontal input from the user is initialized to our horizontal variable using Input.GetAxisRaw function
            horizontal=Input.GetAxisRaw("Horizontal");
            vertical=Input.GetAxisRaw("Vertical");
            if(vertical==-1)
            {
                vertical=0;
            }
            if(!gameEnded)
            {
                timeCounter+=Time.deltaTime;
                seconds=(int)(timeCounter%60);
            }
            timer.text="time : "+seconds+"s";


            //the given input values from the horizontal and vertical variables are passed to the movement vector using Set function and normalized
            //using Normalize function
            movement.Set(horizontal,0f,vertical);
            movement.Normalize();

            //the position of player is varied using the transform component of our player object using movetowards function
            //parameters passed to the MoveTowards function are the initial position,final position and speed .
            //Time.deltaTime is used to get a freame independant motion 
            transform.position = Vector3.MoveTowards(transform.position,transform.position+movement,movespeed*Time.deltaTime);

            if(gameEnded)
            {
                Time.timeScale=0;
                pauseBtn.SetActive(false);
                panel.SetActive(true);
                playBtn.SetActive(false);
                gameEndText.SetActive(true);
            }
        }



        
        
       
    }
    //These functions are called by onclick functions of corresponding buttons
    public void PauseBtnclick()
    {
        Time.timeScale=0;
        pauseBtn.SetActive(false);
        panel.SetActive(true);
        gameMusic.Pause();
        btnAudio.Play();

    }
    public void playBtnClick()
    {
        Time.timeScale=1;
        pauseBtn.SetActive(true);
        panel.SetActive(false);
        btnAudio.Play();
        gameMusic.UnPause();
    }
    public void replay()
    {
        Time.timeScale=1;
        pauseBtn.SetActive(false);
        panel.SetActive(true);
        SceneManager.LoadScene(1);
        btnAudio.Play();
    }
    public void back()
    {
        Time.timeScale=1;
        pauseBtn.SetActive(false);
        panel.SetActive(true);
        SceneManager.LoadScene(0);
        btnAudio.Play();
    }

    //The following functions are associated with the ui button elements in the inspector tab and that is done in the inspector tab and 
    //these functions get automatically called by the triggering action of buttons such as pressing up and down as their name says
    //in these functions the corresponding boolean values are set for checking in the update function

    // public void isQuitUp()
    // {
    //     isQuit=false;
    // }
    // public void isQuitDown()
    // {
    //     isQuit=true;
    // }
    // public void isRestartUp()
    // {
    //     isRestart=false;
    // }
    // public void isRestartDown()
    // {
    //     isRestart=true;
    // }
/*    public void isLeftDown()
    {
        isLeft=true;
    }
    public void isLeftUp()
    {
        isLeft=false;
    }
    public void isRightDown()
    {
        isRight=true;
    }
    public void isRightUp()
    {
        isRight=false;
    }
*/
}


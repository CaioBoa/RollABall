using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    private int count;
    private float time;
    private float popUpTimer;
    public AudioSource PickUpSound;
    public AudioSource EnemySound;
    public TextMeshProUGUI timeText;
    public GameObject PopUpTextObject;
    public TextMeshProUGUI PopUpText;
    public Slider slider;
    public GameObject WarningText;

    void Start()
    {
      count = 20; 
      time = 60;
      rb = GetComponent <Rigidbody>(); 
      SetCountBar();
      SetTimeText();
      PopUpTextObject.SetActive(false);
      WarningText.SetActive(false);
    }

   private void FixedUpdate() 
   {
     Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
     rb.AddForce(movement * 1000 * Time.deltaTime);
   }

   void OnMove (InputValue movementValue)
   {
     Vector2 movementVector = movementValue.Get<Vector2>();
     movementX = movementVector.x; 
     movementY = movementVector.y; 
   }

    void SetTimeText() 
    {
      int Minutes = Mathf.FloorToInt(time / 60F);
      int Seconds = Mathf.FloorToInt(time - Minutes * 60);
      timeText.text = Minutes.ToString() + ":" + Seconds.ToString();
      if (time <= 0)
        {   
            PlayerPrefs.SetInt("GameOver", 0);
            PlayerPrefs.SetFloat("Time", time);
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
   void SetCountBar() 
   {
     slider.value = count;
     if (count <= 0)
       {  
          StartCoroutine(BlinkWarningText());
       }
   }

   void OnTriggerEnter(Collider other) 
   {
     if (other.gameObject.CompareTag("PickUp")) 
       {
          PickUpSound.Play();
          other.gameObject.SetActive(false);
          count -= 1;
          SetCountBar();
          PlayerPrefs.SetInt("Score", 20 - count);
       }
      if (other.gameObject.CompareTag("Enemy")) 
       {
          time -= 10;
          PopUpText.color = new Color(1, 0, 0, 1);
          PopUpText.text = "- 10";
          PopUpTextObject.SetActive(true);
          popUpTimer = 1.0f;
          EnemySound.Play();
       }
       if (other.gameObject.CompareTag("PickUpTime")) 
       {  
          time += 10;
          PickUpSound.Play();
          PopUpText.color = new Color(0.1f, 1, 0, 1);
          PopUpText.text = "+ 10";
          PopUpTextObject.SetActive(true);
          other.gameObject.SetActive(false);
          popUpTimer = 1.0f;
       }
       if (other.gameObject.CompareTag("Goal"))
        {
            if (count == 0)
            {
                PlayerPrefs.SetInt("GameOver", 1);
                PlayerPrefs.SetFloat("Time", time);
                SceneManager.LoadSceneAsync("GameOver");
            }
        }
   }

   void Update()
   {
    time = time - Time.deltaTime;
    SetTimeText();
    if (transform.position.y < -20){
        transform.position = new Vector3(0, 0.5f, 0);
        time -= 10;
        PopUpText.color = new Color(1, 0, 0, 1);
        PopUpText.text = "- 10";
        PopUpTextObject.SetActive(true);
        popUpTimer = 1.0f;
    }
    if (popUpTimer > 0)
    {
        popUpTimer -= Time.deltaTime;
    }
    else
    {
        PopUpTextObject.SetActive(false);
    }
  }

    IEnumerator BlinkWarningText()
    {
        WarningText.SetActive(true); // Ativa o texto
        float blinkDuration = 5.0f; // Duração de 3 segundos
        float blinkInterval = 0.3f; // Intervalo para o piscar (ajustável)

        // Pisco o texto por 3 segundos
        float timer = 0f;
        while (timer < blinkDuration)
        {
            WarningText.SetActive(!WarningText.activeSelf); // Alterna entre ativo/inativo
            yield return new WaitForSeconds(blinkInterval); // Aguarda o intervalo de piscar
            timer += blinkInterval;
        }

        WarningText.SetActive(false); // Desativa o texto após 3 segundos
    }

}

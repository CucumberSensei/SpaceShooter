using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _nextFire = -1f;
   
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private int _lives = 3;
    private bool _isAlive = true;

    [SerializeField]
    private bool _tripleShot = false;
    [SerializeField]
    private bool _speedBoost = false;
    [SerializeField]
    private bool _shield = false;

    [SerializeField]
    private GameObject _shieldVisual;

    [SerializeField]
    private int _score;
    private int _bestScore;
    private UI_Manager _uiManager;

    [SerializeField]
    private GameObject _rightEngineDamage;
    [SerializeField]
    private GameObject _leftEngineDamage;

    [SerializeField]
     private AudioClip _laserClip;
     private AudioSource _audioSource;




    
    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if (_uiManager == null )
        {
            Debug.LogError("UI_Manager is Null");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
       

        transform.position = new Vector3(0, 0, 0);
        
    }

    
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
  
    }

    void CalculateMovement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //Movement
        if (_speedBoost == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * 1.25f * Time.deltaTime);
        }
        

        //Boundaries
        if (transform.position.y >= 4.0f)
        {
            transform.position = new Vector3(transform.position.x, 4.0f, 0);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //Loop in X
        if (transform.position.x > 11.27f)
        {
            transform.position = new Vector3(-11.27f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.27f)
        {
            transform.position = new Vector3(11.27f, transform.position.y, 0);
        }
    }

    void FireLaser() 
    {
        _nextFire = Time.time + _fireRate;
        _audioSource.clip = _laserClip;

        if (_tripleShot == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity);
            _audioSource.Play();
        }
        else
        {
            Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);
            _audioSource.Play();
        }
        
    }

    public void DamageSystem()
    {
        if (_shield)
        {
            _shield = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateImgLives(_lives);

        if (_lives == 2)
        {
            IEnumerator rightEngineDamage()
            {
                yield return new WaitForSeconds(0.2f);
                _rightEngineDamage.SetActive(true);
            }
            StartCoroutine(rightEngineDamage());
        }
        if (_lives == 1) 
        {
            IEnumerator leftEngineDamage()
            {
                yield return new WaitForSeconds(0.2f);
                _leftEngineDamage.SetActive(true);
            }
            StartCoroutine(leftEngineDamage());
        }





        if (_lives < 1)
        {
            _isAlive = false;
            _uiManager.OnGameOver();
            Destroy(gameObject);
        }
    } 

    public bool PlayerIsAlive()
    {
        return _isAlive;
    }

    public void ApplyPowerUp(string powerUp)
    {
        if (powerUp == "TripleShot")
        {
            _tripleShot = true;
            StartCoroutine(PowerUpDownRoutine(powerUp, 5));
        }
        if (powerUp == "SpeedBoost")
        {
            _speedBoost = true;
            StartCoroutine(PowerUpDownRoutine(powerUp, 5));
        }
        if(powerUp == "Shield")
        {
            _shield = true;
            _shieldVisual.SetActive(true);
        }
    }

    IEnumerator PowerUpDownRoutine(string powerUp, float powerUpTimeUP)
    {
        if (powerUp == "TripleShot")
        {
            yield return new WaitForSeconds(powerUpTimeUP);
            _tripleShot = false;         
        }
        if (powerUp == "SpeedBoost")
        {
            yield return new WaitForSeconds(powerUpTimeUP);
            _speedBoost = false;
        }
        if (powerUp == "Shield")
        {
            yield return new WaitForSeconds(powerUpTimeUP);
            _shield = false;
        }
    }

    public void AddScore()
    {
        _score += 10;
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("BestScore", _bestScore);
            _uiManager.UpdateBestScoreText(_bestScore);
        }

        _uiManager.UpdateScoreText(_score);
        

    }

    public int BestScore()
    {
        return _bestScore;
    }

    public int Score()
    {
        return _score;
    }

}

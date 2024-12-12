using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private AudioClip _explotion;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if ( _player == null) 
        {
            Debug.LogError("Player is Null");
        }

        _animator = GetComponent<Animator>();
        if( _animator == null )
        {
            Debug.LogError("Animator is NULL");
        }

        _boxCollider = GetComponent<BoxCollider2D>();
        if( _boxCollider == null)
        {
            Debug.LogError("Box COllider is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if( _audioSource == null)
        {
            Debug.LogError(" Audio Source is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Vector3.down * Time.deltaTime);   

        if (transform.position.y < -5.2f && this.GetComponent<BoxCollider2D>().isActiveAndEnabled)
        {
            transform.position = new Vector3(Random.Range(-9.72f, 9.72f), 7.2f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Laser"))
        {
            _audioSource.clip = _explotion;
            _audioSource.Play();

            Destroy(other.gameObject);

            _player.AddScore();

            this.GetComponent<BoxCollider2D>().enabled = false;
            _animator.SetTrigger("OnEnemyDestroy");
            Destroy(gameObject,2.3f);
        }
        
        
        
        if (other.CompareTag("Player")){

            _audioSource.clip = _explotion;
            _audioSource.Play();

            if (_player != null)
            {
                _player.DamageSystem();
            }

            this.GetComponent<BoxCollider2D>().enabled = false;


            _animator.SetTrigger("OnEnemyDestroy");
            Destroy(gameObject,2.3f);
        }     

    }

}

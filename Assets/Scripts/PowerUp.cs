using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    private AudioSource _audioSource; 
    private CircleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer is NULL");
        }

        _circleCollider = GetComponent<CircleCollider2D>();
        if (_circleCollider == null)
        {
            Debug.LogError("Circle Colider is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _audioSource.Play();
                _circleCollider.enabled = false;
                _spriteRenderer.enabled = false;
                player.ApplyPowerUp(this.tag);
                
            }
            Destroy(this.gameObject, 0.8f);
        }
    }
}

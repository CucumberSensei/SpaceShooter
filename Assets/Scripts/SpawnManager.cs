using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private Player _player;
    [SerializeField]
    private GameObject[] _powerups;
    


    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.Log("The Player is NULL");
        }

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpsRoutine());
    }

   
    IEnumerator SpawnEnemyRoutine()
    {   
        yield return new WaitForSeconds(5.0f);
        while(_player.PlayerIsAlive()) 
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-9.72f, 9.72f), 7.2f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            if (_player.Score() < 800)
            {
                yield return new WaitForSeconds(1.0f);
            }
            else if (_player.Score() < 1600)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
            }
                       
        }
    }

    IEnumerator SpawnPowerUpsRoutine()
    {   
        while(_player.PlayerIsAlive())
        {
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            Vector3 SpawnPosition = new Vector3(Random.Range(-9.72f, 9.72f), 7.2f, 0);
            int randomPowerUp = Random.Range(0, 3);
            if(_player.PlayerIsAlive())
            {
                Instantiate(_powerups[randomPowerUp], SpawnPosition, Quaternion.identity);
            }
            
        }
        
    }









}

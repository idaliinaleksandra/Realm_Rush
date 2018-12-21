using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Range(0.1f, 120f)][SerializeField] float secondsBetweenSpawn = 4f;

    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] GameObject enemyParent;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnEnemySFX;
    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            score++;
            scoreText.text = score.ToString();
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParent.transform;
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    } 
}

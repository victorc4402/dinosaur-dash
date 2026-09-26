using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacle : MonoBehaviour

{
    public float obstaclespeed = 0.00005f;
    public float startpos = 1;
    int moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 currentPosition = transform.position;
        transform.position = new Vector2(startpos,-2.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-obstaclespeed*Time.deltaTime,0);
        if (transform.position[0] < -14)
        {
            transform.position = new Vector2(13,-2.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("deadasf");
        }
    }
}

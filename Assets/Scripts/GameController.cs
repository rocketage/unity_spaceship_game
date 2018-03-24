using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject player1Prefab;
	public GameObject player2Prefab;  
	public Text player1ScoreText;
	public Text player2ScoreText;
	public AudioClip destroySound;
	public float respawnTime = 1.2f;

	private GameObject player1;
	private GameObject player2;
	private int player1Score;
	private int player1Health;
	private int player1GamesWon;
	private bool player1ShouldRespawn;
	private float player1RespawnTime;

	private int player2Score;
	private int player2Health;
	private int player2GamesWon;
	private bool player2ShouldRespawn;
	private float player2RespawnTime;

	private AudioSource audioSource;

	void Awake () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () 
	{
		player1ShouldRespawn = player2ShouldRespawn = true;
		player2RespawnTime = player2RespawnTime = Time.time;
		player1Score = player2Score = 0;
		player1Health = player2Health = 150;
		player1GamesWon = player2GamesWon = 0;
		UpdatePlayer1Stats();
		UpdatePlayer2Stats();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player1ShouldRespawn && (Time.time > player1RespawnTime)) {
			player1ShouldRespawn = false;
			player1 = (GameObject)Instantiate(player1Prefab, new Vector3(-4, 0 , 0), Quaternion.identity);
		}

		if (player2ShouldRespawn && (Time.time > player2RespawnTime)) {
			player2ShouldRespawn = false;
			player2 = (GameObject)Instantiate(player2Prefab, new Vector3(4, 0, 0), Quaternion.identity);
		}
	}

	public void IncreasePlayer1Score(int delta)
	{
		player1Score += delta;
		UpdatePlayer1Stats();
	}

	public void IncreasePlayer2Score(int delta)
	{
		player2Score += delta;
		UpdatePlayer2Stats();
	}

	public void UpdatePlayer1Health(int health)
	{
		player1Health = health;
		UpdatePlayer1Stats();
	}

	public void UpdatePlayer2Health(int health)
	{
		player2Health = health;
		UpdatePlayer2Stats();
	}

	public void PlayerDestroyed(int playerId)
	{
		if (playerId == 1) {
			player1ShouldRespawn = true;
			player1RespawnTime = Time.time + respawnTime;
			player2GamesWon += 1;
			UpdatePlayer2Stats ();			
		} else {
			player2ShouldRespawn = true;
			player2RespawnTime = Time.time + respawnTime;
			player1GamesWon += 1;
			UpdatePlayer1Stats ();
		}

		DestroySound ();
	}

	private void DestroySound()
	{
		audioSource.pitch = Random.Range (0.5f, 1.5f);
		audioSource.PlayOneShot(destroySound, Random.Range (0.8f, 1.0f));
	}

	void UpdatePlayer1Stats()
	{
		player1ScoreText.text = string.Format(
			"P1 Score:{0} Health:{1} Won:{2}", 
			Format(player1Score, 6),
			Format(player1Health, 2),
			player1GamesWon
		);
	}
		
	void UpdatePlayer2Stats()
	{
		player2ScoreText.text = string.Format(
			"P2 Score:{0} Health:{1} Won:{2}", 
			Format(player2Score, 6),
			Format(player2Health, 2),
			player2GamesWon
		);
	}

	string Format(int value, int width)
	{
		int size = (value != 0) ? (width - ((int)Mathf.Log10 ((float)value))) : width;
		string result = "";

		for (int i = 0; i < size; i++) {
			result += "0";
		}

		return result + value.ToString();
	}
}

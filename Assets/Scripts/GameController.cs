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

	private GameObject player1;
	private GameObject player2;
	private int player1Score;
	private int player1Health;
	private int player2Score;
	private int player2Health;

	// Use this for initialization
	void Start () 
	{
		player1 = (GameObject)Instantiate(player1Prefab, new Vector3(-2, 0 , 0), Quaternion.identity);
		player2 = (GameObject)Instantiate(player2Prefab, new Vector3(2, 0, 0), Quaternion.identity);
		player1Score = 0;
		player2Score = 0;
		player1Health = 150;
		player2Health = 150;
		UpdatePlayer1Stats();
		UpdatePlayer2Stats();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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

	void UpdatePlayer1Stats()
	{
		player1ScoreText.text = string.Format(
			"P1 Score:{0} Health:{1}", 
			Format(player1Score, 6),
			Format(player1Health, 2)
		);
	}
		
	void UpdatePlayer2Stats()
	{
		player2ScoreText.text = string.Format(
			"P2 Score:{0} Health:{1}", 
			Format(player2Score, 6),
			Format(player2Health, 2)
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

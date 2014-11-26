﻿using UnityEngine;
using System.Collections;

public class Hud : BaseBehavior
{
	private PlayerController _player;
	private GUIStyle _textStyle = new GUIStyle();
	private GUIStyle _buttonStyle = new GUIStyle();

	private string _playAgainText = "";
	private string _newHighScoreText = "New High Score!";

	public Texture2D PauseButtonImage;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        _textStyle.normal.textColor = Color.black;
        _textStyle.fontSize = 24;

		_playAgainText = IsMobile() ? "Tap to play again" : "Press space to play again";
    }

    void Update()
    {
        if (!_player)
		{
			if (Input.GetKeyDown(KeyCode.Space)|| (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				Application.LoadLevel("level-01");
			}

			return;
		}
    }

    void OnGUI()
	{
		_textStyle.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(10, 10, 100, 20), _player.DistanceTraveled.ToString(), _textStyle);

		if (!_player)
		{
			var cameraPosition = Camera.main.WorldToScreenPoint(Camera.main.transform.position);
			
			_textStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(cameraPosition.x, cameraPosition.y, 20, 20), _playAgainText, _textStyle);

			if (PlayerPrefs.HasKey(Constants.PreviousHighScoreKey) && PlayerPrefs.HasKey(Constants.HighScoreKey))
			{
				var oldScore = PlayerPrefs.GetInt(Constants.PreviousHighScoreKey);
				var score = PlayerPrefs.GetInt(Constants.HighScoreKey);

				if (score > oldScore)
					GUI.Label(new Rect(cameraPosition.x, cameraPosition.y + 20, 20, 20), _newHighScoreText.ToString(), _textStyle);
			}
		}

		_buttonStyle = GUI.skin.label;

		if (GUI.Button(new Rect(Screen.width - 42, 10, 32, 32), PauseButtonImage, _buttonStyle))
		{
			Time.timeScale = Time.timeScale == 1 ? 0 : 1;
		}
    }
}

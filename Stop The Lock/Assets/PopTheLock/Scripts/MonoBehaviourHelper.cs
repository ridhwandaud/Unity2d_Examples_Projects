using UnityEngine;
using System.Collections;

public class MonoBehaviourHelper : MonoBehaviour 
{
	private Player _player;
	public Player player
	{
		get
		{
			if (_player == null)
				_player = FindObjectOfType<Player> ();

			return _player;
		}
	}

	private DotPosition _dotPosition;
	public DotPosition dotPosition
	{
		get
		{
			if (_dotPosition == null)
				_dotPosition = FindObjectOfType<DotPosition> ();

			return _dotPosition;
		}
	}

	private GameManager _gameManager;
	public GameManager gameManager
	{
		get
		{
			if (_gameManager == null)
				_gameManager = FindObjectOfType<GameManager> ();

			return _gameManager;
		}
	}

	private SoundManager _soundManager;
	public SoundManager soundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = FindObjectOfType<SoundManager> ();

			return _soundManager;
		}
	}

	private ColorManager _colorManager;
	public ColorManager colorManager
	{
		get
		{
			if (_colorManager == null)
				_colorManager = FindObjectOfType<ColorManager> ();

			return _colorManager;
		}
	}
}

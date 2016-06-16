using UnityEngine;
using System.Collections;

namespace AppAdvisory.SpringPong
{
	/// <summary>
	/// Attached to the 4 big dots in the game. We just change the name of the dots by convenience. And get a reference to the GameManager.
	/// </summary>
	public class Dot : DotBase
	{
		/// <summary>
		/// The DotPos 
		/// </summary>
		public DotPos dotPos;

		override public void Awake()
		{
			base.Awake();

			name = dotPos.ToString() + "_" + color.ToString();

			GameManager gm = FindObjectOfType<GameManager>();

			if(color == DotColor.Blue)
			{
				GetComponent<SpriteRenderer>().color = gm.colorBlue;
			}
			else
			{
				GetComponent<SpriteRenderer>().color = gm.colorPink;
			}
		}
	}
}
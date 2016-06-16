using UnityEngine;
using System;
using System.Collections;

namespace AppAdvisory.SpringPong
{
	/// <summary>
	/// Class in charge of the player. Attached to the Player GameObject (child of GameManager).
	/// </summary>
	public class Player : DotBase 
	{
		/// <summary>
		/// "Real" random use to select the "poc" sound to display when the player ball bounce on a big dot.
		/// </summary>
		private System.Random rand = new System.Random();
		/// <summary>
		/// Some initializations.
		/// </summary>
		override public void Awake()
		{
			base.Awake();

			sr.color = gm.colorBlue;

			color = DotColor.Blue;
		}
		/// <summary>
		/// Get a random color for the ball (Pink or Blue, please refer to DotColor). Called agter each bounce.
		/// </summary>
		public void RandomColor()
		{
			if(rand.Next(0,2) == 0)
			{
				color = DotColor.Pink;

				sr.color = gm.colorPink;
			}
			else
			{
				color = DotColor.Blue;

				sr.color = gm.colorBlue;
			}
		}
	}
}
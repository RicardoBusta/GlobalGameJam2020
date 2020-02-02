using UnityEngine;

namespace nameSpaceName
{
	[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Scriptable Objects Menu", order = 101)]
	public sealed class GameSettings : ScriptableObject
	{
		public int data = 1; 
	}
}
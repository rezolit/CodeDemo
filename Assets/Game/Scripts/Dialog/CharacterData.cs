using UnityEngine;

namespace Dialog
{
	[CreateAssetMenu(fileName = "CharacterData", menuName = "Dialog/Character", order = 0)]
	public class CharacterData : ScriptableObject
	{
		[SerializeField]
		private Sprite _portrait;

		[SerializeField]
		private string _nameKey;
		
		[SerializeField]
		private Color _color;
		
		public Sprite Portrait => _portrait;
		public string NameKey => _nameKey;
		public Color Color => _color;
	}
}
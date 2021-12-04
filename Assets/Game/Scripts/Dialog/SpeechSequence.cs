using System.Collections.Generic;
using Ineraction;
using Services;
using UnityEngine;
using Zenject;

namespace Interaction
{
	[CreateAssetMenu(fileName = "SpeechTask", menuName = "Dialog/SpeechTask", order = 0)]
	public class SpeechSequence : ScriptableObject
	{
		
		[SerializeField]
		private List<Speech> _speeches;
		
		public List<Speech> Speeches => _speeches;
	}
}
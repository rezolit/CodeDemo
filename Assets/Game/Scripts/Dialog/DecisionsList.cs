using System.Collections.Generic;
using Ineraction;
using Services;
using UnityEngine;
using Zenject;

namespace Dialog
{
	
	
	[CreateAssetMenu(fileName = "DecisionTask", menuName = "Dialog/DecisionTask", order = 0)]
	public class DecisionsList : ScriptableObject
	{
		[SerializeField]
		private List<Decision> _decisions;
		
		public List<Decision> Decisions => _decisions;
	}
}
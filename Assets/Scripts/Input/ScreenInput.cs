using System.Collections.Generic;
using Ballcade.ScreenInputEnteredEvent;
using UnityEngine;

namespace Ballcade
{
	public class ScreenInput : MonoBehaviour
	{
		#region Enums

		[System.Serializable]
		public enum eScreenTouchSide
		{
			None,
			Left,
			Right
		}

		#endregion


		#region Vars

		[SerializeField]
		private ScreenTouchedEvent _screenTouchedEvent;

		private eScreenTouchSide _lastTouchedSide;
		private eScreenTouchSide _cachedScreenTouchSide;


		private List<eScreenTouchSide> _activeTouches = new List<eScreenTouchSide>();

		#endregion


		#region Updates

		private void Update()
		{
			// using a cached version so we can send none once but then it won't keep sending it
			if (_cachedScreenTouchSide == eScreenTouchSide.None && _cachedScreenTouchSide == _lastTouchedSide)
				return;

			SendMovementInput(_lastTouchedSide);
			_cachedScreenTouchSide = _lastTouchedSide;
		}

		#endregion


		#region Events

		public void OnLeftSidePressed()
		{
			_activeTouches.Add(eScreenTouchSide.Left);
			_lastTouchedSide = eScreenTouchSide.Left;
		}

		public void OnRightSidePressed()
		{
			_activeTouches.Add(eScreenTouchSide.Right);
			_lastTouchedSide = eScreenTouchSide.Right;
		}

		public void OnLeftSideReleased()
		{
			OnSideReleased(eScreenTouchSide.Left);

		}
	
		public void OnRightSideReleased()
		{
			OnSideReleased(eScreenTouchSide.Right);
		}

		private void OnSideReleased(eScreenTouchSide sideReleased)
		{
			_activeTouches.Remove(sideReleased);

			if (_activeTouches.Count > 0)
			{
				_lastTouchedSide = _activeTouches[_activeTouches.Count - 1];
			}
			else
			{
				_lastTouchedSide = eScreenTouchSide.None;
			}
		}

		private void SendMovementInput(eScreenTouchSide touchSide)
		{
			_screenTouchedEvent.Raise(new ScreenTouchedData {screenTouchSide = touchSide});
		}

		#endregion
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
	#region Enums

	public enum eScreenTouchSide
	{
		None,
		Left,
		Right
	}

	#endregion

	#region Vars

	[SerializeField] private ScreenTouchedEvent _screenTouchedEvent;

	private eScreenTouchSide _activeScreenTouchSide;

	private eScreenTouchSide _cachedScreenTouchSide;

	#endregion

	#region Updates

	private void Update()
	{
		// using a cached version so we can send none once but then it won't keep sending it
		if (_cachedScreenTouchSide == eScreenTouchSide.None) 
			return;
		
		SendMovementInput(_activeScreenTouchSide);
		_cachedScreenTouchSide = _activeScreenTouchSide;
	}

	#endregion

	#region Events

	public void OnLeftSidePressed()
	{
		_activeScreenTouchSide = eScreenTouchSide.Left;
		_cachedScreenTouchSide = _activeScreenTouchSide;
	}

	public void OnRightSidePressed()
	{
		_activeScreenTouchSide = eScreenTouchSide.Right;
		_cachedScreenTouchSide = _activeScreenTouchSide;
	}

	public void OnSideReleased()
	{
		_activeScreenTouchSide = eScreenTouchSide.None;
	}

	private void SendMovementInput(eScreenTouchSide touchSide)
	{
		_screenTouchedEvent.Raise(new ScreenTouchedData {screenTouchSide = touchSide});
	}

	#endregion
}
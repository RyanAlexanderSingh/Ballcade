using System;
using Ballcade.ScreenInputEnteredEvent;
using UnityEngine;

namespace Ballcade
{
	public class UserPlayerController : PlayerController, IPlayer
	{

#if UNITY_EDITOR
	
		private float _cachedInput;
	
#endif

		#region Updates

		void Update()
		{
			UpdateMovement();
		}

		public void UpdateMovement()
		{
#if UNITY_EDITOR
		
			float h = UnityEngine.Input.GetAxisRaw ("Horizontal");
        
			TryMove(h);
        
#endif
		}

		#endregion


		#region Movement

		private void TryMove(float axis)
		{
			if (!CanMove)
				return;
		
			Vector3 dir = transform.right * axis;
			int dirXNorm = (int) dir.normalized.x;

			SetAnimBasedOnMovementDir(dirXNorm);

			if (dirXNorm == 0)
				return;

			transform.Translate(Time.deltaTime * _playerData.playerSpeed * dir, Space.World);

			transform.position = GetClampedTargetPosition(transform.position);
		}

		#endregion


		#region Listeners

		public void OnScreenTouchedEvent(ScreenTouchedData screenTouchedData)
		{
			float axis;
			switch (screenTouchedData.screenTouchSide)
			{
				case ScreenInput.eScreenTouchSide.None:
					axis = 0f;
					break;
				case ScreenInput.eScreenTouchSide.Left:
					axis = -1f;
					break;
				case ScreenInput.eScreenTouchSide.Right:
					axis = 1f;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			TryMove(axis);
		}

		#endregion
	}
}
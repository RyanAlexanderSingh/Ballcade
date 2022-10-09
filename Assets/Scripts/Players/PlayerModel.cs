using UnityEngine;

namespace Ballcade
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _puckMeshRenderer;
    
        [SerializeField]
        private MeshRenderer _characterMeshRenderer;
    
        [SerializeField]
        protected Animator _animator;

        public Animator Animator => _animator;

        #region Initialise

        public void SetVisualdData(CharacterVisualData visualData)
        {
            _puckMeshRenderer.material = visualData.CharacterMaterial;
            _characterMeshRenderer.material = visualData.PuckMaterial;
        }

        #endregion
    }
}

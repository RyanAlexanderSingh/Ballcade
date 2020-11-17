using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterVisualData", menuName = "Ballcade/Character Visual Data")]
public class CharacterVisualData : ScriptableObject
{
    #region Vars

    [SerializeField]
    private Material _modelMaterial;
    
    [SerializeField]
    private Sprite _characterSprite;

    public Sprite CharacterSprite => _characterSprite;
    
    public Material CharacterMaterial => _modelMaterial;
    
    #endregion


}
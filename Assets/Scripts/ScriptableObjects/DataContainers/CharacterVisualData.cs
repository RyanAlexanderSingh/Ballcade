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
    private Material _puckMaterial;
    
    [SerializeField]
    private Sprite characterPortraitSprite;

    public Sprite CharacterPortraitSprite => characterPortraitSprite;
    
    public Material CharacterMaterial => _modelMaterial;

    public Material PuckMaterial => _puckMaterial;

    #endregion


}
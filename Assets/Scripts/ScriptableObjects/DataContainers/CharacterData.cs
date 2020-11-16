using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Ballcade/Character Data")]
public class CharacterData : ScriptableObject
{
    #region Vars

    [SerializeField] private Material _modelMaterial;

    #endregion

    public Color GetCharacterColour()
    {
        return _modelMaterial.color;
    }

    public Material GetCharacterMaterial()
    {
        return _modelMaterial;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ballcade
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        protected void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}

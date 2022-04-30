using System.Collections;
using System.Collections.Generic;
using UnityEngine;
Using TMPro;

public class Account : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
   
     void Start() {
          textMeshPro.text = "Some text";
     }
}

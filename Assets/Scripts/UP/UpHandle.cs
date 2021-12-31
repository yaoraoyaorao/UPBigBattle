using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpHandle : MonoBehaviour
{
    public Text uname;
    public Text Fans;
    public Animator personAnim;

    public void Init(string name,int fans,RuntimeAnimatorController controller)
    {
        uname.text = name;
        Fans.text = fans.ToString();
        personAnim.runtimeAnimatorController = controller;
    }
}

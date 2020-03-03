using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
    public void closeDialog() {
        this.gameObject.SetActive(false);
    }
}

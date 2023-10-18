using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Initializable: MonoBehaviour
{
    protected bool _inited = false;

    public abstract void Init();
}

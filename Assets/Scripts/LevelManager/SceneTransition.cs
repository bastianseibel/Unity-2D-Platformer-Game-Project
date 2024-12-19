using UnityEngine;
using System.Collections;

public abstract class SceneTransition : MonoBehaviour
{
    [SerializeField] protected float transitionTime = 1f;
    
    public abstract IEnumerator AnimateTransitionIn();
    public abstract IEnumerator AnimateTransitionOut();
}
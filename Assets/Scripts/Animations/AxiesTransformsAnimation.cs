using UnityEngine;
public class AxiesTransformsAnimation : MonoBehaviour
{
    Transform cam;
    Vector3 distance;
    private Vector3 currentScale;
    private bool isEnabled;
    private void OnEnable()
    {
        CalculateCurrentScale();
        LeanTween.scale(gameObject, currentScale, 0.2f).setEaseSpring().setOnComplete(() =>
        {
            isEnabled = true;
            LeanTween.cancel(gameObject);
        });
    }

    private void OnDisable()
    {
        ResetAnimation();
    }
    private void Awake()
    {
        cam = Camera.main.transform;
        ResetAnimation();
    }

    private void ResetAnimation()
    {
        isEnabled = false;
        transform.localScale = Vector3.zero;
    }

    private void CalculateCurrentScale()
    {
        distance = transform.position - cam.position;
        currentScale = Vector3.one* (distance.magnitude/50);
    }
    void Update()
    {
        if (isEnabled)
        {
            CalculateCurrentScale();
            transform.localScale = currentScale;
        }
    }

}

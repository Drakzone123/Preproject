using UnityEngine;

public struct AnimatorVarSmooth
{
    public float value { get => m_value; }
    float target;
    float m_value;
    float velo;
    float smoothTime;
    public void Set(float newTarget, float newSmoothTime = .2f)
    {
        target = newTarget;
        smoothTime = newSmoothTime;
    }
    public void Update()
    {
        m_value = Mathf.SmoothDamp(m_value, target, ref velo, smoothTime);
    }
}
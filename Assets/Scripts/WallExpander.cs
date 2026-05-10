using UnityEngine;

public class WallExpander : MonoBehaviour
{
    [Header("Expansion configuration")]
    [SerializeField] private Vector3 growthDirection = new Vector3(1f, 0f, 0f);
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxGrowth = 10f;

    [Header("Position configuration")]
    [SerializeField] private bool keepBaseFixed = true;

    private float currentGrowth = 0f;

    private void Start()
    {
        NormalizeDirection();
    }

    private void Update()
    {
        if (HasReachedMaxGrowth())
        {
            return;
        }

        ExpandWall();
    }

    private void NormalizeDirection()
    {
        growthDirection = growthDirection.normalized;
    }

    private bool HasReachedMaxGrowth()
    {
        return maxGrowth > 0f && currentGrowth >= maxGrowth;
    }

    private void ExpandWall()
    {
        float increment = speed * Time.deltaTime;
        Vector3 incrementVector = growthDirection * increment;

        IncreaseScale(incrementVector);

        if (keepBaseFixed)
        {
            AdjustPosition(incrementVector);
        }

        currentGrowth += increment;
    }

    private void IncreaseScale(Vector3 incrementVector)
    {
        Vector3 scaleIncrease = new Vector3(
            Mathf.Abs(incrementVector.x),
            Mathf.Abs(incrementVector.y),
            Mathf.Abs(incrementVector.z)
        );
        transform.localScale += scaleIncrease;
    }

    private void AdjustPosition(Vector3 incrementVector)
    {
        transform.Translate(incrementVector / 2f, Space.Self);
    }
}
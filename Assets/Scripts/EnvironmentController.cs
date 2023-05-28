using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public float maxLimboDuration;
    public float limboRechargeSpeed;
    public float growthSpreadFrequency;
    public float growthSpreadMultInlimbo = 2f;
    public GrowthPiece growthPrefab;

    [Tooltip("Object assigned here will be destroyed and replaced with growth!")]
    public List<Transform> growthInitialPositions;

    public bool LimboMode { get; private set; }
    public float CurrentLimboEnergy { get; private set; }

    private List<GrowthPiece> growthList = new List<GrowthPiece>();
    /// <summary>
    /// List of growth that can expand
    /// </summary>
    private List<GrowthPiece> openGrowthList = new List<GrowthPiece>();

    private float growthTimer;

    public void Init()
    {
        for (int i = 0; i < growthInitialPositions.Count; i++)
        {
            InitGrowth(growthInitialPositions[i].position);
            Destroy(growthInitialPositions[i].gameObject);
        }
    }

    private void Update()
    {
        if (!LimboMode)
        {
            CurrentLimboEnergy = Mathf.Clamp(CurrentLimboEnergy + Time.deltaTime * limboRechargeSpeed, 0, maxLimboDuration);

        }
        else
        {
            CurrentLimboEnergy -= Time.deltaTime;
            if(CurrentLimboEnergy <= 0f)
            {
                ToggleLimbo();
            }
        }

        growthTimer += Time.deltaTime * (LimboMode ? growthSpreadMultInlimbo : 1f);

        if(growthTimer > growthSpreadFrequency)
        {
            SpawnGrowthTimed();
            growthTimer = 0;
        }
    }

    private void SpawnGrowthTimed()
    {
        while (openGrowthList.Count > 0)
        {
            GrowthPiece piece = openGrowthList.GetRandom();
            if (piece.TryGetNearbyEmptySlot(out Vector3 newGrowthPos))
            {
                InitGrowth(newGrowthPos);
                break;
            }
            else
            {
                openGrowthList.Remove(piece);
            }
        }

    }

    private void InitGrowth(Vector3 position)
    {
        GrowthPiece growth = Instantiate(growthPrefab, position, Quaternion.identity, transform);
        growthList.Add(growth);
        openGrowthList.Add(growth);

    }


    public bool ToggleLimbo(Tools.BooleanType val = Tools.BooleanType.Toggle)
    {
        bool enabled;
        if(val == Tools.BooleanType.Toggle)
        {
            enabled = !LimboMode;
        }
        else
        {
            enabled = val == Tools.BooleanType.True;
        }

        if (!enabled)
        {
            LimboMode = false;
        }
        else
        {
            if(CurrentLimboEnergy < 0.1f)
            {
                return false;
            }
            else
            {
                LimboMode = true;
            }
        }

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectorManager : MonoBehaviour
{
    public List <PlayerDetector> detectors = new List <PlayerDetector> ();

    private void Awake()
    {
        Global.playerDetectorManager = this;
    }

    public List <PlayerDetector> GetSortedDetectorList()
    {
        return detectors.OrderBy(o => -o.GetTimeSpent()).ToList();
    }
}

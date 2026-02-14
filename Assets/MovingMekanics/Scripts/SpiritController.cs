using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiritController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveInterval = 5f;

    public List<RoomID> movePattern;

    private Dictionary<RoomID, Transform> roomMap;
    private int currentIndex = 0;

    void Start()
    {
        CacheRoomAnchors();
        StartCoroutine(MoveRoutine());
    }

    void CacheRoomAnchors()
    {
        roomMap = new Dictionary<RoomID, Transform>();

        RoomAnchor[] anchors = FindObjectsOfType<RoomAnchor>();
        foreach (var anchor in anchors)
        {
            if (!roomMap.ContainsKey(anchor.roomID))
                roomMap.Add(anchor.roomID, anchor.transform);
        }
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
            MoveToNextRoom();
        }
    }

    void MoveToNextRoom()
    {
        if (movePattern == null || movePattern.Count == 0) return;

        currentIndex = (currentIndex + 1) % movePattern.Count;
        RoomID nextRoom = movePattern[currentIndex];

        if (roomMap.TryGetValue(nextRoom, out Transform target))
        {
            transform.position = target.position;
            Debug.Log($"Spirit pindah ke {nextRoom}");
        }
    }
}

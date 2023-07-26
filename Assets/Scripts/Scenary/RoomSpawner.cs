using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    [SerializeField] private int _openingDirection;


    private RoomTemplates _templates;
    private bool _spawned = false;

    public float _waitTime = 4f;
    
    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Destroy(gameObject, _waitTime);
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!_spawned)
        {
            switch (_openingDirection)
            {
                case 1:
                    SpawnRooms(_templates._bottomRooms[Random.Range(0, _templates._bottomRooms.Length)]);
                    break;
                case 2:
                    SpawnRooms(_templates._topRooms[Random.Range(0, _templates._topRooms.Length)]);
                    break;
                case 3:
                    SpawnRooms(_templates._leftRooms[Random.Range(0, _templates._leftRooms.Length)]);
                    break;
                case 4:
                    SpawnRooms(_templates._rightRooms[Random.Range(0, _templates._rightRooms.Length)]);
                    break;
            }

            _spawned = true;
        }
    }

    private void SpawnRooms(GameObject roomPrefab)
    {
        GameObject newRoom = Instantiate(roomPrefab, transform.position, roomPrefab.transform.rotation);
        newRoom.transform.parent = _templates.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>()._spawned == false && _spawned == false)
            {
                Destroy(gameObject);
            }
            _spawned = true;
        }
    }
}

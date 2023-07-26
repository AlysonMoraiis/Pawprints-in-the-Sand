using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] _bottomRooms;
    public GameObject[] _topRooms;
    public GameObject[] _leftRooms;
    public GameObject[] _rightRooms;

    public List<GameObject> _rooms;
    [SerializeField] private float _waitTime;
    [SerializeField] private GameObject _boss;

    private bool _spawnedBoss;

    private void Update()
    {
        if(_waitTime <= 0 && _spawnedBoss == false)
        {
            for(int i = 0; i < _rooms.Count; i++) 
            {
                if(i == _rooms.Count-1)
                {
                    Instantiate(_boss, _rooms[i].transform.position, Quaternion.identity);
                    _spawnedBoss = true;
                }
            }
        }
        else
        {
            _waitTime -= Time.deltaTime;
        }
    }
}

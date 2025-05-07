using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectListPool = new List<GameObject>();
    private WaitForSeconds _waitTime;
    private GUIControl _GUIControlReference;
    public GameObject _objectSpawnReference;
    
    private float _limitOnX = 5.0f, _limitOnY = 5.0f;
    private int _spawnLimit = 10, _spawnTimeLimitInSeconds = 2;
    public bool isSpawnLoopEnabled {get; private set;} 
    public bool hasPoolBeenInitialized {get; private set;}
    public bool isPoolFull {get; private set;}
    void Start()
    {
        _GUIControlReference = GetComponent<GUIControl>();
        UpdateTextValue();

        isSpawnLoopEnabled = true;
        hasPoolBeenInitialized = false;
        _waitTime = new WaitForSeconds(_spawnTimeLimitInSeconds);
        StartCoroutine(PoolMacro());
    }

    private IEnumerator PoolMacro() {
        while (isSpawnLoopEnabled) {
            if (hasPoolBeenInitialized == false) {
                PoolInitialSetup(_spawnLimit);
            }
            if(isPoolFull == false) {
                PoolNewSpawner();
            }

            foreach(GameObject element in _objectListPool) {
                if (element.activeSelf == false) {
                    element.SetActive(true);
                    UpdateTextValue(1);
                    break;
                }
            }

            PoolCheckForTotalObjects();

            yield return _waitTime;
        }
    }

    private void PoolInitialSetup(int poolSize) {
        for (int i = 0; i < poolSize; i++) {
            Vector3 randomPosition = GenerateRandomPosition();
            GameObject prefabObject;
            prefabObject = Instantiate(_objectSpawnReference, randomPosition, _objectSpawnReference.transform.rotation);
            _objectListPool.Add(NewObjectSetup(prefabObject));
        }

        hasPoolBeenInitialized = true;
        isPoolFull = true;
    }

    private void PoolCheckForTotalObjects() {
        if (_objectListPool.Count < _spawnLimit) {
            isPoolFull = false;
        } else {
            isPoolFull = true;
        }
    }
    private void PoolNewSpawner() {
        int localCurrentNumberOfObjects = _objectListPool.Count;

        for (int i = 0; i < (_spawnLimit - localCurrentNumberOfObjects); i++) {
            Vector3 randomPosition = GenerateRandomPosition();
            GameObject prefabObject;
            prefabObject = Instantiate(_objectSpawnReference, randomPosition, _objectSpawnReference.transform.rotation);
            _objectListPool.Add(NewObjectSetup(prefabObject));
        }
    }
    private GameObject NewObjectSetup(GameObject newObject) {
        Color newRandomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

        newObject.SetActive(false);
        newObject.GetComponent<Renderer>().material.SetColor("_BaseColor", newRandomColor);
        newObject.GetComponent<ObjectScript>().InitialSetup(this);
        return newObject;
    }
    private Vector3 GenerateRandomPosition() {
        float spawnPositionOnX = Random.Range(-1.0f, 1.0f) * _limitOnX;
        float spawnPositionOnY = Random.Range(-1.0f, 1.0f) * _limitOnY;
        Vector3 randomPosition = new Vector3(spawnPositionOnX, spawnPositionOnY);
        return randomPosition;
    }
    public void RemoveReferenceFromObjectPoolList(GameObject targetObject) {
        _objectListPool.RemoveAt(_objectListPool.IndexOf(targetObject));
    }
    public void UpdateTextValue() {
        _GUIControlReference.CountTextUpdate();
    }
    public void UpdateTextValue(int value) {
        _GUIControlReference.CountTextUpdate(value);
    }
}

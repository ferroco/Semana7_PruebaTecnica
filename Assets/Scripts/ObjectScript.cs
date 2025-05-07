using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    ObjectGeneration _objectGeneratorReference;
    public void InitialSetup(ObjectGeneration objectGenerationRef) {
        _objectGeneratorReference = objectGenerationRef;
    }
    void OnMouseDown()
    {
        if (this.isActiveAndEnabled == true) {
            _objectGeneratorReference.UpdateTextValue(-1);
        }
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        _objectGeneratorReference.RemoveReferenceFromObjectPoolList(gameObject);
    }
}

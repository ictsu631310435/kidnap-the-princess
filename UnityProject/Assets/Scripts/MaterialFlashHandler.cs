using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling Material Flash
public class MaterialFlashHandler : MonoBehaviour
{
    #region Data Members
    private MeshRenderer _meshRenderer;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private Material _originalMaterial;
    [Tooltip("Material that will flash")]
    public Material flashMaterial;
    [Tooltip("Duration that material will flash")]
    public float flashDuration;
    private Coroutine _flashCoroutine;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            // Get Compenent
            _meshRenderer = meshRenderer;
            // Get original Material
            _originalMaterial = _meshRenderer.material;
        }
        else if (TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
        {
            // Get Compenent
            _skinnedMeshRenderer = skinnedMeshRenderer;
            // Get original Material
            _originalMaterial = _skinnedMeshRenderer.material;
        }

        // Get Compenent
        //_meshRenderer = GetComponent<MeshRenderer>();
        // Get original Material
        //_originalMaterial = _meshRenderer.material;
    }
    #endregion

    #region Methods
    public void Flash()
    {
        // If there is a _flashCoroutine running, stop it
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }
        // Start a new _flashCoruotine
        _flashCoroutine = StartCoroutine("FlashCoroutine");
    }

    private IEnumerator FlashCoroutine()
    {
        // Change original Material to flashMaterial
        if (_meshRenderer != null)
        {
            _meshRenderer.material = flashMaterial;
        }
        else if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.material = flashMaterial;
        }
        
        // Wait for flashDuration
        yield return new WaitForSeconds(flashDuration);

        // Change back to original Material
        if (_meshRenderer != null)
        {
            _meshRenderer.material = _originalMaterial;
        }
        else if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.material = _originalMaterial;
        }
        
        // Reset _flashCoroutine after finished
        _flashCoroutine = null;
    }
    #endregion
}

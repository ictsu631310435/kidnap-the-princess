using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling Material Flash
public class MaterialFlashHandler : MonoBehaviour
{
    #region Data Members
    private MeshRenderer _meshRenderer;

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
        // Get Compenent
        _meshRenderer = GetComponent<MeshRenderer>();
        // Get original Material
        _originalMaterial = _meshRenderer.material;
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
        _meshRenderer.material = flashMaterial;
        // Wait for flashDuration
        yield return new WaitForSeconds(flashDuration);
        // Change back to original Material
        _meshRenderer.material = _originalMaterial;
        // Reset _flashCoroutine after finished
        _flashCoroutine = null;
    }
    #endregion
}

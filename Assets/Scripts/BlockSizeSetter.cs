using UnityEngine;

public class BlockSizeSetter : IBlockSizeSetter
{
    private Vector3 _blockTemplateSize;

    public void SetBlockTemplateSize(Vector3 newTemplateSize)
    {
        _blockTemplateSize = newTemplateSize;
    }
    
    public void ApplySize(Transform blockTransform)
    {
        blockTransform.localScale = _blockTemplateSize;
    }
    
    public bool ShouldApplySize()
    {
        return _blockTemplateSize != default;
    }
}
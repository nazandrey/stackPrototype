using UnityEngine;

public interface IBlockSizeSetter
{
    void SetBlockTemplateSize(Vector3 newTemplateSize);
    void ApplySize(Transform blockTransform);
    bool ShouldApplySize();
}
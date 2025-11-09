using TMPro;
using UnityEngine;

public class DamadgePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    private void Setup(int dmgAmount)
    {
        textMesh.SetText(dmgAmount.ToString());
    }
    public static DamadgePopup Create(Vector3 position, int dmgAmount)
    {
        Transform dmgPopupTransform = Instantiate(AssetStorage.pfDamagePopup, position, Quaternion.identity);
        DamadgePopup damadgePopup = dmgPopupTransform.GetComponent<DamadgePopup>();
        damadgePopup.Setup(300);

        return damadgePopup;
    }
}

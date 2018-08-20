using UnityEngine;

public class ControllerAlignment : MonoBehaviour
{
    public AlignmentManager alignmentManager;

    public SteamVR_TrackedController controller;

    void Start()
    {
        if (!controller)
        {
            Debug.LogError("Controller must have SteamVR_TrackedController behavior.");
            return;
        }

        controller.TriggerClicked += Controller_TriggerClicked;
    }

    private void Controller_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (alignmentManager.CurrentlyAligning)
        {
            alignmentManager.ControllerClicked(this.transform);
        }
    }
}

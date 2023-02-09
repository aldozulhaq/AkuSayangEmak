using UnityEngine;
using Cinemachine;
 
namespace Ihaten
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's X co-ordinate
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class CustomCinemachineExtension : CinemachineExtension
    {
        [Tooltip("Lock the camera's Z position to this value")]
        public float lockZPos = 0;
    
        protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.z = lockZPos;
            state.RawPosition = pos;
        }
    }
    }
}

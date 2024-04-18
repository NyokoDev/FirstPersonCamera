﻿using FirstPersonCamera.MonoBehaviours;
using Game;
using Game.Common;
using Game.Rendering;
using Game.Tools;
using Unity.Entities;
using UnityEngine;

namespace FirstPersonCamera.Systems
{
    /// <summary>
    /// The core system controlling the camera and setup
    /// </summary>
    public partial class FirstPersonCameraSystem : GameSystemBase
    {
        private FirstPersonCameraController Controller
        {
            get;
            set;
        }

        private bool IsRaycastingOverridden
        {
            get;
            set;
        }

        private RenderingSystem _renderingSystem;
        private ToolRaycastSystem _toolRaycastSystem;
        private ToolSystem _toolSystem;

        protected override void OnCreate( )
        {
            base.OnCreate( );

            UnityEngine.Debug.Log( "FirstPersonCamera loaded!" );

            _renderingSystem = World.GetExistingSystemManaged<RenderingSystem>( );
            _toolSystem = World.GetExistingSystemManaged<ToolSystem>( );
            _toolRaycastSystem = World.GetExistingSystemManaged<ToolRaycastSystem>( );

            CreateOrGetController( );
        }

        /// <summary>
        /// Not used
        /// </summary>
        protected override void OnUpdate( )
        {
        }

        /// <summary>
        /// Update the controller
        /// </summary>
        public void UpdateCamera()
        {
            Controller.UpdateCamera( );
        }

        /// <summary>
        /// Create the c ontroller if needed
        /// </summary>
        private void CreateOrGetController()
        {
            var existingObj = GameObject.Find( nameof( FirstPersonCameraController ) );

            if ( existingObj != null )
                Controller = existingObj.GetComponent<FirstPersonCameraController>();
            else
                Controller = new GameObject( nameof( FirstPersonCameraController ) ).AddComponent<FirstPersonCameraController>();
        }

        /// <summary>
        /// Toggle the UI on or off and restore raycasting if necesssary
        /// </summary>
        /// <param name="hidden"></param>
        public void ToggleUI( bool hidden )
        {
            _renderingSystem.hideOverlay = hidden;
            //Colossal.UI.UIManager.defaultUISystem.enabled = !hidden;

            if ( hidden )
            {
                _toolRaycastSystem.raycastFlags |= RaycastFlags.FreeCameraDisable;
                _toolSystem.activeTool = World.GetExistingSystemManaged<DefaultToolSystem>( );
            }
            else
                _toolRaycastSystem.raycastFlags &= ~RaycastFlags.FreeCameraDisable;
        }

        /// <summary>
        /// Turn raycasting on or off
        /// </summary>
        /// <param name="isEnabled"></param>
        public void ToggleRaycasting( bool isEnabled )
        {
            IsRaycastingOverridden = !isEnabled;

            if ( isEnabled )
                _toolRaycastSystem.raycastFlags &= ~RaycastFlags.FreeCameraDisable;
            else
            _toolRaycastSystem.raycastFlags |= RaycastFlags.FreeCameraDisable;
        }

        protected override void OnDestroy( )
        {
            base.OnDestroy( );

            if ( Controller != null )
            {
                GameObject.Destroy( Controller.gameObject );
                Controller = null;
            }
        }
    }
}

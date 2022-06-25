﻿using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.General
{
    [RequireComponent(typeof(Camera))]
    public class CameraView : LinkableView
    {
        public Vector2 DefaultResolution => _defaultResolution;
        [SerializeField] private Vector2 _defaultResolution = new Vector2(1080, 1920);
        public float WidthOrHeight => _widthOrHeight;
        [Range(0f, 1f)] [SerializeField] private float _widthOrHeight = 0;
        
        public float InitialSize => _initialSize;
        [SerializeField] private float _initialSize = 0;

        private Camera _camera;
        private float _targetAspect;
        public ref float GetTargetAspect() => ref _targetAspect;
        private float _horizontalFov = 120f;
        public ref float GetHorizontalFov() => ref _horizontalFov;


        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            _camera = GetComponent<Camera>();
            _targetAspect = DefaultResolution.x / DefaultResolution.y;
            _horizontalFov = CalcVerticalFov(_camera.fieldOfView, 1 / _targetAspect ); 
            _initialSize = _camera.orthographicSize;
        }

        public ref Camera GetCamera()
        {
            return ref _camera;
        }
        
        public float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            return 2 * Mathf.Atan(Mathf.Tan(hFovInDeg * Mathf.Deg2Rad / 2) / aspectRatio) * Mathf.Rad2Deg;
        }
    }
}
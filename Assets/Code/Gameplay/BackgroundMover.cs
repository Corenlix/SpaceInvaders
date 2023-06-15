using System;
using Railcar.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _cellsCount;
        [SerializeField] private Renderer _renderer;
        private IDisposable _subscribe;
        private float _height;
        private float _initialY;

        [Inject]
        private void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time)
        {
            _subscribe = time.Observe(OnUpdated);
            _initialY = transform.position.y;
        }

        private void Awake()
        {
            _height = _renderer.bounds.size.y;
            for (int i = -_cellsCount; i <= _cellsCount; i++)
            {
                Instantiate(_renderer, transform.position + Vector3.up * (i * _height), _renderer.transform.rotation, transform);
            }
        }

        private void OnUpdated(float deltaTime)
        {
            transform.Translate(Vector3.down * (deltaTime * _speed));
            while (_initialY - transform.position.y > _height)
            {
                transform.Translate(Vector3.up * _height);
            }
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
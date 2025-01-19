using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class TrakectoryEnemy : MonoBehaviour
{
  [SerializeField] private List<Transform> _movePoints;
  [SerializeField] private Transform _model;
  [SerializeField] private float _moveDuration;
  [SerializeField] private PathType _pathType;

  private Tween _tween;

  private int _damage;

  public void Init(int damage)
  {
    _damage = damage;

    _tween.Play();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.transform.TryGetComponent(out PlayerModel playerModel))
    {
      playerModel.TakeDamage(_damage);
      gameObject.SetActive(false);
    }
  }

  private void Awake()
  {
    Vector3[] path = new Vector3[_movePoints.Count];
    for (int i = 0; i < _movePoints.Count; i++)
      path[i] = _movePoints[i].position;

    _tween = _model.DOPath(path, _moveDuration, _pathType)
      .SetEase(Ease.Linear)
      .SetLoops(-1, LoopType.Restart)
      .SetOptions(true)
      .SetLink(gameObject);
  }

  [Button]
  public void StartMove()
  {
    _tween.Play();
  }

  private void OnDrawGizmos()
  {
    if (_movePoints == null || _movePoints.Count < 2) return;

    Gizmos.color = Color.red;

    for (int i = 0; i < _movePoints.Count; i++)
    {
      Gizmos.DrawSphere(_movePoints[i].position, 0.1f);

      if (i < _movePoints.Count - 1)
        Gizmos.DrawLine(_movePoints[i].position, _movePoints[i + 1].position);
      else
        Gizmos.DrawLine(_movePoints[i].position, _movePoints[0].position); // Замыкаем путь
    }
  }

  public void Pause()
  {
    _tween.Pause();
  }

  public void Resume()
  {
    _tween.Play();
  }
}
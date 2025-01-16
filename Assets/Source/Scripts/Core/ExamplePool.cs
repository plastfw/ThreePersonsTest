// using UnityEngine;
//
// public class HeadPool : MonoBehaviour
// {
//   [SerializeField] private Head _headPrefab;
//   [SerializeField] private int _initialPoolSize = 5;
//
//   private ObjectPool<Head> _headPool;
//
//   private void Start()
//   {
//     _headPool = new ObjectPool<Head>(
//       () =>
//       {
//         var head = Instantiate(_headPrefab);
//         head.Init(this);
//         return head;
//       },
//       head => { },
//       head => { head.Reset(); },
//       _initialPoolSize);
//   }
//
//   public Head GetHead() => _headPool.Get();
//
//   public void ReturnHead(Head head) => _headPool.Return(head);
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;
        protected void Start()
        {
            _root = SetupTree();
            RestartTree();
        }
       /* private void Update()
        {
            if (_root!=null)
            {
                _root.Evaluate();
            }
        }*/
        public void RestartTree()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
            else
            {
                _root = SetupTree();
            }
        }
        public void StopTree()
        {
            _root = null;
        }
        protected abstract Node SetupTree();
    }
}

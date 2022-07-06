using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFighter
{

    public enum AiState { Patroling, Searching, Attacking, Turning }

    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f)] protected float moveSpeed = 5f;
        [SerializeField, Range(5f, 100f)] protected float tankRotSec = 50f;
        protected Vector2 moveDir;

        protected Vector2 rotate;
        [SerializeField, Range(1f, 360f)] protected float degreesPerSec = 50f;
        [SerializeField] protected GameObject _tankHead;
        protected Transform _tankHeadTransform;   

        protected Animator _anim;
        protected int movingHash;

        protected virtual void Start()
        {
            _anim = GetComponent<Animator>();
            _tankHeadTransform = _tankHead.transform;
            movingHash = Animator.StringToHash("moving");
        }

        protected virtual void Update()
        {
            MovementAnim();
            GetInputs();
        }

        protected virtual void FixedUpdate()
        {
            Move();
            Rotate();
            RotHead();
        }

        private void MovementAnim()
        {
            if (moveDir.x != 0f || moveDir.y != 0f)
            {
                _anim.SetBool(movingHash, true);
            }
            else
            {
                _anim.SetBool(movingHash, false);
            }
        }

        protected virtual void GetInputs()
        {

        }


        /// <summary>
        /// changing player movement to just forward and backward
        /// for player to move left an right, player need to rotate
        /// then continue in thier facing direction
        /// </summary>
        private void Rotate()
        {
            transform.Rotate(0f,0f, moveDir.x * -tankRotSec * Time.deltaTime);
        }

        private void Move()
        {
            transform.Translate(0f, moveDir.y * moveSpeed * Time.deltaTime, 0f);
        }


        /// <summary>
        /// At the moment for RotHead function 
        /// only using the rotation on xaxis looking from the top view 
        /// which is the rotation in the global world
        /// </summary>
        private void RotHead()
        {
            _tankHeadTransform.Rotate(0f,0f, rotate.x  * -degreesPerSec * Time.deltaTime);
        }
    }

}

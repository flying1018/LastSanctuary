using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MoveObject
{
   [SerializeField] private Sprite[] sprite;
   private Sprite _originSprite;
   private PuzzleManager _puzzleManager;
   private SpriteRenderer _spriteRenderer;
   private LeverObject _leverObject;
   private int _curSpriteIndex = 0;
   private void Awake()
   {
      _puzzleManager = GetComponentInParent<PuzzleManager>();
      _leverObject = GetComponentInParent<LeverObject>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _originSprite = _spriteRenderer.sprite;
   }

   public override void MoveObj()
   {
      OnAction();
   }

   public override void ReturnObj()
   {
      
      OnReturn();
   }

   public void OnAction()
   {
      if (_puzzleManager != null)
         _puzzleManager.OnCheckCorrect(gameObject);

      if (sprite != null)
      {
         _spriteRenderer.sprite = sprite[_curSpriteIndex];
         _curSpriteIndex++;
      }
   }

   public void OnReturn()
   {
      if (_puzzleManager != null)
         _puzzleManager.OnRemoveCorrect(gameObject);
      _spriteRenderer.sprite = _originSprite;
      _curSpriteIndex = 0;
   }

   public void Reset()
   {
      _leverObject.ReturnLever();
      _spriteRenderer.sprite = _originSprite;
      _curSpriteIndex = 0;
   }
}

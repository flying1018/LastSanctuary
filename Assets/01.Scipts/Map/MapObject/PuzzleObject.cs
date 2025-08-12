using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MoveObject
{
   [SerializeField] private Sprite[] sprite;
   private Sprite _originSprite;
   private PuzzleManager _sequencePuzzleManager;
   private SpriteRenderer _spriteRenderer;
   private LeverObject _leverObject;
   
   private int _curSpriteIndex = 0;
   private void Awake()
   {
      _sequencePuzzleManager = GetComponentInParent<PuzzleManager>();
      _leverObject = GetComponentInParent<LeverObject>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _originSprite = _spriteRenderer.sprite;
   }

   public override void MoveObj()
   {
      OnAction();
   }

   public void OnAction()
   {
      if (_sequencePuzzleManager != null)
      _sequencePuzzleManager.OnCheckCorrect(gameObject);

      if (sprite != null)
      {
         _spriteRenderer.sprite = sprite[_curSpriteIndex];
         _curSpriteIndex++;
      }
   }

   public void Reset()
   {
      _leverObject.ReturnLever();
      _spriteRenderer.sprite = _originSprite;
      _curSpriteIndex = 0;
   }
}

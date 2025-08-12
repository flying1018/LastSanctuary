
<img width="380" height="270" alt="Sprite-0001" src="https://github.com/user-attachments/assets/eab0a3cb-baf7-4caf-b703-8055b9fb71c0" />

### 게임 소개: **Last Sanctuary - 잿더미 위의 맹세**

**장르**: 2D 액션 플랫포머 / 메트로배니아 / 소울라이크

**플랫폼**: PC

**플레이 시간**: 약 30~60분 (데모 기준)

---

### 프로젝트 개요 및 목표

- **정밀한 전투**: 회피, 가드, 경직 유도 등 *소울라이크 전투의 긴장감*을 2D로 구현
- **보스전 중심 설계**: *패턴 기반의 보스*, 페이즈 전환, 약점 노출 등으로 구성된 핵심 전투
- **성역 복구 시스템**: 진행에 따라 파괴된 성역을 하나씩 복원하며 새로운 기능과 NPC 해금

---

### 기술적인 도전 과제

**이준영 :** 

<details>
    <summary> 플레이어, 적, 보스 리지드 바디 타입을 Dynamic → Kinematic으로 변경 </summary>
    
    - 중간 발표 후 피드백 중에서 Dynamic을 사용할 필요가 없다는 피드백을 받았음.
    - Dynamic은 기본적으로 차지하는 리소스가 많아 무거움.(자동 중력 처리, 충돌 처리 등)
    - 나중에 멀티 환경을 구현하려면 Dynamic은 사용하기 힘듦.
    - 정확한 움직임 예측 및 제어, 설계 최적화, 안전성 및 신뢰성 향상, 효율적인 시뮬레이션 및 비용 절감 등의 장점이 많고 공부가 됨.
    
</details>

<details>
    
 <summary> 인터페이스 방식 FSM 구조 사용  </summary>
 
    - 심화 강의에서 배운 인터페이스 방식 FSM을 사용.
    - 배운 내용을 바탕으로 직접 사용해보면서 장점과 단점을 체득.
    - 장점을 배웠던 점 : 오류 발생 시 애니메이터와 현재 상태를 Debug해보며 오류 원인 파악이 용의했음.
    - 단점을 느낀 점 : 현재 실수로 확장이 어렵게 구조를 작성하였는데, 굉장히 많은 코드를 고쳐야 됨. → 시간 부족으로 인해서 현재는 방치중.

</details>

<details>
    
 <summary> 델리게이트 사용 </summary>
 
    - 델리게이트를 이용하여 협업성을 강화 하려 했으나 문제점만 발생.
    - 이벤트 등록, 해제를 제대로 하지 않으면 오히려 오류 발생.
    - 델리게이트를 만든 것을 팀원에게 제대로 알리지 않으면 결국 활용되지 못함.

</details>

**안상진 :**
<details>
 <summary> 유니티 이벤트(UnityEvent) 사용 </summary>
    
    - 기존에는 델리게이트만 사용했으나, 유니티 인스펙터에서 연결 가능하고 직관적인 UnityEvent를 사용해봄.
    - 장점: 인스펙터에서 간단하게 이벤트 리스너를 추가/제거할 수 있어, 런타임 중에도 빠르게 UI나 오브젝트 반응을 변경 가능.
    - 배운 점:  스크립트 수정 없이 기획자가 직접 이벤트를 연결할 수 있어 작업 효율 향상.
    - 단점: 델리게이트보다 성능이 조금 떨어지고, 코드 상에서 추적이 어려운 경우가 있어 디버깅이 복잡함.

</details>
    

**김재경** : 
<details>
 <summary> 시네머신을 사용한 카메라 연출</summary>
 
    - 카메라를 이용한 연출인 줌 인, 줌 아웃, 카메라 흔들림 등의 기능이 필요함.
    - 시네머신을 사용하여 빠르고 비교적 간단한 코드로 해결.
    - 손쉽게 기능을 연출한 건 좋았지만, 나중에 더 복잡한 카메라 기능이 필요할 땐 결국 코드 공부가 필수.

</details>

---

### 사용된 기술 스택
- Visual Studio - 2022
- Rider - 2025.1.2
- Unity - 2022.3.17f1
- GitHub
- FigJam : https://www.figma.com/board/3hqwVR3FJmog7jrVYkMP4O/Last-Sanctuary?node-id=0-1&t=zbtd7tMUzlFc0op8-1
- Notion : [11조 - ATLANTIS Studio](https://www.notion.so/11-ATLANTIS-Studio-2182dc3ef5148017bad6fd9b4426f148?pvs=21)

<details>
<summary> Kinematic을 이용해서 액션 게임 구현</summary>
    
    - **장점** :
        - 정확한 움직임 예측 및 제어
        - 설계 최적화
        - 안전성 및 신뢰성 향상
        - 효율적인 시뮬레이션 및 비용 절감
    - **단점** :
        - 역학적 요소의 한계
        - 예측 정확도의 감소
        - 특이점(Singularity) 문제
        - 제약 조건 처리의 한계
    - **프로젝트에서 활용** :
        - 현재 플레이어, 몬스터, 보스 등 Kinematic으로 움직임을 제어 중.
        - 플레이어 동작이 코드에 작성한 대로 정확한 움직임을 보여서 좋았음.
        - 현재 프로젝트에서 씬에 필요한 모든 몬스터를 한꺼번에 생성 하는데, Dynamic에 비해서 확실히 가볍게 실행되는게 느껴짐.
        
</details>

<details>
    
<summary>Cinemachine을 이용해서 다양한 연출 구현</summary>

    - **장점** :
        - 코드 없는(Codeless) 카메라 제어
        - 다양한 카메라 기능 제공
        - 성능 최적화
        - 비파괴적(Non-destructive) 작업
    - **단점** :
        - 학습 곡선(Learning Curve)
        - 커스텀 로직의 한계
        - 특정 장르에 부적합
        - 성능 오버헤드
    - **프로젝트에서 활용** :
        - 흔들림, 전환, 피사체 추적, 화면 내 피사체 크기 자동 조절 등의 기능을 구현하고 연출에 사용 중.

</details>

<details>

<summary>인터페이스 방식 FSM 구조를 활용한 다양한 기능 구현(플레이어, 적, 보스, UI)</summary>

    - **장점** :
        - 높은 유연성과 확장성
        - 낮은 결합도(Low Coupling)
        - 코드의 재사용성 및 가독성
    - **단점** :
        - 복잡한 초기 설정
        - 과도한 객체 생성
        - 상태 간의 정보 공유 어려움
        - 전이(Transition) 로직의 분산
    - **프로젝트에서 활용** :
        - 플레이어 : 키 입력에 따른 상태 변환을 구현.
        - 적 : 외부 변화 및 시간 측정에 따른 상태 변환을 구현.
        - 보스 : 공격 위주의 상태 변환을 구현.
        - UI : 키 입력에 따른 상태 변환을 구현. 추후 애니메이션 연출 작업에 용의함.

</details>

<details>

<summary>어드레서블을 이용한 에셋 관리</summary>

    - **장점** :
        - 쉬운 사용법과 직관적인 관리
        - 동적 로딩 및 언로딩
        - 유연한 배포 옵션
        - 자동 의존성 관리
    - **단점** :
        - 초기 학습 곡선
        - 초기 빌드 시간 증가
        - 복잡한 환경에서의 관리
        - 오버헤드 발생 가능성
        - 버전 관리 문제
    - **프로젝트에서 활용** :
        - 현재는 용량이 큰 BGM 파일들을 게임 시작 시에 가져옴.
        - FSM구조에 사용되는 데이터를 보관한 ScriptableObject 파일들을 가져와도 될 듯함.
        - 초기에 만들어서 쉽게 활용 가능했으나, 기능 구현 중에는 직렬화로 먼저 구현하였기 때문에 나중에 신경써서 적용해주지 않으면 활용이 어려움.
        - 오류 발생 확률이 높은 String 키 방식이라 지양하게 되었음.
        
</details>

<details>
    
<summary>오브젝트 풀링을 활용한 메모리 최적화</summary>

    - **장점** :
        - 성능 최적화
        - 메모리 관리 용이
        - 예측 가능한 동작
    - **단점** :
        - 초기 메모리 사용량 증가
        - 복잡한 관리
        - 관리 로직의 복잡성
        - 범용성 부족
    - **프로젝트에서 활용** :
        - 몬스터, 투사체 등의 오브젝트를 폴링을 이용해서 관리중.
        - 몬스터, 투사체의 생성, 파괴  시에 자잘한 렉은 보이지 않음.
        - 가끔 키 값을 잘못 관리하면 엉뚱한 오브젝트가 생성됨.

</details>

<details>
        
<summary>UnityEvent를 활용한 기능 직렬화</summary>

    - **장점** :
        - 낮은 결합도
        - 직관적인 UI 기반 설정
        - 다양한 타입 지원
        - 확장성
    - **단점** :
        - 성능 오버헤드
        - 제약적인 매개변수
        - 디버깅의 어려움
        - 직렬화 문제
    - **프로젝트에서 활용** :
        - 델리게이트의 일종이므로 팀원이 작성한 코드에 기능을 추가하기가 용의 하였음.
        - 하지만 처음 UnityEvent를 접해서 기능 연결이 코드로 안되어 있어서 코드로 기능 해석이 힘들었음.

</details>

---

### 클라이언트 구조

- **게임 흐름도**
    
    <img width="654" height="339" alt="image" src="https://github.com/user-attachments/assets/37fb9460-6323-43cd-a866-013078f3dead" />

    
- **Kinematic 이동 관련 구조**
    
  <img width="548" height="228" alt="image" src="https://github.com/user-attachments/assets/e8e0facc-c2be-4cad-8504-fb0d40bb8757" />

    
- **플레이어 상태 머신**
    
    <img width="705" height="308" alt="image" src="https://github.com/user-attachments/assets/c6901fdf-1a9b-406c-9ca8-d2df1ef52faa" />

    
- **적 상태 머신**
    
 <img width="726" height="324" alt="image" src="https://github.com/user-attachments/assets/7850da3d-2b8c-4c7b-97d4-2e0bff78cfff" />

    
- **보스 상태 머신**
    
   <img width="804" height="317" alt="image" src="https://github.com/user-attachments/assets/1d694592-6016-4cfc-8973-afb6efe91053" />

    - 아쉽게도 시간부족으로 인해 이상적인 형태로는 구현을 못함.
    - 원하는 형태 :
        - Boss를 상속 받는 Boss01, Boss02
        - BossCondition을 상속 받는 Boss01Condition, Boss02Condition
        - BossStateMachine을 상속 받는 Boss01StateMachine, Boss02StateMachine
     
          
- **UI 상태 머신**
  
    <img width="664" height="404" alt="image" src="https://github.com/user-attachments/assets/c1809d66-ec70-4836-9130-fc77e50409e9" />
    
    - UI는 수많은 직렬화 데이터가 필요(Button, Text 등)
    - 이 많은 직렬화 데이터를 UIManager에 할당하기에는 문제가 있어보임.
    - 그래서 BaseState에 MonoBehavior를 상속하여 해당 상태에서 필요한 직렬화 데이터는 직접 넣도록 변경
    - 다만 이 과정에서 UnifiedUI에 사용되는 직렬화 데이터는 RelicUI, SkillUI, SettingUI에 다 3번씩 할당 해줘야 하는 형태가 되었음. 개선필요.
 
      
- **무기 구조**
    
   <img width="540" height="309" alt="image" src="https://github.com/user-attachments/assets/360bd0ce-62d1-431a-93da-8bad482bb4f6" />

    - 뒤늦게 확인해보니 BossWeapon과 EnemyWeapon의 코드가 같음.
    - 아마도 초기에는 매개변수를 다양하게 전달하여서 분리하였으나, 지금은 WeaponInfo에 넣어서 보내서 생긴 문제인듯 함.
 
      
- **피해 입는 구조**
    
   <img width="614" height="263" alt="image" src="https://github.com/user-attachments/assets/582d3f7e-c49a-4138-ae92-ec46506fa9ae" />
    
    - 모든 컨디션은 컨디션을 상속받고 거기에 필요한 인터페이스를 상속 받는 방식
 

- **아이템 구조**
    
   <img width="554" height="287" alt="image" src="https://github.com/user-attachments/assets/59c917b8-152a-4df6-8ddd-48da7e4c08aa" />
    
- **맵 오브젝트 구조**
    
    <img width="548" height="405" alt="image" src="https://github.com/user-attachments/assets/61b6b4cc-26cd-4061-84d2-fd12948cc50f" />


### 코드 샘플 및 주석

| 팀원 | 담당 기능 |
| --- | --- |
| 이준영, 안상진 |    <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/FSM%EA%B5%AC%EC%A1%B0%EB%A5%BC%20%ED%99%9C%EC%9A%A9%ED%95%9C%20Player%2CEnemy%2CBoss%2CUI"> **FSM구조를 활용한 Player,Enemy,Boss,UI**</A> |
| 이준영, 안상진 |<A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/Kinematic%EC%9D%84%20%EC%9D%B4%EC%9A%A9%ED%95%9C%20%EC%A7%80%ED%98%95%20%EC%B6%A9%EB%8F%8C%EC%9D%98%20%EC%9D%B4%EB%8F%99%20%EA%B8%B0%EB%8A%A5"> **Kinematic을 이용한 지형 충돌의 이동 기능** </A>| 
| 이준영 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/ScripableObject%EB%A5%BC%20%ED%99%9C%EC%9A%A9%ED%95%9C%20%EB%8D%B0%EC%9D%B4%ED%84%B0%20%EA%B4%80%EB%A6%AC"> **ScripableObject를 활용한 데이터 관리** </A> | 
| 이준영, 안상진 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%EC%83%81%EC%86%8D%20%EA%B5%AC%EC%A1%B0%EB%A5%BC%20%EC%9D%B4%EC%9A%A9%ED%95%9C%20Weapon"> **상속 구조를 이용한 Weapon** </A> | 
| 이준영 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%EC%B6%94%EC%83%81%20%ED%81%B4%EB%9E%98%EC%8A%A4%EC%99%80%20%EC%9D%B8%ED%84%B0%ED%8E%98%EC%9D%B4%EC%8A%A4%EB%A5%BC%20%EC%82%AC%EC%9A%A9%ED%95%9C%20Condition%20%EA%B5%AC%EC%A1%B0">**추상 클래스와 인터페이스를 사용한 Condition 구조**</A> | 
| 이준영, 안상진, 김재경 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/Cinemacine%EC%9D%84%20%ED%99%9C%EC%9A%A9%ED%95%9C%20%EC%B9%B4%EB%A9%94%EB%9D%BC%20%EC%97%B0%EC%B6%9C">**Cinemacine을 활용한 카메라 연출**</A> | 
| 안상진, 김재경 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/Singleton%EC%9D%84%20%ED%99%9C%EC%9A%A9%ED%95%9C%20%EB%8B%A4%EC%96%91%ED%95%9C%20%EB%A7%A4%EB%8B%88%EC%A0%80">**Singleton을 활용한 다양한 매니저**</A> | 
| 이준영, 김재경  | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%EC%83%81%ED%98%B8%EC%9E%91%EC%9A%A9%20%EA%B0%80%EB%8A%A5%ED%95%9C%20%EC%95%84%EC%9D%B4%ED%85%9C%20%EA%B5%AC%EC%A1%B0">**상호작용 가능한 아이템 구조** </A>| 
| 김재경 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%EC%96%B4%EB%93%9C%EB%A0%88%EC%84%9C%EB%B8%94%EC%9D%84%20%ED%99%9C%EC%9A%A9%ED%95%9C%20SoundManager"> **어드레서블을 활용한 SoundManager** </A> |
| 김재경 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8%20%EC%A0%9C%EC%9E%91%EC%97%90%20%EB%8F%84%EC%9B%80%EB%90%98%EB%8A%94%20%EB%8B%A4%EC%96%91%ED%95%9C%20%ED%97%AC%ED%8D%BC ">**프로젝트 제작에 도움되는 다양한 헬퍼** </A>| 
| 안상진, 김재경 | <A href="https://github.com/leejy1685/LastSanctuary_Code/tree/main/%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%EC%99%80%20%EC%83%81%ED%98%B8%EC%9E%91%EC%9A%A9%20%EA%B0%80%EB%8A%A5%ED%95%9C%20%EB%A7%B5%20%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8">**플레이어와 상호작용 가능한 맵 오브젝트**</A> | 

---

### 팀원 구성

| 팀원 | 역할 |
| --- | --- |
류천복 | 메인 기획 |
경원철 | 서브 기획 |
이준영 | 메인 개발 |
안상진 | 서브 개발 |
김재경 | 서브 개발 |

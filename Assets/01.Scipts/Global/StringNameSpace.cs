
/// <summary>
/// 오류가 많은 스트링 값을 모아두는 클래스
/// </summary>
public static class StringNameSpace
{
    public const string test = "Test/Prefabs/Cube";

    //게임 데이터 경로
    public static class GameData
    {
        public const string SavePath = "/02.Data/GameData";

        public const string MapItem = "MapItem.json";
    }

    //Json 데이터 경로
    public static class JsonAddress
    {
        public const string EnemyJsonAddress = "Assets/02.Data/GameData/TestEnemyData.json";
    }

    //사운드에 필요한 주소
    public static class SoundAddress
    {
        public const string MainMixer = "Assets/05.Input/MainMixer.mixer";
        public const string BGMMixer = "Assets/05.Input/MainMixer.mixer[BGMMixer]";

        public const string SFXPrefab = "Assets/04.Prefabs/Global/SFX.prefab";

        public const string TutorialBGM = "Assets/06.Resource/Sound/BGM/Tutorials_Sound.wav";
        public const string TutorialBossPhase1 = "Assets/06.Resource/Sound/BGM/Boss01_Phase1.wav";
        public const string TutorialPhaseShift = "Assets/06.Resource/Sound/BGM/Boss01_PhaseShift.wav";
        public const string TutorialBossPhase2 = "Assets/06.Resource/Sound/BGM/Boss01_Phase2.wav";
    }

    //어드레서블에 필요한 라벨
    public static class Labels
    {
        public const string Relic = "Relic";
    }

    //테그 정보
    public static class Tags
    {
        public const string Respawn = "Respawn";
        public const string Finish = "Finish";
        public const string EditorOnly = "EditorOnly";
        public const string MainCamera = "MainCamera";
        public const string Player = "Player";
        public const string GameController = "GameController";
        public const string Ladder = "Ladder";
        public const string AerialPlatform = "AerialPlatform";
        public const string SavePoint = "SavePoint";
        public const string Enemy = "Enemy";
        public const string Ground = "Ground";
        public const string BackGround = "BackGround";
        public const string Wall = "Wall";

    }

    public static class ValueSpace
    {
        public const float MoveDistance = 3.6f;
    }
}

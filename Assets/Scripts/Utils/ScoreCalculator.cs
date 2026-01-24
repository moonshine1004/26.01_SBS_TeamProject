public static class ScoreUtil
{
    /// <summary>
    /// 클리어 시 사용할 점수 계산 식
    /// </summary>
    /// <param name="tileScore">주파한 계단 수</param>
    /// <param name="remainingTime">남은 시간</param>
    /// <param name="playerBonus">캐릭터 별 보너스 배율</param>
    /// <returns>최종 점수</returns>
    public static int CalculateClearScore(int tileScore, float remainingTime, float playerBonus)
    {
        float score = ((remainingTime + tileScore) * playerBonus) * 100;
        return (int)score;
    }
}
namespace StationSearch.Models
{
    public static class PrefectureCodeMapper
    {
        public static string GetPrefecture(PrefectureCode code)
        {
            switch (code)
            {
                case PrefectureCode.Tokyo: return "東京都";
                default: return "";
            }
        }
    }
}

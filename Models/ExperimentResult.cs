namespace WordSorterApp.Models
{
    public class ExperimentResult
    {
        public int WordCount { get; set; }
        public long QuickSortTimeMs { get; set; }
        public long AbcSortTimeMs { get; set; }
    }
}
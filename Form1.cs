using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WordSorterApp.Models;
using WordSorterApp.Services;

namespace WordSorterApp
{
    public partial class Form1 : Form
    {
        private WordSorter _wordSorter;
        private List<ExperimentResult> _experiments;

        public Form1()
        {
            InitializeComponent();
            _wordSorter = new WordSorter();
            _experiments = new List<ExperimentResult>();
            InitializeDataGrids();
            comboBoxWordCount.SelectedIndex = 0;
        }

        private void InitializeDataGrids()
        {
            // Настройка таблицы результатов
            dataGridViewResults.Columns.Clear();
            dataGridViewResults.Columns.Add("Word", "Слово");
            dataGridViewResults.Columns.Add("Count", "Количество");
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Настройка таблицы экспериментов
            dataGridViewExperiments.Columns.Clear();
            dataGridViewExperiments.Columns.Add("Words", "Количество слов");
            dataGridViewExperiments.Columns.Add("QuickSortTime", "Быстрая (мс)");
            dataGridViewExperiments.Columns.Add("AbcSortTime", "ABC (мс)");
            dataGridViewExperiments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxInput.Text))
            {
                MessageBox.Show("Введите текст для сортировки!");
                return;
            }

            try
            {
                // Быстрая сортировка
                var (quickResults, quickTime) = _wordSorter.QuickSortWords(textBoxInput.Text);
                DisplayResults(quickResults, quickTime, true);

                // ABC-сортировка
                var (abcResults, abcTime) = _wordSorter.AbcSortWords(textBoxInput.Text);
                DisplayResults(abcResults, abcTime, false);

                labelTimeQuick.Text = $"Быстрая: {quickTime} мс";
                labelTimeAbc.Text = $"ABC: {abcTime} мс";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (comboBoxWordCount.SelectedItem == null)
            {
                MessageBox.Show("Выберите количество слов!");
                return;
            }

            try
            {
                int wordCount = int.Parse(comboBoxWordCount.SelectedItem.ToString());
                string generatedText = GenerateEnglishText(wordCount);
                textBoxInput.Text = generatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации: {ex.Message}");
            }
        }

        private void DisplayResults(List<SortResult> results, long time, bool isQuickSort)
        {
            if (!isQuickSort) return; // Показываем только результаты быстрой сортировки

            dataGridViewResults.Rows.Clear();
            foreach (var result in results)
            {
                dataGridViewResults.Rows.Add(result.Word, result.Count);
            }

            // Добавляем эксперимент в таблицу
            int wordCount = results.Sum(r => r.Count);
            _experiments.Add(new ExperimentResult
            {
                WordCount = wordCount,
                QuickSortTime = isQuickSort ? time : 0,
                AbcSortTime = !isQuickSort ? time : 0
            });

            UpdateExperimentsTable();
        }

        private void UpdateExperimentsTable()
        {
            dataGridViewExperiments.Rows.Clear();
            foreach (var exp in _experiments)
            {
                dataGridViewExperiments.Rows.Add(
                    exp.WordCount,
                    exp.QuickSortTime,
                    exp.AbcSortTime
                );
            }
        }

        private string GenerateEnglishText(int wordCount)
        {
            var words = new[]
            {
                "the", "be", "to", "of", "and", "a", "in", "that", "have", "I",
                "it", "for", "not", "on", "with", "he", "as", "you", "do", "at",
                "this", "but", "his", "by", "from", "they", "we", "say", "her", "she",
                "or", "an", "will", "my", "one", "all", "would", "there", "their", "what",
                "so", "up", "out", "if", "about", "who", "get", "which", "go", "me",
                "when", "make", "can", "like", "time", "no", "just", "him", "know", "take",
                "people", "into", "year", "your", "good", "some", "could", "them", "see", "other",
                "than", "then", "now", "look", "only", "come", "its", "over", "think", "also",
                "back", "after", "use", "two", "how", "our", "work", "first", "well", "way",
                "even", "new", "want", "because", "any", "these", "give", "day", "most", "us"
            };

            var random = new Random();
            var generatedWords = new List<string>();

            for (int i = 0; i < wordCount; i++)
            {
                generatedWords.Add(words[random.Next(words.Length)]);
            }

            return string.Join(" ", generatedWords);
        }
    }

    public class ExperimentResult
    {
        public int WordCount { get; set; }
        public long QuickSortTime { get; set; }
        public long AbcSortTime { get; set; }
    }
}
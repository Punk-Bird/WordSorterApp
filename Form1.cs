using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordSorterApp.Models;
using WordSorterApp.Services;

namespace WordSorterApp
{
    public partial class Form1 : Form
    {
        private WordSorter _wordSorter;
        private List<ExperimentResult> _experiments;
        private string _currentTimeUnit = "Миллисекунды";
        private bool _showChart = false;

        public Form1()
        {
            InitializeComponent();
            _wordSorter = new WordSorter();
            _experiments = new List<ExperimentResult>();
            InitializeDataGrids();
            comboBoxWordCount.SelectedIndex = 0;
            comboBoxTimeUnit.SelectedIndex = 0;
        }

        private void InitializeDataGrids()
        {
            dataGridViewResults.Columns.Clear();
            dataGridViewResults.Columns.Add("Word", "Слово");
            dataGridViewResults.Columns.Add("Count", "Количество");
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewExperiments.Columns.Clear();
            dataGridViewExperiments.Columns.Add("Words", "Количество слов");
            dataGridViewExperiments.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewExperiments.Columns.Add("QuickSortTime", "Быстрая");
            dataGridViewExperiments.Columns.Add("AbcSortTime", "ABC");
            dataGridViewExperiments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Форматирование для лучшего отображения больших чисел
            dataGridViewExperiments.Columns["Words"].DefaultCellStyle.Format = "N0";
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
                List<SortResult> quickResults, abcResults;
                long quickTime, abcTime;

                // Выбираем метод измерения в зависимости от выбранной единицы
                if (_currentTimeUnit == "Микросекунды")
                {
                    (quickResults, quickTime) = _wordSorter.QuickSortWordsPrecise(textBoxInput.Text);
                    (abcResults, abcTime) = _wordSorter.AbcSortWordsPrecise(textBoxInput.Text);
                }
                else
                {
                    (quickResults, quickTime) = _wordSorter.QuickSortWords(textBoxInput.Text);
                    (abcResults, abcTime) = _wordSorter.AbcSortWords(textBoxInput.Text);
                }

                // Конвертируем время если нужно
                quickTime = ConvertTime(quickTime);
                abcTime = ConvertTime(abcTime);

                DisplayResults(quickResults);
                UpdateTimeLabels(quickTime, abcTime);

                // Добавляем эксперимент
                int wordCount = quickResults.Sum(r => r.Count);
                _experiments.Add(new ExperimentResult
                {
                    WordCount = wordCount,
                    QuickSortTimeMs = quickTime,
                    AbcSortTimeMs = abcTime
                });

                UpdateExperimentsTable();
                if (_showChart) panelChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private long ConvertTime(long timeMs)
        {
            return _currentTimeUnit switch
            {
                "Секунды" => timeMs / 1000,
                "Микросекунды" => timeMs,
                _ => timeMs // Миллисекунды
            };
        }

        private string GetTimeUnitSuffix()
        {
            return _currentTimeUnit switch
            {
                "Секунды" => " сек",
                "Микросекунды" => " мкс",
                _ => " мс"
            };
        }

        private void UpdateTimeLabels(long quickTime, long abcTime)
        {
            string suffix = GetTimeUnitSuffix();
            labelTimeQuick.Text = $"Быстрая: {quickTime}{suffix}";
            labelTimeAbc.Text = $"ABC: {abcTime}{suffix}";
        }

        private void DisplayResults(List<SortResult> results)
        {
            dataGridViewResults.Rows.Clear();
            foreach (var result in results)
            {
                dataGridViewResults.Rows.Add(result.Word, result.Count);
            }
        }

        private void UpdateExperimentsTable()
        {
            dataGridViewExperiments.Rows.Clear();
            string suffix = GetTimeUnitSuffix();

            foreach (var exp in _experiments)
            {
                dataGridViewExperiments.Rows.Add(
                    exp.WordCount,
                    exp.QuickSortTimeMs + suffix,
                    exp.AbcSortTimeMs + suffix
                );
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

                // Показываем прогресс для больших текстов
                if (wordCount > 2000)
                {
                    Cursor = Cursors.WaitCursor;
                    buttonGenerate.Enabled = false;
                    buttonGenerate.Text = "Генерация...";
                    Application.DoEvents(); // Обновляем интерфейс
                }

                string generatedText = GenerateEnglishText(wordCount);
                textBoxInput.Text = generatedText;

                // Показываем статистику
                ShowGenerationStats(wordCount, generatedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации: {ex.Message}");
            }
            finally
            {
                // Восстанавливаем интерфейс
                Cursor = Cursors.Default;
                buttonGenerate.Enabled = true;
                buttonGenerate.Text = "Сгенерировать текст";
            }
        }

        private void ShowGenerationStats(int targetWordCount, string generatedText)
        {
            // Подсчитываем реальное количество слов
            var words = generatedText.Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '\t', '\n', '\r' },
                                          StringSplitOptions.RemoveEmptyEntries);
            int actualWordCount = words.Length;

            // Показываем краткую статистику в заголовке
            this.Text = $"Word Sorter Application - Сгенерировано: {actualWordCount} слов (цель: {targetWordCount})";

            // Для больших текстов показываем информацию о размере
            if (targetWordCount >= 1000)
            {
                int textLength = generatedText.Length;
                double kbSize = textLength / 1024.0;
                /*
                MessageBox.Show($"Текст успешно сгенерирован!\n" +
                               $"Слов: {actualWordCount}\n" +
                               $"Символов: {textLength:N0}\n" +
                               $"Размер: {kbSize:F1} КБ",
                               "Генерация завершена",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
                */
            }
        }

        private void comboBoxTimeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTimeUnit = comboBoxTimeUnit.SelectedItem.ToString();
            UpdateExperimentsTable();

            // Обновляем labels текущего времени
            if (_experiments.Any())
            {
                var lastExp = _experiments.Last();
                UpdateTimeLabels(lastExp.QuickSortTimeMs, lastExp.AbcSortTimeMs);
            }
        }

        private void buttonClearExperiments_Click(object sender, EventArgs e)
        {
            _experiments.Clear();
            UpdateExperimentsTable();
            panelChart.Invalidate();
        }

        private void buttonShowChart_Click(object sender, EventArgs e)
        {
            _showChart = !_showChart;
            buttonShowChart.Text = _showChart ? "Скрыть график" : "График";
            panelChart.Visible = _showChart;
            label4.Visible = _showChart;

            if (_showChart)
            {
                this.Height = 600;
                panelChart.Invalidate();
            }
            else
            {
                this.Height = 550;
            }
        }

        private void panelChart_Paint(object sender, PaintEventArgs e)
        {
            if (!_showChart || _experiments.Count == 0) return;

            var g = e.Graphics;
            g.Clear(Color.White);

            int padding = 40;
            int width = panelChart.Width - padding * 2;
            int height = panelChart.Height - padding * 2;

            // Находим максимальные значения для масштабирования
            long maxTime = _experiments.Max(exp => Math.Max(exp.QuickSortTimeMs, exp.AbcSortTimeMs));
            int maxWords = _experiments.Max(exp => exp.WordCount);

            if (maxTime == 0) maxTime = 1;

            // Рисуем оси
            g.DrawLine(Pens.Black, padding, padding, padding, padding + height);
            g.DrawLine(Pens.Black, padding, padding + height, padding + width, padding + height);

            // Подписи осей
            g.DrawString("Количество слов", this.Font, Brushes.Black, padding + width / 2 - 30, padding + height + 10);

            var rotateFormat = new StringFormat();
            rotateFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            g.DrawString("Время (" + GetTimeUnitSuffix().Trim() + ")", this.Font, Brushes.Black, 5, padding + height / 2 - 30, rotateFormat);

            // Сортируем эксперименты по количеству слов
            var sortedExps = _experiments.OrderBy(exp => exp.WordCount).ToList();

            // Рисуем график для быстрой сортировки
            DrawChartLine(g, sortedExps, exp => exp.QuickSortTimeMs, Color.Red, padding, width, height, maxTime, maxWords);

            // Рисуем график для ABC сортировки
            DrawChartLine(g, sortedExps, exp => exp.AbcSortTimeMs, Color.Blue, padding, width, height, maxTime, maxWords);

            // Легенда
            g.FillRectangle(new SolidBrush(Color.Red), padding + width - 100, padding - 20, 10, 10);
            g.DrawString("Быстрая", this.Font, Brushes.Black, padding + width - 85, padding - 20);

            g.FillRectangle(new SolidBrush(Color.Blue), padding + width - 100, padding - 5, 10, 10);
            g.DrawString("ABC", this.Font, Brushes.Black, padding + width - 85, padding - 5);
        }

        private void DrawChartLine(Graphics g, List<ExperimentResult> exps, Func<ExperimentResult, long> timeSelector,
                                 Color color, int padding, int width, int height, long maxTime, int maxWords)
        {
            if (exps.Count < 2) return;

            var points = new PointF[exps.Count];

            for (int i = 0; i < exps.Count; i++)
            {
                var exp = exps[i];
                float x = padding + (exp.WordCount / (float)maxWords) * width;
                float y = padding + height - (timeSelector(exp) / (float)maxTime) * height;
                points[i] = new PointF(x, y);
            }

            // Рисуем линию
            using (var pen = new Pen(color, 2))
            {
                g.DrawLines(pen, points);
            }

            // Рисуем точки
            foreach (var point in points)
            {
                g.FillEllipse(new SolidBrush(color), point.X - 3, point.Y - 3, 6, 6);
            }
        }

        private string GenerateEnglishText(int wordCount)
        {
            // Расширенный список английских слов (400+ слов)
            var words = new[]
            {
        // Основные глаголы (80 слов)
        "be", "have", "do", "say", "get", "make", "go", "know", "take", "see",
        "come", "think", "look", "want", "give", "use", "find", "tell", "ask", "work",
        "seem", "feel", "try", "leave", "call", "show", "provide", "create", "develop", "understand",
        "consider", "become", "include", "continue", "set", "learn", "change", "begin", "help", "play",
        "run", "move", "like", "live", "believe", "hold", "bring", "happen", "write", "sit",
        "stand", "lose", "pay", "meet", "include", "contain", "represent", "form", "read", "grow",
        "open", "walk", "stop", "serve", "die", "send", "build", "stay", "fall", "reach",
        "remember", "love", "buy", "wait", "serve", "decide", "apply", "explain", "offer", "plan",

        // Существительные (150 слов)
        "time", "person", "year", "way", "day", "thing", "man", "world", "life", "hand",
        "part", "child", "eye", "woman", "place", "work", "week", "case", "point", "government",
        "company", "number", "group", "problem", "fact", "people", "system", "program", "question", "business",
        "power", "money", "story", "month", "book", "family", "state", "student", "country", "city",
        "school", "friend", "father", "mother", "water", "room", "area", "food", "door", "house",
        "car", "service", "market", "information", "technology", "computer", "internet", "software", "data", "research",
        "health", "education", "music", "art", "film", "science", "history", "language", "culture", "society",
        "nature", "environment", "energy", "development", "management", "experience", "knowledge", "community", "security", "quality",
        "price", "product", "customer", "project", "team", "minute", "hour", "morning", "evening", "night",
        "summer", "winter", "spring", "autumn", "color", "sound", "light", "air", "fire", "earth",
        "result", "effect", "process", "level", "industry", "service", "theory", "growth", "form", "value",
        "body", "market", "law", "policy", "study", "office", "moment", "reason", "future", "position",
        "interest", "subject", "period", "situation", "table", "need", "court", "report", "times", "section",
        "member", "century", "evidence", "freedom", "source", "care", "activity", "series", "model", "space",

        // Прилагательные (120 слов)
        "good", "new", "first", "last", "long", "great", "little", "own", "other", "old",
        "right", "big", "high", "different", "small", "large", "next", "early", "young", "important",
        "public", "bad", "same", "able", "better", "best", "beautiful", "happy", "easy", "hard",
        "strong", "clear", "free", "real", "open", "complete", "simple", "present", "special", "difficult",
        "possible", "interesting", "national", "international", "local", "global", "modern", "traditional", "digital", "physical",
        "natural", "social", "economic", "political", "cultural", "environmental", "technical", "professional", "personal", "general",
        "specific", "current", "future", "past", "recent", "ancient", "quick", "slow", "hot", "cold",
        "warm", "cool", "dark", "light", "bright", "soft", "loud", "quiet", "sweet", "bitter",
        "major", "minor", "central", "similar", "human", "various", "available", "popular", "basic", "certain",
        "common", "individual", "necessary", "positive", "private", "serious", "final", "primary", "recent", "significant",
        "standard", "able", "aware", "careful", "effective", "electronic", "federal", "financial", "legal", "medical",
        "mental", "normal", "official", "powerful", "random", "responsible", "safe", "separate", "successful", "unique",

        // Наречия и местоимения (60 слов)
        "very", "always", "often", "sometimes", "never", "quickly", "slowly", "easily", "well", "now",
        "then", "here", "there", "everywhere", "somewhere", "anywhere", "nowhere", "today", "yesterday", "tomorrow",
        "always", "usually", "frequently", "rarely", "almost", "completely", "totally", "partially", "mostly", "generally",
        "specifically", "directly", "indirectly", "immediately", "eventually", "finally", "initially", "recently", "soon", "later",
        "together", "alone", "abroad", "ahead", "back", "close", "deep", "early", "far", "fast",
        "high", "inside", "near", "outside", "straight", "wide", "wrong", "correctly", "properly", "perfectly",

        // Профессии и специализации (40 слов)
        "doctor", "teacher", "engineer", "scientist", "artist", "writer", "musician", "actor", "director", "manager",
        "developer", "designer", "analyst", "researcher", "professor", "student", "lawyer", "nurse", "architect", "photographer",
        "journalist", "editor", "chef", "pilot", "driver", "farmer", "worker", "officer", "agent", "specialist",
        "consultant", "expert", "assistant", "associate", "coordinator", "administrator", "supervisor", "technician", "therapist", "counselor",

        // Технологии и современные термины (40 слов)
        "digital", "virtual", "online", "mobile", "cloud", "network", "database", "algorithm", "application", "platform",
        "interface", "device", "machine", "robot", "artificial", "intelligence", "learning", "analysis", "innovation", "solution",
        "strategy", "process", "method", "approach", "concept", "theory", "practice", "implementation", "optimization", "automation",
        "blockchain", "cryptocurrency", "metaverse", "quantum", "biotechnology", "nanotechnology", "robotics", "cybersecurity", "analytics", "framework"
    };

            var random = new Random();
            var generatedWords = new List<string>();

            // Для больших текстов добавляем больше разнообразия
            var commonWords = new[] { "the", "and", "for", "with", "that", "this", "from", "have", "are", "was", "is", "in", "it", "to", "of", "a" };

            for (int i = 0; i < wordCount; i++)
            {
                // Для больших текстов увеличиваем разнообразие
                if (wordCount > 1000)
                {
                    // 15% шанс использовать очень распространенное слово
                    if (random.Next(100) < 15 && i > 10)
                    {
                        generatedWords.Add(commonWords[random.Next(commonWords.Length)]);
                    }
                    else
                    {
                        generatedWords.Add(words[random.Next(words.Length)]);
                    }
                }
                else
                {
                    // Для маленьких текстов - больше разнообразия
                    generatedWords.Add(words[random.Next(words.Length)]);
                }

                // Добавляем знаки препинания для реалистичности
                if (random.Next(100) < 8 && i < wordCount - 1)
                {
                    var punctuation = new[] { ",", ".", "!" };
                    if (random.Next(100) < 25)
                    {
                        generatedWords[generatedWords.Count - 1] += punctuation[random.Next(punctuation.Length)];
                    }
                }

                // Для очень больших текстов показываем прогресс
                if (wordCount > 5000 && i % 1000 == 0)
                {
                    buttonGenerate.Text = $"Генерация... {i}/{wordCount}";
                    Application.DoEvents();
                }
            }

            // Добавляем заглавные буквы в начале предложений
            var result = new List<string>();
            bool newSentence = true;

            foreach (var word in generatedWords)
            {
                if (newSentence && word.Length > 0 && char.IsLetter(word[0]))
                {
                    result.Add(char.ToUpper(word[0]) + word.Substring(1));
                    newSentence = false;
                }
                else
                {
                    result.Add(word);
                }

                if (word.EndsWith(".") || word.EndsWith("!") || word.EndsWith("?"))
                {
                    newSentence = true;
                }
            }

            return string.Join(" ", result);
        }
    }
}
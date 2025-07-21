using System.Collections.ObjectModel;

namespace FastLineSeriesSample
{
    public class ViewModel
    {
        private Random randomNumber;
        public ObservableCollection<DataModel> DataSource { get; set; }

        public ViewModel()
        {
            randomNumber = new Random();
            DataSource = GenerateData(500);
        }

        public ObservableCollection<DataModel> GenerateData(int dataCount)
        {
            ObservableCollection<DataModel> data = new();
            DateTime date = new DateTime(2010, 1, 1);
            double value = 100;

            int totalCount = dataCount;
            int nanBlockCount = 3;
            int[] nanBlockStarts = new int[nanBlockCount];
            int[] nanBlockLengths = new int[nanBlockCount];

            for (int i = 0; i < nanBlockCount; i++)
            {
                int blockLength = randomNumber.Next(25, 38);
                int maxStart = totalCount - blockLength;
                int start;

                do
                {
                    start = randomNumber.Next(0, maxStart);
                }
                while (nanBlockStarts.Any(s => Math.Abs(s - start) < 10));

                nanBlockStarts[i] = start;
                nanBlockLengths[i] = blockLength;
            }

            for (int j = 0; j < totalCount; j++)
            {
                bool isNaN = false;

                for (int k = 0; k < nanBlockCount; k++)
                {
                    if (j >= nanBlockStarts[k] && j < nanBlockStarts[k] + nanBlockLengths[k])
                    {
                        isNaN = true;
                        break;
                    }
                }

                if (isNaN)
                {
                    data.Add(new DataModel { Date = date, Metric = double.NaN });
                }
                else
                {
                    data.Add(new DataModel { Date = date, Metric = value });

                    if (randomNumber.NextDouble() > 0.5)
                        value += randomNumber.NextDouble();
                    else
                        value -= randomNumber.NextDouble();

                    value = Math.Min(value, 134);
                }

                date = date.AddDays(1);
            }

            return data;
        }
    }
}

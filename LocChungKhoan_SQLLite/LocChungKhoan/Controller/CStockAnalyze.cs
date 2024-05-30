using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan.Controller
{
    public static class Extensions
    {
        // Hàm mở rộng để tính độ lệch chuẩn cho danh sách số
        public static decimal StandardDeviation(this IEnumerable<decimal> values)
        {
            if (values == null || !values.Any())
            {
                return 0;
            }

            decimal avg = values.Average();
            decimal sumOfSquares = values.Select(x => (x - avg) * (x - avg)).Sum();
            return (decimal)Math.Sqrt((double)sumOfSquares / (values.Count() - 1));
        }
    }
    public class StockAnalyzer
    {
        private readonly List<BieuDoKhoiLuong> stockDataList;

        public StockAnalyzer(List<BieuDoKhoiLuong> stockDataList)
        {
            this.stockDataList = stockDataList;
        }
        //==============================phân tích dựa trên đột biến giá và khối lượng
        private bool IsVolumePriceVolatile(decimal[] volumes, decimal[] prices, int weeks)
        {
            if (volumes.Length < weeks * 5 || prices.Length < weeks * 5)
            {
                throw new ArgumentException("Độ dài mảng khối lượng và giá phải lớn hơn hoặc bằng 5* số tuần");
            }

            decimal[] averageVolumes = CalculateAverage(volumes, weeks);
            decimal[] averagePrices = CalculateAverage(prices, weeks);

            decimal currentWeekVolume = volumes[volumes.Length - 1];
            decimal currentWeekPrice = prices[prices.Length - 1];

            decimal volumeChange = (currentWeekVolume - averageVolumes[averageVolumes.Length - 1]) / averageVolumes[averageVolumes.Length - 1];
            decimal priceChange = (currentWeekPrice - averagePrices[averagePrices.Length - 1]) / averagePrices[averagePrices.Length - 1];

            // Tùy chỉnh ngưỡng biến động theo chiến lược
            const decimal volumeThreshold = (decimal)0.2; // Biến động khối lượng tối thiểu 20%
            const decimal priceThreshold = (decimal)0.05; // Biến động giá tối thiểu 5%

            return Math.Abs(volumeChange) >= volumeThreshold && Math.Abs(priceChange) >= priceThreshold;
        }
        // Hàm tính biên độ dao động tuyệt đối
        public decimal CalculateAbsoluteVolatility(List<BieuDoKhoiLuong> data)
        {
            decimal highestPrice = data.Max(d => d.GiaCaoNhat);
            decimal lowestPrice = data.Min(d => d.GiaThapNhat);
            return highestPrice - lowestPrice;
        }

        // Hàm tính biên độ dao động tương đối
        public decimal CalculateRelativeVolatility(List<BieuDoKhoiLuong> data)
        {
            decimal averagePrice = data.Average(d => d.GiaDongCua);
            return CalculateAbsoluteVolatility(data) / averagePrice;
        }

        // Hàm tính độ lệch chuẩn giá đóng cửa
        public decimal CalculateClosingPriceStandardDeviation(List<BieuDoKhoiLuong> data)
        {
            return data.Select(d => d.GiaDongCua).StandardDeviation();
        }

        // Hàm tính độ lệch chuẩn khối lượng giao dịch
        public decimal CalculateVolumeStandardDeviation(List<BieuDoKhoiLuong> data)
        {
            return data.Select(d => d.KhoiLuong).StandardDeviation();
        }

        // Hàm kiểm tra biến động giá
        public bool IsPriceVolatilitySignificant(List<BieuDoKhoiLuong> data, decimal threshold)
        {
            decimal relativeVolatility = CalculateRelativeVolatility(data);
            return relativeVolatility > threshold;
        }

        // Hàm kiểm tra biến động khối lượng
        public bool IsVolumeVolatilitySignificant(List<BieuDoKhoiLuong> data, decimal threshold)
        {
            decimal volumeStandardDeviation = CalculateVolumeStandardDeviation(data);
            return volumeStandardDeviation > threshold;
        }

        // Hàm in ra các cổ phiếu có biến động đáng chú ý
        public void PrintVolatilityStocks(List<BieuDoKhoiLuong> listCoPhieu, int days, decimal priceThreshold=0.05m, decimal volumeThreshold=0.1m)       
        {            
            // Lấy dữ liệu của các cổ phiếu trong 'days' ngày gần nhất
            var groupedStocks = listCoPhieu.GroupBy(s => s.MaChungKhoan)
                .Where(g => g.Count() >= days)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.OrderByDescending(d => d.Ngay).Take(days).ToList() });

            Console.WriteLine("Các cổ phiếu có biến động đáng chú ý:");
            foreach (var stock in groupedStocks)
            {
                bool isPriceVolatilitySignificant = IsPriceVolatilitySignificant(stock.Data, priceThreshold);
                bool isVolumeVolatilitySignificant = IsVolumeVolatilitySignificant(stock.Data, volumeThreshold);

                if (isPriceVolatilitySignificant || isVolumeVolatilitySignificant)
                {
                    Console.WriteLine($"Mã cổ phiếu: {stock.MaChungKhoan}");
                    Console.WriteLine($"Biến động giá: {(isPriceVolatilitySignificant ? "Có" : "Không")}");
                    Console.WriteLine($"Biến động khối lượng: {(isVolumeVolatilitySignificant ? "Có" : "Không")}");
                    Console.WriteLine();
                }
            }
        }
        //================================================Tính theo MACD
        public List<string> AnalyzePotentialIncreaseWithVolumePriceMACD()
        {
            var groupedData = stockDataList.GroupBy(x => x.MaChungKhoan);

            List<string> potentialStocks = new List<string>();

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa
                var volumes = group.Select(x => x.KhoiLuong).ToArray(); // Lấy khối lượng

                // Kiểm tra biến động khối lượng và giá trong 3 tuần
                if (IsVolumePriceVolatile(volumes, prices, 3))
                {
                    // Tính toán MACD trong 3 tuần
                    decimal[] macdLine = CalculateMACD(prices, 12, 26, 9);

                    // Xác định MACD tuần kế tiếp
                    decimal nextWeekMACD = macdLine[macdLine.Length - 1];

                    // Xác định xu hướng MACD
                    bool isBullishMACD = IsBullishMACD(nextWeekMACD);

                    if (isBullishMACD)
                    {
                        potentialStocks.Add(stockCode);
                    }
                }
            }

            return potentialStocks;
        }        

        private decimal[] CalculateAverage(decimal[] values, int weeks)
        {
            if (values.Length < weeks)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng số tuần");
            }

            decimal[] averages = new decimal[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                int startIndex = Math.Max(0, i - weeks + 1);
                int endIndex = Math.Min(i, values.Length - 1);

                decimal sum = 0;
                int count = endIndex - startIndex + 1;

                for (int j = startIndex; j <= endIndex; j++)
                {
                    sum += values[j];
                }

                averages[i] = sum / count;
            }

            return averages;
        }
        private decimal[] CalculateMACD(decimal[] prices, int EMA12Period, int EMA26Period, int signalPeriod)
        {
            if (prices.Length < Math.Max(EMA12Period, EMA26Period) + signalPeriod)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng tổng chu kỳ EMA và chu kỳ tín hiệu");
            }

            decimal[] ema12 = CalculateEMA(prices, EMA12Period);
            decimal[] ema26 = CalculateEMA(prices, EMA26Period);

            decimal[] macdLine = new decimal[prices.Length];
            decimal[] signalLine = new decimal[prices.Length];
            decimal[] macdHistogram = new decimal[prices.Length];

            for (int i = EMA12Period - 1; i < prices.Length; i++)
            {
                macdLine[i] = ema12[i] - ema26[i];

                if (i >= EMA26Period - 1 + signalPeriod)
                {
                    signalLine[i] = CalculateEMA(macdLine, signalPeriod)[i];
                    macdHistogram[i] = macdLine[i] - signalLine[i];
                }
            }

            return macdHistogram;
        }

        private decimal[] CalculateEMA(decimal[] prices, int period)
        {
            if (prices.Length < period)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng chu kỳ EMA");
            }

            decimal[] ema = new decimal[prices.Length];

            ema[0] = prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                ema[i] = (prices[i] * (2 / (period + 1))) + (ema[i - 1] * ((period - 1) / (period + 1)));
            }

            return ema;
        }
        private bool IsBullishMACD(decimal macdValue)
        {
            if (macdValue > 0)
            {
                return true; // MACD dương, có khả năng tăng giá
            }
            else
            {
                return false; // MACD âm, có khả năng giảm giá
            }
        }
        public void AnalyzePotentialIncreaseWithMACD()
        {
            var groupedData = stockDataList.GroupBy(x => x.MaChungKhoan);

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa

                // Tính toán MACD trong 3 tuần
                decimal[] macdLine = CalculateMACD(prices, 12, 26, 9);

                // Xác định MACD tuần kế tiếp
                decimal nextWeekMACD = macdLine[macdLine.Length - 1];

                // Xác định xu hướng MACD
                bool isBullishMACD = IsBullishMACD(nextWeekMACD);

                Console.WriteLine("Mã chứng khoán: {0}", stockCode);
                Console.WriteLine("MACD tuần kế tiếp: {1}", nextWeekMACD);

                if (isBullishMACD)
                {
                    Console.WriteLine("Cổ phiếu có tiềm năng tăng giá dựa trên MACD");
                }
                else
                {
                    Console.WriteLine("Cổ phiếu trung lập hoặc có tiềm năng giảm giá dựa trên MACD");
                }

                Console.WriteLine("---------------------------------");
            }
        }
        //------Tính theo RSI
        public void AnalyzePotentialIncreaseByRSI()
        {
            var groupedData = stockDataList.GroupBy(x => x.MaChungKhoan);

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa

                decimal rsi = CalculateRSI(prices, 14); // Chu kỳ RSI 14
                bool isBullish = IsRSIBullish(rsi);

                Console.WriteLine("Mã chứng khoán: {0}", stockCode);
                Console.WriteLine("RSI: {1}", rsi);

                if (isBullish)
                {
                    Console.WriteLine("Cổ phiếu có tiềm năng tăng giá");
                }
                else
                {
                    Console.WriteLine("Cổ phiếu trung lập hoặc có tiềm năng giảm giá");
                }

                Console.WriteLine("---------------------------------");
            }
        }
        private decimal CalculateRSI(decimal[] prices, int period)
        {
            if (prices.Length < period)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng chu kỳ RSI");
            }

            decimal[] averageUp = new decimal[prices.Length];
            decimal[] averageDown = new decimal[prices.Length];

            // Khởi tạo giá trị trung bình
            averageUp[0] = 0;
            averageDown[0] = 0;

            // Tính toán giá trị trung bình tăng và giảm
            for (int i = 1; i < prices.Length; i++)
            {
                decimal change = prices[i] - prices[i - 1];

                if (change >= 0)
                {
                    averageUp[i] = change + (period - 1) * averageUp[i - 1];
                    averageDown[i] = averageDown[i - 1];
                }
                else
                {
                    averageUp[i] = averageUp[i - 1];
                    averageDown[i] = change + (period - 1) * averageDown[i - 1];
                }
            }

            // Tính toán RSI
            decimal[] rsi = new decimal[prices.Length];

            for (int i = period; i < prices.Length; i++)
            {
                if (averageDown[i] == 0)
                {
                    rsi[i] = 100;
                }
                else
                {
                    rsi[i] = 100 - (100 / (1 + averageUp[i] / averageDown[i]));
                }
            }

            return rsi[prices.Length - 1];
        }
        private bool IsRSIBullish(decimal rsiValue)
        {
            if (rsiValue >= 70)
            {
                return false; // RSI quá mua, có khả năng giảm giá
            }
            else if (rsiValue <= 30)
            {
                return true; // RSI quá bán, có khả năng tăng giá
            }
            else
            {
                return false; // RSI trung lập, cần kết hợp với các phân tích khác
            }
        }

        //-------------Tính theo StochasticOscillator
        public void AnalyzeStochasticOscillator(int periodK, int periodD)
        {
            var groupedData = stockDataList.GroupBy(x => x.MaChungKhoan);

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa

                // Tính toán SOST
                decimal[] sostK = CalculateSOSTK(prices, periodK);
                decimal[] sostD = CalculateSOSTD(prices, periodK, periodD);

                // Phân tích SOST
                AnalyzeSOST(stockCode, sostK, sostD);
            }
        }

        private decimal[] CalculateSOSTK(decimal[] prices, int periodK)
        {
            if (prices.Length < periodK)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng chu kỳ K");
            }

            decimal[] sostK = new decimal[prices.Length];

            for (int i = periodK - 1; i < prices.Length; i++)
            {
                decimal lowestLow = prices[i - periodK + 1];
                decimal highestHigh = prices[i - periodK + 1];

                for (int j = i - periodK + 2; j <= i; j++)
                {
                    lowestLow = Math.Min(lowestLow, prices[j]);
                    highestHigh = Math.Max(highestHigh, prices[j]);
                }

                if (highestHigh == lowestLow)
                {
                    sostK[i] = 100;
                }
                else
                {
                    sostK[i] = 100 * ((prices[i] - lowestLow) / (highestHigh - lowestLow));
                }
            }

            return sostK;
        }

        private decimal[] CalculateSOSTD(decimal[] prices, int periodK, int periodD)
        {
            if (prices.Length < periodK + periodD)
            {
                throw new ArgumentException("Độ dài mảng giá phải lớn hơn hoặc bằng chu kỳ K + chu kỳ D");
            }

            decimal[] sostK = CalculateSOSTK(prices, periodK);
            decimal[] sostD = new decimal[prices.Length];

            for (int i = periodK + periodD - 1; i < prices.Length; i++)
            {
                decimal sum = 0;

                for (int j = i - periodD + 1; j <= i; j++)
                {
                    sum += sostK[j];
                }

                sostD[i] = sum / periodD;
            }

            return sostD;
        }
        // cách sử dụng
        //List<BieuDoKhoiLuong> stockDataList = LoadStockData(); // Tải dữ liệu giá
        //StochasticOscillatorAnalysis analysis = new StochasticOscillatorAnalysis(stockDataList);
        //analysis.AnalyzeStochasticOscillator(14, 3);
        private void AnalyzeSOST(string stockCode, decimal[] sostK, decimal[] sostD)
        {
            // Phân tích SOST và đưa ra kết luận
            Console.WriteLine("Mã chứng khoán: {0}", stockCode);
            Console.WriteLine("SOST K: {0}", string.Join(", ", sostK));
            Console.WriteLine("SOST D: {0}", string.Join(", ", sostD));

            // Ví dụ: Phân tích đơn giản dựa trên ngưỡng
            const decimal overboughtThreshold = 80;
            const decimal oversoldThreshold = 20;

            // Phân tích SOST K
            for (int i = 0; i < sostK.Length; i++)
            {
                if (sostK[i] >= overboughtThreshold)
                {
                    Console.WriteLine("**Giá đang ở vùng quá mua (SOST K >= {0})**", overboughtThreshold);
                }
                else if (sostK[i] <= oversoldThreshold)
                {
                    Console.WriteLine("**Giá đang ở vùng quá bán (SOST K <= {0})**", oversoldThreshold);
                }
            }

            // Kết hợp SOST K và SOST D để phân tích xu hướng
            for (int i = 0; i < sostK.Length; i++)
            {
                if (sostK[i] >= overboughtThreshold && sostD[i] > sostK[i])
                {
                    Console.WriteLine("**Giá có thể đảo chiều giảm (SOST K quá mua và SOST D cắt xuống)**");
                }
                else if (sostK[i] <= oversoldThreshold && sostD[i] < sostK[i])
                {
                    Console.WriteLine("**Giá có thể đảo chiều tăng (SOST K quá bán và SOST D cắt lên)**");
                }
            }
        }
        
    }
}

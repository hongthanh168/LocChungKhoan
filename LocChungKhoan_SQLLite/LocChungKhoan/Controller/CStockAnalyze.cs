using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
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

        #region "Tìm điểm kiệt cung"
        public List<string> TimDiemKietCung(List<BieuDoKhoiLuong> duLieu)
        {
            List<string> danhSachCoPhieu = new List<string>();
            //danh sách mã chứng khoán
            List<string> maChungKhoans = duLieu.Select(x => x.MaChungKhoan).Distinct().ToList();
            foreach (string maChungKhoan in maChungKhoans)
            {
                // Lấy dữ liệu của mã chứng khoán
                List<BieuDoKhoiLuong> duLieuCoPhieu = duLieu.Where(x => x.MaChungKhoan == maChungKhoan).ToList();
                if (KiemTraDiemKietCung(duLieuCoPhieu))
                {
                    danhSachCoPhieu.Add(maChungKhoan);
                }
            }
            return danhSachCoPhieu;
        }
        public bool KiemTraDiemKietCung(List<BieuDoKhoiLuong> duLieuCoPhieu)
        {
            // Lấy dữ liệu của soNgayPhanTich ngày gần nhất
            List<BieuDoKhoiLuong> duLieuGanNhat = duLieuCoPhieu.OrderByDescending(x => x.Ngay).ToList();
            //tính trung bình khối lượng
            decimal avgVolume = duLieuGanNhat.Average(x => x.KhoiLuong);
            // Duyệt qua dữ liệu từng ngày
            for (int i = 0; i < duLieuGanNhat.Count - 3; i++)
            {
                //ý tưởng là tìm 1 danh sách 3 ngày mà giá giảm liên tục (nến đỏ) và khối lượng thấp
                if (duLieuGanNhat[i].GiaDongCua < duLieuGanNhat[i].GiaMoCua &&
                    duLieuGanNhat[i + 1].GiaDongCua < duLieuGanNhat[i + 1].GiaMoCua &&
                    duLieuGanNhat[i + 2].GiaDongCua < duLieuGanNhat[i + 2].GiaMoCua &&
                    duLieuGanNhat[i].KhoiLuong < avgVolume &&
                    duLieuGanNhat[i + 1].KhoiLuong < avgVolume &&
                    duLieuGanNhat[i + 2].KhoiLuong < avgVolume
                    )
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        
        #region "Tìm biến động khối lượng"        
        public bool KhoiLuongTangKyLuc(List<BieuDoKhoiLuong > data, decimal nguongKhoiLuong = 2.0m, int soLuong=3)
        {
            //ý tưởng là: tìm 1 ngày có khối lượng đạt tỷ lệ nguongKhoiLuong so với ngày trước đó
            //ví dụ ngưỡng là 250% , nếu ngày hôm nay có khối lượng gấp 2.5 lần ngày hôm qua thì xem xét
            //tính tổng những cây nến có khối lượng lớn hơn ngưỡngKhoiLuong, nếu nó > soLuong thì trả về true
            data = data.OrderByDescending(d => d.Ngay).ToList();
            int count = 0;
            for (int i = 0; i < data.Count -2; i++)
            {
                //kiểm tra có gấp đôi ngày trước đó hay không?
                if (data[i].KhoiLuong> data[i+1].KhoiLuong * nguongKhoiLuong)
                {
                    count++;
                }
                if (count >= soLuong)
                {
                    return true;
                }
            }
            return false;
        }
        //lấy cổ phiếu biến động khối lượng
        public List<string> LayCoPhieuBienDongKhoiLuong(List<BieuDoKhoiLuong> listCoPhieu, DateTime ngayTinh, decimal volumeThreshold = 2.0m, int soLuong = 3)
        {
            List<string> result = new List<string>();
            var listKhoiLuong = BieuDoKhoiLuongController.LayKhoiLuongTrungBinh(ngayTinh, 100);
            //join listKhoiLuong với listCoPhieu để lấy thông tin khối lượng
            var temp = from khoiLuong in listKhoiLuong
                       join coPhieu in listCoPhieu on khoiLuong.MaChungKhoan equals coPhieu.MaChungKhoan
                       select new {khoiLuong.MaChungKhoan,khoiLuong.KhoiLuongTB,  coPhieu.Ngay, coPhieu.KhoiLuong, coPhieu.GiaDongCua, coPhieu.GiaMoCua  };
            //group by mã chứng khoán
            var list = temp.GroupBy(s => s.MaChungKhoan)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.ToList() }); 
            foreach (var stock in list)
            {
                //order by date
                var data = stock.Data.OrderByDescending(x => x.Ngay).ToList();
                int i = 0;
                bool kq = false;
                while (i < data.Count - 1 && !kq)
                {
                    //kiểm tra có gấp đôi ngày trước đó hay không?
                    if (data[i].KhoiLuong > data[i].KhoiLuongTB * volumeThreshold && data[i].GiaMoCua < data[i].GiaDongCua)
                    {
                        result.Add(stock.MaChungKhoan);
                        kq = true;
                    }
                    i++;
                }
            }
            return result;
        }

        #endregion

        #region "MACD"
        //================================================Tính theo MACD
        public enum XuHuong
        {
            Tang,
            Giam,
            KhongRoRang
        }
        // Xác định cổ phiếu có khả năng tăng giá dựa trên MACD
        /*
         * 1. Tính đường trung bình động (Moving Average - MA) nhanh (EMA12):
                EMA12 = (Giá đóng cửa hiện tại * (2 / (N + 1))) + (EMA12 của ngày trước * (1 - (2 / (N + 1))))
                N = 12 (số ngày của chu kỳ nhanh)
            2. Tính đường trung bình động chậm (EMA26):
                EMA26 = (Giá đóng cửa hiện tại * (2 / (N + 1))) + (EMA26 của ngày trước * (1 - (2 / (N + 1))))
                N = 26 (số ngày của chu kỳ chậm)
            3. Tính đường MACD:
                MACD = EMA12 - EMA26
            4. Tính đường tín hiệu (Signal Line):
                Signal Line = Trung bình động của MACD trong 9 ngày (EMA9 của MACD)
            5. Tính đường Histogram:
                Histogram = MACD - Signal Line
            Giải thích các thành phần:
                EMA12: Trung bình động mũ của giá đóng cửa trong 12 ngày. Đánh giá xu hướng ngắn hạn.
                EMA26: Trung bình động mũ của giá đóng cửa trong 26 ngày. Đánh giá xu hướng dài hạn.
                MACD: Sự chênh lệch giữa EMA12 và EMA26. Cho biết sức mạnh của xu hướng hiện tại.
                Signal Line: Trung bình động mũ của MACD trong 9 ngày. Giúp xác định điểm mua và điểm bán.
                Histogram: Sự chênh lệch giữa MACD và Signal Line. Cho biết mức độ mạnh yếu của tín hiệu mua/bán.
            Lưu ý:
                Các giá trị của N (12, 26, 9) có thể thay đổi tùy theo nhu cầu của nhà đầu tư.
                MACD là một chỉ báo theo xu hướng, nên hiệu quả của nó phụ thuộc vào xu hướng thị trường.
                Không nên sử dụng MACD như là một chỉ báo duy nhất để đưa ra quyết định đầu tư.
           */
        public List<string> LayCoPhieuTangTheoMACD(List<BieuDoKhoiLuong> duLieu)
        {
            List<string> coPhieuTangGia = new List<string>();
            //danh sách mã chứng khoán
            List<string> maChungKhoans = duLieu.Select(x => x.MaChungKhoan).Distinct().ToList();
            foreach (string maChungKhoan in maChungKhoans)
            {
                // Lấy dữ liệu của mã chứng khoán
                List<BieuDoKhoiLuong> duLieuCoPhieu = duLieu.Where(x => x.MaChungKhoan == maChungKhoan).OrderBy (x => x.Ngay).ToList();

                // Tính MACD cho mã chứng khoán
                XuHuong macd = CalculateMacd(duLieuCoPhieu);

                // Kiểm tra điều kiện MACD tăng và cắt đường tín hiệu
                if (macd == XuHuong.Tang )
                {
                    coPhieuTangGia.Add(maChungKhoan);
                }
            }

            return coPhieuTangGia;
        }

        // Xác định cổ phiếu có khả năng giảm giá dựa trên MACD
        public XuHuong  CalculateMacd(List<BieuDoKhoiLuong> stockData, int shortPeriod = 12, int longPeriod = 26, int signalPeriod = 9)
        {
            //dữ liệu được sắp xếp theo ngày giảm dần
            if (stockData == null || stockData.Count < longPeriod)
                return XuHuong.KhongRoRang ;

            var closingPrices = stockData.Select(x => x.GiaDongCua).ToList();
            var emaShort = CalculateEma(closingPrices, shortPeriod);
            var emaLong = CalculateEma(closingPrices, longPeriod);

            // Tính MACD
            List<decimal> macd = new List<decimal>();
            for (int i = 0; i < stockData.Count; i++)
            {
                macd.Add(emaShort[i] - emaLong[i]);
            }
            var signalLine = CalculateEma(macd, signalPeriod);

            XuHuong trend = CheckTrend(macd, signalLine);
            return trend;
        }
        public  List<decimal> CalculateEma(List<decimal> prices, int period)
        {
            var ema = new List<decimal>();
            decimal multiplier = 2m / (period + 1);
            decimal previousEma = prices.Take(prices.Count - period).Average();

            for (int i = 0; i < prices.Count - period; i++)
            {
                ema.Add(previousEma);
            }

            for (int i = prices.Count - period; i < prices.Count; i++)
            {
                decimal currentEma = (prices[i] - previousEma) * multiplier + previousEma;
                ema.Add(currentEma);
                previousEma = currentEma;
            }

            return ema;
        }
        public XuHuong CheckTrend(List<decimal> macdLine, List<decimal> signalLine)
        {
            if (macdLine == null || signalLine == null || macdLine.Count != signalLine.Count)
                return XuHuong.KhongRoRang;

            var lastIndex = macdLine.Count - 1;

            //kiểm tra 2 đường này có cắt nhau không?
            //cắt nhau từ dưới lên thì xu hướng tăng, cắt nhau từ trên xuống thì xu hướng giảm
            //cắt nhau từ dưới lên

            for (int i = lastIndex; i > macdLine.Count -26; i--)
            {
                //cắt nhau từ dưới lên
                if (macdLine[i] > signalLine[i] && macdLine[i - 1] < signalLine[i - 1])
                {
                    return XuHuong.Tang;
                }
                //cắt nhau từ trên xuống
                if (macdLine[i] < signalLine[i] && macdLine[i - 1] > signalLine[i - 1])
                {
                    return XuHuong.Giam;
                }
            }
             return XuHuong.KhongRoRang ;
        }
        #endregion

        #region Tính theo RSI --đã test ổn
        public List<string> LayCoPhieuTheoRSI(List<BieuDoKhoiLuong> duLieu, int rsi_Min, int rsi_Max, int soNgay = 14)
        {
            List<string> danhSachCoPhieu = new List<string>();
            //phải sắp xếp dữ liệu theo ngày tăng dần trước khi nhóm
            var groupedData = duLieu.OrderBy(x => x.Ngay).GroupBy(x => x.MaChungKhoan);

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa

                decimal rsi = CalculateRsi(prices, soNgay); // Chu kỳ RSI 14
                //XuHuong isBullish = IsRSIBullish(rsi);
                if (rsi>=rsi_Min && rsi<=rsi_Max)
                {
                    danhSachCoPhieu.Add(stockCode);
                }
            }
            return danhSachCoPhieu;
        }
        public decimal CalculateRsi(decimal[] prices, int period = 14)
        {
            if (prices == null || prices.Length < period)
                return -1;

            var gains = new List<decimal>();
            var losses = new List<decimal>();

            for (int i = 1; i < prices.Length; i++)
            {
                var change = prices[i] - prices[i - 1];
                if (change > 0)
                    gains.Add(change);
                else
                    losses.Add(Math.Abs(change));
            }

            decimal averageGain = gains.Count > 0 ? gains.Average() : 0;
            decimal averageLoss = losses.Count > 0 ? losses.Average() : 0;

            decimal rs = averageLoss == 0 ? 0 : averageGain / averageLoss;
            decimal rsi = 100 - (100 / (1 + rs));

            return rsi;
        }
        public XuHuong IsRSIBullish(decimal rsiValue)
        {
            if (rsiValue >= 70)
            {
                return XuHuong.Giam; // RSI quá mua, có khả năng giảm giá
            }
            else if (rsiValue <= 30 && rsiValue > 0)
            {
                return XuHuong.Tang; // RSI quá bán, có khả năng tăng giá
            }
            else
            {
                return XuHuong.KhongRoRang; // RSI trung lập, cần kết hợp với các phân tích khác
            }
        }

        #endregion

        #region Tính theo StochasticOscillator
        public List<string> LayCoPhieuTangTheoStochasticOscillator(List<BieuDoKhoiLuong> duLieu, int periodK=14, int periodD=3)
        {
            List<string> danhSachCoPhieu = new List<string>();  
            var groupedData = duLieu.GroupBy(x => x.MaChungKhoan);

            foreach (var group in groupedData)
            {
                string stockCode = group.Key;
                var prices = group.Select(x => x.GiaDongCua).ToArray(); // Lấy giá đóng cửa
                if (prices.Length >= periodK +periodD)
                {
                    // Tính toán SOST
                    decimal[] sostK = CalculateSOSTK(prices, periodK);
                    decimal[] sostD = CalculateSOSTD(prices, periodK, periodD);

                    // Phân tích SOST
                    XuHuong xh = AnalyzeSOST(sostK, sostD);
                    if (xh == XuHuong.Tang)
                    {
                        danhSachCoPhieu.Add(stockCode);
                    }
                }
            }
            return danhSachCoPhieu;
        }

        public decimal[] CalculateSOSTK(decimal[] prices, int periodK)
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

        public decimal[] CalculateSOSTD(decimal[] prices, int periodK, int periodD)
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

        public XuHuong AnalyzeSOST(decimal[] sostK, decimal[] sostD)
        {            
            // Ví dụ: Phân tích đơn giản dựa trên ngưỡng
            const decimal overboughtThreshold = 80;
            const decimal oversoldThreshold = 20;

            //// Phân tích SOST K
            //for (int i = 0; i < sostK.Length; i++)
            //{
            //    if (sostK[i] >= overboughtThreshold)
            //    {
            //        Console.WriteLine("**Giá đang ở vùng quá mua (SOST K >= {0})**", overboughtThreshold);
            //    }
            //    else if (sostK[i] <= oversoldThreshold)
            //    {
            //        Console.WriteLine("**Giá đang ở vùng quá bán (SOST K <= {0})**", oversoldThreshold);
            //    }
            //}

            // Kết hợp SOST K và SOST D để phân tích xu hướng
            for (int i = 0; i < sostK.Length; i++)
            {
                if (sostK[i] >= overboughtThreshold && sostD[i] > sostK[i])
                {
                    return XuHuong.Giam;
                    //Console.WriteLine("**Giá có thể đảo chiều giảm (SOST K quá mua và SOST D cắt xuống)**");
                }
                else if (sostK[i] <= oversoldThreshold && sostD[i] < sostK[i])
                {
                    return XuHuong.Tang;
                    //Console.WriteLine("**Giá có thể đảo chiều tăng (SOST K quá bán và SOST D cắt lên)**");
                }
            }
            return XuHuong.KhongRoRang;
        }
        #endregion

        #region Đồ thị nến Nhật
        const decimal TY_LE_NEN_DAI = 0.5m; //nến dài là độ thay đổi giá đóng cửa so với giá mở cửa lớn hơn 50%
        const decimal TY_LE_NEN_CUC_NGAN = 0.1m; //nến doji là độ thay đổi giá đóng cửa so với giá mở cửa nhỏ hơn 10%
        enum LoaiThanNen
        {
            Dai,
            Ngan,
            CucNgan
        }
        public enum MauHinhNen
        {
            BullishEngulfing,
            Hammer,
            PiercingLine,
            MorningStar,
        }
        public List<string> LayCoPhieuTheoNenNhat(List<BieuDoKhoiLuong> listCoPhieu, MauHinhNen mauHinh)
        {
            List<string> result = new List<string>();
            // Lấy dữ liệu của các cổ phiếu trong 'days' ngày gần nhất
            var groupedStocks = listCoPhieu.GroupBy(s => s.MaChungKhoan)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.ToList() });
            foreach (var stock in groupedStocks)
            {
                if (KiemTraMauHinhNenNhat(stock.Data, mauHinh))
                {
                    result.Add(stock.MaChungKhoan);
                }
            }
            return result;
        }
        public bool KiemTraMauHinhNenNhat(List<BieuDoKhoiLuong> data, MauHinhNen mauHinh)
        {
            //sắp xếp dữ liệu theo ngày giảm dần
            data = data.OrderByDescending(x => x.Ngay).ToList();
            bool kq = false;
            for (int i = 0; i < data.Count - 1; i++)
            {
                switch (mauHinh)
                {
                    case MauHinhNen.BullishEngulfing:
                        if (i + 1 < data.Count-1)
                        {
                            kq= KiemTraBullishEngulfing(data[i], data[i + 1]);
                        }
                        break;
                    case MauHinhNen.Hammer:
                        if (i + 2 < data.Count-1)
                        {
                            kq = KiemTraHammerTangGia(data[i], data[i + 1], data[i + 2]);
                        }
                        break;
                    case MauHinhNen.PiercingLine:
                        if (i + 1 < data.Count-1)
                        {
                            kq = KiemTraPiercingLine(data[i], data[i + 1]);
                        }
                        break;
                    case MauHinhNen.MorningStar:
                        if (i + 1 < data.Count-1 && i>0)
                        {
                            //cây nến sắp xếp theo chiều tăng dần thời gian
                            kq = KiemTraMorningStar(data[i+1], data[i], data[i-1]);
                        }
                        break;
                }
                if ( kq)
                {
                    return true;
                }
            }
            return kq;
        }

        //duLieuCoPhieu: dữ liệu của 1 mã cổ phiếu
        //Thân nến ngắn, bóng nến dưới dài, bóng nến trên ngắn.Cho thấy sức mua mạnh mẽ đã đẩy giá lên sau khi giảm xuống mức thấp.
        public bool KiemTraHammerTangGia(BieuDoKhoiLuong nenHienTai, BieuDoKhoiLuong nenNgayHomQua, BieuDoKhoiLuong nenNgayHomKia)
        {
            return //ĐK1: 2 nen trước đó giảm giá
                !KiemTraNenXanh(nenNgayHomQua) && !KiemTraNenXanh(nenNgayHomKia) &&
                //ĐK2: thân nến ngắn tức là giá mở cửa và giá đóng cửa gần nhau
                KiemTraThanNen(nenHienTai) == LoaiThanNen.Ngan  &&
                //ĐK3: búa ngược hay xuôi đều được
                NenHammer(nenHienTai) 
                ;
        }
        //Nến xanh (Bullish) bao trọn nến đỏ (Bearish) trước đó. Cho thấy sức mua mạnh mẽ đã đẩy giá lên và vượt qua mức kháng cự.
        //Nến đầu tiên là một nến giảm và có thể là một nến Doji
        //Nến kế tiếp là một nến tăng mạnh và có thân nến dài hơn nến phía trước, phần thân bao trùm toàn bộ cây nến đỏ phía trước.
        //Nến Tăng có giá ở đáy nến thấp hơn giá đóng cửa của nến giảm trước nó, Đỉnh của nến tăng có giá đóng cửa cao hơn giá mở cửa của cây nến giảm trước nó.
        //Không cần quan tâm đến phần bóng nến trên hoặc dưới
        public bool KiemTraBullishEngulfing(BieuDoKhoiLuong nenHienTai, BieuDoKhoiLuong nenTruoc)
        {
            decimal thanNenHienTai = Math.Abs(nenHienTai.GiaDongCua - nenHienTai.GiaMoCua);
            decimal thanNenTruocDo = Math.Abs(nenTruoc.GiaDongCua - nenTruoc.GiaMoCua);
            if (//kiểm tra nến hiện tại là nến xanh, nến trước đó là nến đỏ
                nenHienTai.GiaDongCua > nenHienTai.GiaMoCua && nenTruoc.GiaDongCua < nenTruoc.GiaMoCua &&
                //nến xanh bao trọn nến đỏ tức là thân nến xanh lớn hơn thân nến đỏ
                thanNenHienTai > thanNenTruocDo   &&
                //giá đóng cửa của nến xanh cao hơn giá mở cửa của nến đỏ
                nenHienTai.GiaDongCua > nenTruoc.GiaMoCua && 
                //giá mở cửa của nến xanh thấp hơn giá đóng cửa của nến đỏ
                nenHienTai.GiaMoCua < nenTruoc.GiaDongCua
                )
            {
                return true;
            }
            return false;
        }
        //kiểm tra nến thân dài hay thân ngắn
        private bool NenThanDai(BieuDoKhoiLuong nen)
        {
            //thân nến dài: giá đóng cửa cao hơn giá mở cửa đáng kể
            decimal thanNen = Math.Abs(nen.GiaDongCua - nen.GiaMoCua);
            return thanNen > TY_LE_NEN_DAI  * nen.GiaMoCua;
        }
        public bool NenDoj(BieuDoKhoiLuong nen)
        {
            //nến doji là nến mà giá mở cửa và giá đóng cửa gần nhau, do đó thân nến cực ngắn, có hình như dấu + hoặc dấu -.
            if (KiemTraThanNen(nen) == LoaiThanNen.CucNgan )
            {
                return true;
            }
            return false;
        }
        public bool NenHammer(BieuDoKhoiLuong nen)
        {
            //nến hammer là nến mà có thân ngắn, bóng nến đầu này dài gấp nhiều lần bóng nến đầu kia
            if (KiemTraThanNen(nen) == LoaiThanNen.Ngan && (BongNenTren(nen) > 2 * BongNenDuoi(nen) || BongNenDuoi(nen) > 2* BongNenTren (nen)))
            {
                return true;
            }
            return false;
        }
        private  decimal ThanNen (BieuDoKhoiLuong nen)
        {
            return Math.Abs(nen.GiaDongCua - nen.GiaMoCua);
        }
        private  LoaiThanNen KiemTraThanNen(BieuDoKhoiLuong nen)
        {
            decimal thanNen = ThanNen(nen);
            if (thanNen > TY_LE_NEN_DAI * nen.GiaMoCua)
            {
                return LoaiThanNen.Dai;
            }
            if (thanNen < TY_LE_NEN_CUC_NGAN * nen.GiaMoCua)
            {
                return LoaiThanNen.CucNgan;
            }
            return LoaiThanNen.Ngan;
        }
        private  bool KiemTraNenXanh(BieuDoKhoiLuong nen)
        {
            //nếu trả về true là nến xanh, false là nến đỏ
            return nen.GiaDongCua > nen.GiaMoCua;
        }
        private  decimal BongNenTren(BieuDoKhoiLuong nen)
        {
            if (KiemTraNenXanh(nen))
            {
                return nen.GiaCaoNhat - nen.GiaDongCua;
            }
            else
            {
                return nen.GiaCaoNhat - nen.GiaMoCua;
            }
        }
        private  decimal BongNenDuoi(BieuDoKhoiLuong nen)
        {
            if (KiemTraNenXanh(nen))
            {
                return nen.GiaMoCua - nen.GiaThapNhat;
            }
            else
            {
                return nen.GiaDongCua - nen.GiaThapNhat;
            }
        }
        //Nến xanh (Bullish) với giá mở cửa thấp hơn giá đóng cửa của nến đỏ (Bearish) trước đó. Cho thấy sức mua mạnh mẽ đã đẩy giá lên và vượt qua mức kháng cự.
        //Piercing Pattern thường xuất hiện ở cuối của xu hướng giảm giá.
        //Piercing Pattern là một cụm 2 nến liên tiếp nhau.
        //Cây nến thứ nhất là một nến đỏ lớn, trong khi đó cây nến thứ hai là một nến xanh lớn với chiều dài thân nến tối thiểu bằng 50% cây nến thứ nhất.
        //Cây nến thứ nhất và cây nến thứ hai nên có kích cỡ tương đồng với các nến trước đó (trong khoảng 10 – 15 nến).
        public  bool KiemTraPiercingLine(BieuDoKhoiLuong nenHienTai, BieuDoKhoiLuong nenTruoc)
        {
            return (!KiemTraNenXanh(nenTruoc ) && KiemTraThanNen(nenTruoc) == LoaiThanNen.Dai &&
                     KiemTraNenXanh(nenHienTai) && KiemTraThanNen(nenHienTai) == LoaiThanNen.Dai &&
                   nenHienTai.GiaDongCua > 0.5m * ThanNen(nenTruoc ) + nenTruoc.GiaDongCua ) ;
        }
        
        public  bool KiemTraMorningStar(BieuDoKhoiLuong nen1, BieuDoKhoiLuong nen2, BieuDoKhoiLuong nen3)
        {
            //– Cây nến thứ nhất của mô hình luôn là nến giảm(nến đỏ) với phần thân tương đối lớn. Nến này có phần thân càng dài càng tốt.
            //– Cây nến thứ hai là có thể là nến xanh hoặc nến đỏ.Nến có phần thân nhỏ và thường thuộc dạng nến Doji hoặc Spinning top.
            //– Cây nến thứ 3 bắt buộc là nến tăng mạnh(nến xanh) với phần thân lớn, có chiều dài tối thiểu phải bằng ½ đến ¾ cây nến thứ 1.
            //– Nếu khoảng trống(GAP) giữa cây nến thứ 2 với hai nến còn lại càng lớn thì mô hình càng hiệu quả.
            return //DK1: cây nến là nến đỏ với thân lớn
                !KiemTraNenXanh(nen1) && NenThanDai(nen1) &&
                //DK2: cây nến giữa là nến doji hoặc spinning top
                NenDoj(nen2) &&
                //DK3: cây nến sau là nến xanh với thân lớn
                KiemTraNenXanh(nen3) && NenThanDai(nen3) && ThanNen(nen3) >= 0.5m * ThanNen(nen1)
                   ;
        }
        #endregion

        #region "VSA"
        public static decimal SMA(List<BieuDoKhoiLuong> duLieu, int viTri, int heSo)
        {
            if (duLieu.Count < heSo)
            {
                return 0;
            }
            //dữ liệu đã đc sắp xếp giảm dần theo ngày
            //lấy số lượng dữ liệu cần tính
            var data = duLieu.Skip(viTri).Take(heSo).ToList();
            decimal sma = data.Average(x => x.GiaDongCua);
            return sma;
        }
        //VSA: Volume Spread Analysis
        public List<string> LayCoPhieuTheoPivotPocket(List<BieuDoKhoiLuong> duLieu, decimal priceIncrease, int soNgayXet = 1)
        {
            List<string> danhSachCoPhieu = new List<string>();

            //group by mã chứng khoán
            //lỏng hơn trên TradingView do chưa xét các đường MA50, MA200
            var list = duLieu.GroupBy(s => s.MaChungKhoan)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.ToList() });
            foreach (var stock in list)
            {  
                //với mỗi 1 ngày, ta xét giá trị trong 10 ngày trước đó
                //do đó ta lấy sẵn dữ liệu của ngày nhỏ nhất + 10 ngày trước đó
                var maxDate = stock.Data.Max(x => x.Ngay);
                var data = BieuDoKhoiLuongController.GetAllByDaysAndMaCK(soNgayXet + 10, maxDate, stock.MaChungKhoan);
                data = data.OrderByDescending(x => x.Ngay).ToList ();
                int i = 0;
                bool kq = false;
                while (i < soNgayXet && !kq)
                {
                    //tính MA10
                    decimal ma10 = SMA(data, i, 10);
                    //lấy giá trị max volume của các nến đỏ trước nến hiện thời 10 nến
                    var temp = data.Skip (i+1).Take(10).ToList ();
                    decimal maxVolume = Decimal.MaxValue;
                    try
                    {
                        maxVolume = temp.Where(x => x.GiaMoCua > x.GiaDongCua).Max(x => x.KhoiLuong);
                    }
                    catch
                    {

                    }
                        
                    //kiểm tra khối lượng có tăng mạnh và giá đóng cửa tăng mạnh?
                    if (data[i].GiaDongCua > ma10 &&  data[i].KhoiLuong > maxVolume && data[i].GiaDongCua > data[i].GiaMoCua * (1+priceIncrease/100.0m))
                    {
                        danhSachCoPhieu.Add(stock.MaChungKhoan);
                        kq = true;
                    }
                    i++;
                }
            }
            return danhSachCoPhieu;
        }
        public List<string> LayCoPhieuTheoNoSupplyBar(List<BieuDoKhoiLuong> duLieu, decimal priceDifferent, int soNgayXet =5)
        {
            // Hàm kiểm tra nến No Supply, xuất hiện sau 1 phiên điều chỉnh
            //+ thân nến nhỏ (chênh lệch < 1% so với giá mở cửa)
            //+ volume nhỏ hơn 2 cây nến trước đó.
            //+ Râu nến hướng xuống dưới và không quá dài
            //Các nến trước đó vol không cao và là giảm cũng ko quá sâu
            //tính ra râu nến trên và râu nến dưới
            List<string> danhSachCoPhieu = new List<string>();

            //group by mã chứng khoán
            var list = duLieu.GroupBy(s => s.MaChungKhoan)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.ToList() });
            foreach (var stock in list)
            {
                //order by date
                var data = stock.Data.OrderByDescending(x => x.Ngay).ToList();
                int i = 0;
                bool kq = false;
                while (i < soNgayXet && i <data.Count-2 && !kq)
                {
                    decimal rauNenTren = data[i].GiaCaoNhat - Math.Max(data[i].GiaDongCua, data[i].GiaMoCua);
                    decimal rauNenDuoi = Math.Min(data[i].GiaDongCua, data[i].GiaMoCua) - data[i].GiaThapNhat;
                    decimal thanNen = Math.Abs(data[i].GiaDongCua - data[i].GiaMoCua);
                    if (data[i].KhoiLuong < data[i+1].KhoiLuong && data[i].KhoiLuong< data[i+2].KhoiLuong && rauNenDuoi> rauNenTren && thanNen < priceDifferent/100.0m * data[i].GiaMoCua && rauNenDuoi < 2*thanNen)
                    {
                        danhSachCoPhieu.Add(stock.MaChungKhoan);
                        kq = true;
                    }
                    i++;
                }
            }
            return danhSachCoPhieu;
        }
        #endregion

        #region "Hàm tự chế"
        public List<string> LayCoPhieuManh(List<BieuDoKhoiLuong> duLieu, int soNgayXet = 5)
        {
            //ý tưởng là tìm cổ phiếu mà ngày vnindex giảm nhưng nó vẫn xanh, mà xanh quyết liệt
            //giá đóng cửa > giá mở cửa và đóng cửa ở nửa thân trên của nến
            //chưa hoàn thiện
            List<string> danhSachCoPhieu = new List<string>();
            //group by mã chứng khoán
            var list = duLieu.GroupBy(s => s.MaChungKhoan)
                .Select(g => new { MaChungKhoan = g.Key, Data = g.ToList() });
            foreach (var stock in list)
            {
                //order by date
                var data = stock.Data.OrderByDescending(x => x.Ngay).ToList();
                int i = 0;
                bool kq = false;
                while (i < soNgayXet && !kq)
                {
                    if (data[i].GiaDongCua > data[i + 1].GiaDongCua)
                    {
                        danhSachCoPhieu.Add(stock.MaChungKhoan);
                        kq = true;
                    }
                    i++;
                }
            }
            return danhSachCoPhieu;
        }
        #endregion
    }
}

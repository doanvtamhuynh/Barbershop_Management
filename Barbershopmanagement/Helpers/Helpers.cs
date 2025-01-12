namespace BarbershopManagement.Helpers
{
    public class Helpers
    {
        public string tongThoiGian(int soPhut)
        {
            string ketqua = "";
            int soGio = 0;
            if (soPhut < 60)
            {
                ketqua = soPhut.ToString() + " phút";
            }
            else
            {
                while (soPhut >= 60)
                {
                    soGio++;
                    soPhut -= 60;
                }
                if (soPhut == 0)
                {
                    ketqua = soGio.ToString() + " giờ";
                }
                else
                {
                    ketqua = soGio.ToString() + ":" + soPhut.ToString() + " giờ";
                }
            }
            return ketqua;
        }
    }
}
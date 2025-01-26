using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EasyModern.Core.Utils
{
    public class PEMutator
    {
        public static bool MutatePE()
        {
            try
            {
                string originalExe = Application.ExecutablePath; //Assembly.GetExecutingAssembly().Location

                Random random = new Random();

                int RandomLength = random.Next(5, 13);
                string randomName = GenerateRandomString(RandomLength) + ".exe";
                string newExe = Path.Combine(Path.GetDirectoryName(originalExe), randomName);

                byte[] oldExeBytes = File.ReadAllBytes(originalExe);
                int second = DateTime.Now.Second;
                byte[] randomBytes = Encoding.UTF8.GetBytes(GenerateRandomString(second));
                byte[] mutatedArray = ArrayConcat(oldExeBytes, randomBytes);

                File.WriteAllBytes(newExe, mutatedArray);

                Process.Start(newExe);

                string cmdArgs = $"/C timeout 5 && del \"{originalExe}\"";

                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = cmdArgs,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error mutating executable:" + ex.Message);
                return false;
            }
        }

        private static byte[] ArrayConcat(byte[] x, byte[] y)
        {
            byte[] newx = new byte[x.Length + y.Length];
            x.CopyTo(newx, 0);
            y.CopyTo(newx, x.Length);
            return newx;
        }

        /// <summary>
        /// Calcula e imprime por consola el MD5 y SHA256 del archivo indicado.
        /// </summary>
        /// <param name="filePath">La ruta completa del archivo a analizar.</param>
        public static void CheckHash(string filePath)
        {
            try
            {
                // MD5
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var md5Hash = md5.ComputeHash(stream);
                        string md5Hex = BitConverter
                            .ToString(md5Hash)
                            .Replace("-", "")
                            .ToLowerInvariant();
                        Console.WriteLine($"MD5     : {md5Hex}");
                    }
                }

                // SHA256
                using (var sha256 = SHA256.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var sha256Hash = sha256.ComputeHash(stream);
                        string sha256Hex = BitConverter
                            .ToString(sha256Hash)
                            .Replace("-", "")
                            .ToLowerInvariant();
                        Console.WriteLine($"SHA256  : {sha256Hex}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error calculating hash: " + ex.Message);
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[rnd.Next(chars.Length)]);
            }
            return sb.ToString();
        }
    }

}

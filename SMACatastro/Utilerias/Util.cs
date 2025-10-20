using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilerias
{
    public class Util
    {
        static string hash { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        public void soloLetras(KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        public void soloNumero(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        public string CodificarMd5(string Passcode)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(Passcode);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    Passcode = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return Passcode;
        }

        public string DeCodificarMd5(string Passcode)
        {
            byte[] data = Convert.FromBase64String(Passcode); //
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    Passcode = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return Passcode;
        }

        public string CodeBase64(string Passcode)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(Passcode);

            return Convert.ToBase64String(encbuff);
        }
        
        public string DeCodeBase64(string Passcode)
        {
            byte[] decbuff = Convert.FromBase64String(Passcode);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        public string scm(string valor)
        {
            // Si la cadena no está vacía, la envuelve en comillas simples
            if (!string.IsNullOrEmpty(valor))
            {
                return "'" + sol(valor) + "'";
            }
            else
            {
                return "null";
            }
        }
        public string sol(string s)
        {
            // Duplica las comillas simples en la cadena
            return s.Replace("'", "''");
        }
        public void Alfanumerico(KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
    }
}

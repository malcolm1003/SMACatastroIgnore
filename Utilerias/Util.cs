using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;


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

        public void TextBox_Enter(object sender, EventArgs e)
        {

            TextBox currentTextBox = (TextBox)sender;
            currentTextBox.BackColor = System.Drawing.Color.Yellow;
            TextBox txt = (TextBox)sender;
            txt.SelectAll();

            // Opcional: Restaurar color cuando pierde el foco
            currentTextBox.Leave += (s, args) =>
            {
                currentTextBox.BackColor = System.Drawing.SystemColors.Window;
            };
        }

        public void RadioButon_Enter(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            radioButton.BackColor = System.Drawing.Color.Yellow;
            radioButton.ForeColor = System.Drawing.Color.Black;

            // Opcional: Restaurar color cuando pierde el foco
            radioButton.Leave += (s, args) =>
            {
                radioButton.BackColor = System.Drawing.Color.FromArgb(55, 61, 69);
                radioButton.ForeColor = System.Drawing.Color.White;
            };
        }

        public void Cbo_Box_Enter(object sender, EventArgs e)
        {
            ComboBox currentCboBox = (ComboBox)sender;
            currentCboBox.BackColor = System.Drawing.Color.Yellow;

            // Opcional: Restaurar color cuando pierde el foco
            currentCboBox.Leave += (s, args) =>
            {
                currentCboBox.BackColor = System.Drawing.SystemColors.Window;
            };
        }

        public void CapturarPantallaConInformacion(Exception ex)
        {
            try
            {
                string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

                if (!Directory.Exists(carpetaCapturas))
                {
                    Directory.CreateDirectory(carpetaCapturas);
                }

                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

                        // Agregar información detallada del error en la imagen
                        using (Font font = new Font("Arial", 11))
                        using (Brush brush = new SolidBrush(Color.DarkRed))
                        {
                            string infoError = $"EXCEPCIÓN CAPTURADA - {DateTime.Now}\n" +
                                             $"Mensaje: {ex.Message}\n" +
                                             $"Tipo: {ex.GetType().Name}\n" +
                                            // $"Usuario: {Program.acceso_usuario}\n" +
                                             $"Proceso: Cambio Clave Catastral\n" +
                                             $"Origen: {ex.Source}\n" +
                                             $"Stack Trace: {ex.StackTrace?.Substring(0, Math.Min(200, ex.StackTrace.Length))}...";

                            // Fondo semitransparente para el texto
                            using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
                            {
                                g.FillRectangle(backgroundBrush, 10, 10, 650, 200);
                            }

                            g.DrawString(infoError, font, brush, new RectangleF(15, 15, 650, 190));
                        }
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string nombreArchivo = $"error_excepcion_{timestamp}.png";
                    string filePath = Path.Combine(carpetaCapturas, nombreArchivo);

                    bitmap.Save(filePath, ImageFormat.Png);

                    Console.WriteLine($"Captura de excepción guardada: {filePath}");
                }
            }
            catch (Exception captureEx)
            {
                Console.WriteLine($"Error al capturar pantalla de excepción: {captureEx.Message}");
            }
        }



    }
}

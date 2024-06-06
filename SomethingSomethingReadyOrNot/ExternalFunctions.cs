using SomethingSomethingReadyOrNot.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SomethingSomethingReadyOrNot
{
    public class ExternalFunctions : Form
    {
        public static Response OverrideImage()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog()
                {
                    FileName = "Select a Image file",
                    Filter = "Image files (*.png)|*.png|All files (*.*)|*.*",
                    Title = "Open Image file"
                };
                openFileDialog1.ShowDialog();
                Bitmap img;

                img = new Bitmap(openFileDialog1.FileName.ToString());
                float width = 400;
                float height = 600;
                float scale = Math.Min(width / img.Width, height / img.Height);
                int scaleWidth = (int)(img.Width * scale);
                int scaleHeight = (int)(img.Height * scale);
                img = new Bitmap(img, new Size(scaleWidth, scaleHeight));
                Byte[] title = ImageToByte(img);

                /* string path = AppDomain.CurrentDomain.BaseDirectory;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < title.Length; i++)
                    {
                        if (i != title.Length - 1)
                        {
                            sb.Append(title[i].ToString() + ";");
                        }
                        else
                        {
                            sb.Append(title[i].ToString());
                        }
                    }
                    FileSomething.ImageBytes = sb.ToString();
                    string a = sb.ToString();
                    fs.Write(Encoding.ASCII.GetBytes(a), 0, a.Length);
                }*/
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < title.Length; i++)
                {
                    if (i != title.Length - 1)
                    {
                        sb.Append(title[i].ToString() + ";");
                    }
                    else
                    {
                        sb.Append(title[i].ToString());
                    }
                }
                FileSomething.ImageBytes = sb.ToString();
                return ResponseFactory<bool>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateFailureResponse(ex);
            }

            /*Image image;

            List<string> lines = new List<string>();
            string test = FileSomething.ImageBytes;
            String[] Words = test.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            using (StreamReader sr = File.OpenText(test))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    lines.Add(s);
                }
            }
            List<Byte> bytesList = new List<Byte>();
            for (int i = 0; i < Words.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(Words[i]))
                {
                    bytesList.Add((byte)int.Parse(Words[i]));
                }
            }
            Byte[] bytes = bytesList.ToArray();
            image = ByteToImage(bytes);
            PictureBox pb1 = new PictureBox();
            Form sampleForm = new Form();
            PictureBox pctBox = new PictureBox();
            pctBox.Image = image;
            pctBox.Dock = DockStyle.Fill;
            sampleForm.Controls.Add(pctBox);
            sampleForm.ShowDialog();*/
        }

        public static SingleResponse<string> OpenFileDialogForm()
        {
            try
            {
                FolderBrowserDialog diag = new FolderBrowserDialog();
                diag.ShowDialog();
                return ResponseFactory<string>.CreateSuccessItemResponse(diag.SelectedPath);
            }
            catch (Exception ex)
            {
                return ResponseFactory<string>.CreateFailureItemResponse(ex);
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static SingleResponse<Image> ByteToImage(Byte[] img)
        {
            try
            {
                Image image;
                using (var ms = new MemoryStream(img))
                {
                    image = Image.FromStream(ms);
                }
                return ResponseFactory<Image>.CreateSuccessItemResponse(image);
            }
            catch (Exception ex)
            {
                return ResponseFactory<Image>.CreateFailureItemResponse(ex);
            }
        }

        /* public static KeyValuePair<string, string> SplitToKeyValue(string text)
         {
                 Regex p = new Regex(@"^(\w+)\s+(.*)$");
                 Match m = p.Match(text);
                 return new KeyValuePair<string, string>(m.Groups[1].Value, m.Groups[2].Value);
         }*/

        private static Response OverrideFile()
        {
            string path2 = FileSomething.Path + @"\ReadyOrNot";
            if (Directory.Exists(path2))
            {
                string newPath = FileSomething.Path + @"\ReadyOrNot\Content\Splash\Splash.bmp";
                File.Delete(newPath);
                using (FileStream fs = File.Create(newPath))
                {
                    // Add some information to the file.
                    string test = FileSomething.ImageBytes;
                    String[] Words = test.Split(new string[] { FileSomething.StringSeparator }, StringSplitOptions.None);
                    List<Byte> bytesList = new List<Byte>();
                    for (int i = 0; i < Words.Length - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(Words[i]))
                        {
                            bytesList.Add((byte)int.Parse(Words[i]));
                        }
                    }
                    Byte[] bytes = bytesList.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
                return ResponseFactory<string>.CreateSuccessResponse();
            }
            else
            {
                return ResponseFactory<string>.CreateFailureResponse("Path doesn't exist");
            }
        }

        public static Response OverrideSplashScreen()
        {
            try
            {
                if (!FileSomething.HasFoundPath)
                {
                    SingleResponse<string> response = OpenFileDialogForm();
                    if (response.HasSuccess)
                    {
                        FileSomething.HasFoundPath = true;
                        FileSomething.Path = response.Item;
                        return OverrideFile();
                    }
                    else
                    {
                        return ResponseFactory<string>.CreateFailureResponse("Path doesn't exist");
                    }
                }
                else
                {
                    return OverrideFile();
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateFailureResponse(ex);
            }
        }
    }
}
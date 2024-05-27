using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using UtfUnknown;



namespace XMLFileSelector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelectXML_Click(object sender, EventArgs e)
        {
            xmlFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            xmlFileDialog.Title = "Select an XML File";

            if (xmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = xmlFileDialog.FileName;
                try
                {
                    // Dosyanın orijinal kodlamasını tespit edip içeriği oku
                    using (FileStream fileStream = File.OpenRead(filePath))
                    using (StreamReader reader = new StreamReader(fileStream, true))
                    {
                        var encoding = reader.CurrentEncoding; // Orijinal kodlamayı al
                        string content = reader.ReadToEnd(); // Dosya içeriğini oku

                        // UTF-8 olarak içeriği yeniden kodla ve RichTextBox'a yükle
                        byte[] bytes = encoding.GetBytes(content);
                        byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, bytes);
                        string utf8String = Encoding.UTF8.GetString(utf8Bytes);

                        // UTF-8 string'i XDocument olarak parse et ve RichTextBox'a yaz
                        XDocument doc = XDocument.Parse(utf8String);
                        rtbDisplayXML.Text = doc.ToString();
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("XML parsing error: " + ex.Message, "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void DisplaySelectedTags(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            string[] tagsToDisplay = { "PartyIdentification", "PartyName", "Person", "PartyTaxScheme", "Sender" };

            var selectedContent = tagsToDisplay.SelectMany(tag =>
                doc.Descendants(tag).Select(element => element.ToString()))
                .Aggregate((current, next) => current + "\n" + next);

            rtbDisplayXML.Text = selectedContent;
        }

        private bool CheckTCKN(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);
                var idElement = doc.Descendants("ID").FirstOrDefault(el => (string)el.Attribute("schemeID") == "TCKN");
                if (idElement != null)
                {
                    string tckn = idElement.Value;
                    if (tckn.Length != 11)
                    {
                        MessageBox.Show("TC Numarası 11 haneli değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true; // Yine de içeriği göster
                    }
                }
                return true; // Her şey düzgünse içeriği göster
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("XML dosyası hatalı: " + ex.Message, "XML Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Hata varsa içeriği gösterme
            }
        }

        private void BtnSaveXML_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbDisplayXML.Text) && !string.IsNullOrEmpty(xmlFileDialog.FileName))
            {
                try
                {
                    XDocument doc = XDocument.Parse(rtbDisplayXML.Text);
                    // StreamWriter'ı UTF-8 kodlaması ile kullan
                    using (StreamWriter writer = new StreamWriter(xmlFileDialog.FileName, false, Encoding.UTF8))
                    {
                        doc.Save(writer);
                        MessageBox.Show("XML file has been saved successfully in UTF-8 format.", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("XML parsing error: " + ex.Message, "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No file is loaded or text box is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // RichTextBox'ın içeriğini temizle
            rtbDisplayXML.Clear();

            // OpenFileDialog'ın seçili dosyasını sıfırla
            xmlFileDialog.FileName = string.Empty;

            // Kullanıcıya bilgi ver
            MessageBox.Show("Yeni bir dosya seçebilirsiniz.", "Yenilendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnConvertToUTF8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(xmlFileDialog.FileName))
            {
                try
                {
                    string filePath = xmlFileDialog.FileName;
                    byte[] fileContentBytes = File.ReadAllBytes(filePath);

                    // Kodlama tespiti yap
                    var detectionResult = CharsetDetector.DetectFromBytes(fileContentBytes);
                    Encoding originalEncoding = detectionResult.Detected?.Encoding;

                    string content;
                    if (originalEncoding != null)
                    {
                        // Dosyayı orijinal kodlamayla oku
                        content = originalEncoding.GetString(fileContentBytes);
                    }
                    else
                    {
                        // Varsayılan olarak UTF-8 kullan
                        content = Encoding.UTF8.GetString(fileContentBytes);
                    }

                    // İçeriği UTF-8'e dönüştür
                    byte[] utf8Bytes = Encoding.Convert(originalEncoding ?? Encoding.UTF8, Encoding.UTF8, originalEncoding?.GetBytes(content) ?? fileContentBytes);
                    string utf8Content = Encoding.UTF8.GetString(utf8Bytes);

                    rtbDisplayXML.Text = utf8Content;
                    MessageBox.Show("Dosya başarıyla UTF-8 formatına dönüştürüldü.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dönüştürme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir dosya seçin.", "Dosya Seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




    }
}

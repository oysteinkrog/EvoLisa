using System.Windows.Forms;

namespace GenArt.Core.Classes
{
    public static class FileUtil
    {
        public static string XmlExtension = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
        public static string DnaExtension = "dna files (*.dna)|*.dna|xml files (*.xml)|*.xml|All files (*.*)|*.*";
        public static string ImgExtension = "gif files (*.gif)|*.gif|bmp files (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*";

        public static string GetSaveFileName(string filter)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = filter;
                if (!dialog.ShowDialog().Equals(DialogResult.Cancel))
                    return dialog.FileName;
                return null;
            }
        }

        public static string GetOpenFileName(string filter)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = filter;
                if (!dialog.ShowDialog().Equals(DialogResult.Cancel))
                    return dialog.FileName;
                return null;
            }
        }
    }
}

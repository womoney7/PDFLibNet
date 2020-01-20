using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFLibNet;
using System.Drawing.Imaging;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ConvertPDF2Image("C:\\Users\\dengli\\Desktop\\New Microsoft Word Document.pdf", "C:\\Users\\dengli\\", "test", 1, 5);
        }

        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }

        /// <summary>
        /// ��PDF�ĵ�ת��ΪͼƬ�ķ���
        /// </summary>
        /// <param name="pdfInputPath">PDF�ļ�·��</param>
        /// <param name="imageOutputPath">ͼƬ���·��</param>
        /// <param name="imageName">����ͼƬ������</param>
        /// <param name="startPageNum">��PDF�ĵ��ĵڼ�ҳ��ʼת��</param>
        /// <param name="endPageNum">��PDF�ĵ��ĵڼ�ҳ��ʼֹͣת��</param>
        /// <param name="imageFormat">��������ͼƬ��ʽ</param>
        /// <param name="definition">����ͼƬ�������ȣ�����Խ��Խ����</param>
        public static void ConvertPDF2Image(string pdfInputPath, string imageOutputPath,
            string imageName, int startPageNum, int endPageNum, ImageFormat imageFormat = null, Definition definition = Definition.Five)
        {
            PDFWrapper pdfWrapper = new PDFWrapper();

            pdfWrapper.LoadPDF(pdfInputPath);

            if (!System.IO.Directory.Exists(imageOutputPath))
            {
                Directory.CreateDirectory(imageOutputPath);
            }

            // validate pageNum
            if (startPageNum <= 0)
            {
                startPageNum = 1;
            }

            if (endPageNum > pdfWrapper.PageCount)
            {
                endPageNum = pdfWrapper.PageCount;
            }

            if (startPageNum > endPageNum)
            {
                int tempPageNum = startPageNum;
                startPageNum = endPageNum;
                endPageNum = startPageNum;
            }

            // start to convert each page
            for (int i = startPageNum; i <= endPageNum; i++)
            {
                pdfWrapper.ExportJpg(imageOutputPath + imageName + i.ToString() + ".jpg", i, i, 150, 90);//��������������ͼƬ��ҳ������С��ͼƬ����
                if (pdfWrapper.IsJpgBusy) { System.Threading.Thread.Sleep(50); }
            }

            pdfWrapper.Dispose();
        }
    }
}

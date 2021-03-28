using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class Form1 : Form
    {
        private string filePath = @"C:\Users\Jackson\Downloads";
        private string currentlySelectedItemName = "";
        private bool isFile = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath;
            loadFilesAndDirectories();
        }

        /// <summary>
        /// Заполняет ListView файлами и директориями
        /// </summary>
        public void loadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = filePath + @"\" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileNameLabel.Text = fileDetails.Name;
                    fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);
                }
                else
                {

                    fileAttr = File.GetAttributes(filePath);

                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();           //Возвращает все файлы
                    DirectoryInfo[] dirs = fileList.GetDirectories(); //Возвращает все директории

                    string fileExtension = "";
                    listView1.Items.Clear();

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 21);
                    }

                    for (int i = 0; i < files.Length; i++)
                    {
                        listView1.Items.Add(files[i].Name, 20);

                        /*fileExtension = files[i].Extension.ToUpper();
                        switch (fileExtension)
                        {
                            case ".MP3":
                            case ".MP2":
                                listView1.Items.Add(dirs[i].Name, 9);
                                break;
                            case ".MKV":
                            case ".AVI":
                            case ".MPEG":
                            case ".MOV":
                            case ".WMA":
                                listView1.Items.Add(dirs[i].Name, 10);
                                break;
                            case ".PDF":
                                listView1.Items.Add(dirs[i].Name, 12);
                                break;
                            case ".DOC":
                            case ".DOCX":
                            case ".TXT":
                                listView1.Items.Add(dirs[i].Name, 3);
                                break;
                            case ".BMP":
                            case ".GIF":
                                listView1.Items.Add(dirs[i].Name, 6);
                                break;
                            case ".PNG":
                                listView1.Items.Add(dirs[i].Name, 13);
                                break;

                            case ".PSD":
                                listView1.Items.Add(dirs[i].Name, 14);
                                break;
                            case ".RAR":
                            case ".ZIP":
                                listView1.Items.Add(dirs[i].Name, 15);
                                break;

                            default:
                                listView1.Items.Add(dirs[i].Name, 20);
                                break;
                        }*/
                    }


                }
                else
                {
                    fileNameLabel.Text = currentlySelectedItemName;
                }


            }
            catch (Exception e)
            {

            }

        }


        public void loadButtonAction()
        {
            removeBackSlash();
            filePath = filePathTextBox.Text;
            loadFilesAndDirectories();
            isFile = false;
        }

        public void removeBackSlash()
        {
            string path = filePathTextBox.Text;
            if (path.LastIndexOf(@"\") == path.Length - 1)
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1); ;
            }
        }

        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf(@"\"));
                isFile = false;
                filePathTextBox.Text = path;
                removeBackSlash();
            }
            catch (Exception)
            {

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;

            FileAttributes fileAttr = File.GetAttributes(filePath + @"\" + currentlySelectedItemName);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                filePathTextBox.Text = filePath + @"\" + currentlySelectedItemName;
            }
            else
            {
                isFile = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }
    }
}

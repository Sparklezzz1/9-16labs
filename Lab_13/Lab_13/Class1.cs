using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace lab_13
{
    class DMVLog
    {
        public static class DMVDiskInfo
        {
            public static void ShowFreeSpace(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);
                Console.WriteLine($"свободное место на диске {drive.Name} {drive.TotalFreeSpace}");
            }
            public static void ShowFileSystem(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);
                Console.WriteLine($"Диск {drive.Name} с файловой системой: {drive.DriveFormat}");
            }
            public static void AllInfo()
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach(DriveInfo drive in drives)
                {
                    if(drive.IsReady)
                    {
                        Console.WriteLine($"Имя диска {drive.Name}");
                        Console.WriteLine($"Объем диска {drive.TotalSize}");
                        Console.WriteLine($"Свободное место на диске {drive.TotalFreeSpace}");
                        Console.WriteLine($"Метка тома {drive.VolumeLabel}");
                    }
                }
            }
        }
        public static class DMVFileInfo
        {
            public static void ShowFilePath(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Полный путь к файлу: {file.FullName}");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует");
            }
            public static void ShowFileSizeExtName(string path)
            {
                FileInfo file = new FileInfo(path);
                if(file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Размер файла: {file.Length} байт");
                    Console.WriteLine($"Расширение файла: {file.Extension}");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует");
            }
            public static void ShowCreationFile(string path)
            {
                FileInfo file = new FileInfo(path);
                if(file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Время создания: {file.CreationTime}");
                }
            }
        }
        public static class DMVDirInfo
        {
            public static void ListOfDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка {directory.Name}");
                    Console.WriteLine("Подкоталоги:");
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                }
                else
                    Console.WriteLine("Такого каталога не существует");
            }
            public static void CreationTime(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка: {directory.Name}");
                    Console.WriteLine($"Дата создания: {directory.CreationTime}");
                }
                else
                    Console.WriteLine("Такого каталога не существует ");
            }
            public static void NumerOfFiles(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if(directory.Exists)
                {
                    int number = 0;
                    Console.WriteLine($"Папка: {directory.Name}");
                    string[] files = Directory.GetFiles(path);
                    foreach(string s in files)
                    {
                        Console.WriteLine(s);
                        number++;
                    }
                    Console.WriteLine($"Общее количество файлов в папке: {number}");
                }
                else
                    Console.WriteLine("Такого каталога не существует");
            }
            public static void ParentDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    Console.WriteLine($"Папка: {directory.Name}");
                    Console.WriteLine($"Рлдительский каталок: {directory.Parent}");
                }
            }
        }
        public static class DMVFileManager
        {
            public static void Task_a()
            {
                string path1 = "d:\\";
                string path2 = "d:\\DMVInspect";
                string path3 = "d:\\DMVInspect\\DMVdirInfo.txt";
                string copyPath = "d:\\DMVInspect\\DMVdirinfoCopiedAndRenamed";

                if (Directory.Exists(path1))
                {
                    Console.WriteLine("Каталоги:");
                    string[] dirs = Directory.GetDirectories(path1);
                    foreach(string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Файлы: ");
                    string[] files = Directory.GetFiles(path1);
                    foreach(string s in files)
                    {
                        Console.WriteLine(s);
                    }
                }

                DirectoryInfo newDir = new DirectoryInfo(path2);
                if (!newDir.Exists)
                {
                    newDir.Create();
                    Console.WriteLine("Новая папка успешно создана");
                }

                try
                {
                    string[] dirs = Directory.GetDirectories(path1);
                    string[] files = Directory.GetFiles(path1);
                    using(StreamWriter sw = new StreamWriter(path3, true, Encoding.Default))
                    {
                        sw.WriteLine("Информация о диске D:");
                        sw.WriteLine("Каталоги:");
                        foreach(string s in dirs)
                        {
                            sw.WriteLine(s);
                        }
                        sw.WriteLine("Файлы:");
                        foreach(string s in files)
                        {
                            sw.WriteLine(s);
                        }
                        Console.WriteLine("Запись выполнена!");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                FileInfo file = new FileInfo(path3);
                if(file.Exists)
                {
                    file.CopyTo(copyPath);
                    file.Delete();
                    Console.WriteLine("Файл скопирован и удален");
                }
            }
            public static void Task_b(string path)
            {
                string path1 = "d:\\DMVFiles";
                string path2 = "d:\\DMVInspect\\DMVFiles";
                DirectoryInfo newDir = new DirectoryInfo(path1);
                if(!newDir.Exists)
                {
                    newDir.Create();
                    Console.WriteLine("Новая папка успешно создана!");
                }

                DirectoryInfo dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    foreach(FileInfo item in dir.GetFiles())
                    {
                        if(item.Name.Contains(".txt"))
                        {
                            item.CopyTo($"d:\\DMVFiles\\{item.Name}");
                        }
                    }
                }

                DirectoryInfo directory = new DirectoryInfo(path1);
                if (directory.Exists)
                {
                    directory.MoveTo(path2);
                    Console.WriteLine("Перемещение прошло успешно!");
                }
            }
            public static void Task_c()
            {
                string path1 = "d:\\DMVInspect\\DMVFiles";
                string path2 = "d:\\DMVInspect\\DMVFiles\\compress.gz";
                DirectoryInfo dir = new DirectoryInfo(path1);
                foreach(FileInfo file in dir.GetFiles())
                {
                    //поток для чтения исходного файла
                    using(FileStream sourceStream = new FileStream(file.FullName, FileMode.OpenOrCreate))
                    {
                        //поток для записи сжатого файла
                        using(FileStream targetStream = File.Create(path2))
                        {
                            //поток архивации 
                            using(GZipStream gz = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(gz);
                                Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1} сжатый размер: {2}.", file.FullName, sourceStream.Length.ToString(),
                                    targetStream.Length.ToString());
                            }
                        }
                    }
                }

                string path3 = "d:\\Документы";
                DirectoryInfo directory = new DirectoryInfo(path1);
                foreach(FileInfo file in directory.GetFiles())
                {
                    if (file.Name.Contains(".txt"))
                    {
                        using(FileStream sourceStream = new FileStream(path2, FileMode.OpenOrCreate))
                        {
                            //поток для записи востоновленного файла
                            using (FileStream targetStream = File.Create($"{path3}\\{file.Name}"))
                            {
                                //поток разархивации
                                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                {
                                    decompressionStream.CopyTo(targetStream);
                                    Console.WriteLine("Восстановлен файл: {0}", path2);
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}

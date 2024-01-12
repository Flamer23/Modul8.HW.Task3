namespace Modul8.HW.Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\zuevi\\Desktop\\HW";
            long sizeBefore = 0;
            long sizeAfter = 0;

            int count = 0;

            if (Directory.Exists(path))
            {
                Count(path, ref sizeBefore);
                Console.WriteLine($"Размер до чистки - {sizeBefore}");
                Clear(path, ref count);
                Count(path, ref sizeAfter);
                Console.WriteLine($"Размер после чистки - {sizeAfter}");
                Console.WriteLine($"Освобождено - {sizeBefore - sizeAfter}");
                Console.WriteLine($"Файлов удалено - {count}");
            }
            else
            {
                Console.WriteLine("Папка по указанному пути не существует");
            }
        }

        public static void Clear(string path, ref int count)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            DirectoryInfo[] subDirectories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.LastAccessTime.AddMinutes(30) < DateTime.Now) 
                {
                    try
                    {
                        file.Delete();
                        count++;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("Файл не найден. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                }
            }

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                if (subDirectory.GetDirectories() != null)
                {
                    try
                    {
                        Clear(subDirectory.FullName, ref count);
                        if(subDirectory.GetDirectories().Length == 0) 
                        { 
                            subDirectory.Delete(); 
                        }
                        
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }

                }
                else
                {
                    string subPath = subDirectory.FullName;
                    Clear(subPath, ref count);
                }
            }
        }

        public static void Count(string path, ref long sizeBefore)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            DirectoryInfo[] subDirectories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                try
                {
                    sizeBefore += file.Length;
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Файл не найден. Ошибка: " + ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }

            }

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                if (subDirectory.GetDirectories() != null)
                {
                    try
                    {
                        Count(subDirectory.FullName, ref sizeBefore);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                }

            }

        }
    }
}

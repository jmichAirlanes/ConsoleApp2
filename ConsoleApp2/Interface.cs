using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Interface
    {
        public Human CurentHuman { get; set; }
        public string Path { get; set; }
        public void Start()
        {
            Console.WriteLine("Привет!/n Введи своё имя для отображения своего плейлиста или зарегистрируйся: ");
            string l = Console.ReadLine();
            while (true)
            {
                Console.WriteLine(l+ 
                    ", вы находитесь в главном меню.\n " +
                    "выберите опцию:\n" +
                    "1.Добавить трэк в мой плейлист \n" +
                    "2.Посмотреть треклист \n " +
                    "3.Закрыть приложуху нахуй ");
                int q = Convert.ToInt32(Console.ReadLine());
                if (q == 3) break;
                if (q == 2) 
                {
                    this.ViewList(l);
                }
                if (q == 1)
                {
                    if (CurentHuman == null)
                    {
                        this.ViewList(l);
                    }
                    this.AddTrack();
                }
                Console.ReadLine();
            }
           

            

        }
        private void ViewList(string name) 
        {   
            CurentHuman = new Human();
            string folderName ="humans";
            bool isExist = Directory.Exists(folderName);
            this.Path = folderName + "/" + name + $"/{name}.json";
            if (!isExist) 
            {
                Directory.CreateDirectory(folderName);
            }
            bool isUserExist = Directory.Exists(folderName +"/"+name);
            if (!isUserExist)
            {
                Directory.CreateDirectory(folderName + "/" + name);

                CurentHuman.Name = name;
                var Result = JsonConvert.SerializeObject(CurentHuman);
               
                File.WriteAllText(Path, Result);

            }
            else 
            {
                
                using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {

                        string fileContents = reader.ReadToEnd();
                        CurentHuman = JsonConvert.DeserializeObject<Human>(fileContents);
                    }

                }
                if (CurentHuman.Musics==null ||CurentHuman.Musics.Length == 0)
                {
                    Console.WriteLine("У вас нет музыки. Добавь, мавпа.");
                    this.AddTrack();

                }
                else 
                {
                    Console.WriteLine("Выбери трэк:");
                    for (int i = 0; i < CurentHuman.Musics.Length; i++)
                    {
                        Console.WriteLine($"{i+1} {CurentHuman.Musics[i].Name }");
                    }
                    int numOfSound = Convert.ToInt32(Console.ReadLine());
                    Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome",CurentHuman.Musics[numOfSound-1].Link);                    
                }

            }
        }
        private void AddTrack()
        {
            Console.WriteLine("введи название группы, трэка и ссылку на них через ентер");
            Music newMusic=new Music();
            newMusic.Group = Console.ReadLine();
            newMusic.Name = Console.ReadLine();
            newMusic.Link = Console.ReadLine();
            if (CurentHuman.Musics==null)
            {
                CurentHuman.Musics = new Music[1];
            }
            CurentHuman.Musics[CurentHuman.Musics.Length-1] = newMusic;

            Console.WriteLine("Композиция добавлена");
            
              
            var Result = JsonConvert.SerializeObject(CurentHuman);
            File.WriteAllText(Path, Result);

        }

    }
}

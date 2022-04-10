using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class PlayList
    {
        public PlayList(string name, Music[] musics)
        {
            this.Name = name;
            this.Musics = musics;

            
          
        }
        public string Name { get; set; }
        public Music[] Musics { get; set; }
    }
}

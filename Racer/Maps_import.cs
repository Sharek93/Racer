using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racer
{
    class Maps_import
    {
        List<Image> start = new List<Image>();
        Random rand = new Random();
        RacerEntities db = new RacerEntities();

        //## POBIERANIE LISTY ID MAP ZPEŁNIAJĄCYCH WARUNEK

        public List<int> Maps_IDs(string start)
        {
            List<int> IDs = (from b in db.Map where b.start == start select b.ID).ToList();
            return IDs;
        }

        //## POBIERANIE OBAZU MAPY O KONKRETNYM ID

        public Image Map(int ID)
        {
            var temp = (from b in db.Map where b.ID == ID select b.mapa).First();
            var stream = new MemoryStream(temp);
            Image map = Image.FromStream(stream);
            return map;
        }

        //## POBIERANIE OBRAZU MINIMAPY

        public Image Minimap(int ID)
        {
            var temp = (from b in db.Map where b.ID == ID select b.mini_mapa).First();
            var stream = new MemoryStream(temp);
            Image minimap = Image.FromStream(stream);
            return minimap;
        }

        //## POBIERANIE ILOŚCI PUNKÓW KONTROLNYCH

        public int Checkpoint_count(int ID)
        {
            int chechpoint_count = (from b in db.Map where b.ID == ID select b.punkty_kontrolne).First();
            return chechpoint_count;
        }

        //## POBIERANIE KIERUNKU KONCA MAPY

        public string Map_end(int ID)
        {
            string map_end = (from b in db.Map where b.ID == ID select b.koniec).First();
            return map_end;
        }
    }
}

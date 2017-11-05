using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racer
{
    class Map_constructor
    {
        Maps_import map_import = new Maps_import();
        Random rand = new Random();
        List<int> maps_IDs = new List<int>();
        Bitmap default_map = new Bitmap(1, 1);
        Bitmap minimap1 = new Bitmap(450, 450);
        Bitmap minimap2 = new Bitmap(450, 450);
        Bitmap main_map1 = new Bitmap(9000, 9000);
        Bitmap main_map2 = new Bitmap(9000, 9000);

        //## ZMIENNE

        int[] current_main_map_segments_ids = new int[9];
        int[] current_minimap_segments_ids = new int[9];

        int checkpoints_count = 0;

        byte current_main_map = 1;      

        string last_map_end;
        int last_map_location;
        bool load;

        //## WŁAŚCIWOŚCI

        public string Last_map_end
        {
            get
            {
                return last_map_end;
            }
        }

        public int Last_map_location
        {
            get
            {
                return last_map_location;
            }
        }

        public bool Load
        {
            get
            {
                return load;
            }
        }

        public Bitmap Get_main_map()
        {
            switch (current_main_map)
            {
                case 1:
                    {
                        return main_map1;
                    }
                case 2:
                    {
                        return main_map2;
                    }
                default:
                    {
                        return default_map;
                    }
            }
        }

        public Bitmap Get_minimap()
        {
            switch (Current_main_map)
            {
                case 1:
                    {
                        return minimap1;
                    }
                case 2:
                    {
                        return minimap2;
                    }
                default:
                    {
                        return default_map;
                    }
            }
        }

        public byte Current_main_map
        {
            get
            {
                return current_main_map;
            }

            set
            {
                current_main_map = value;
            }
        }

        public int Checkpoints_count
        {
            get
            {
                return checkpoints_count;
            }
        }

        //## PROJEKTOWANIE STARTOWEJ MAPY

        public void Start_map()
        {
            int[] map_segments_id = new int[9];
            int[] minimap_segments_id = new int[9];

            maps_IDs = map_import.Maps_IDs("start");
            int id = rand.Next(0, maps_IDs.Count - 1);


            map_segments_id[4] = maps_IDs[id];
            minimap_segments_id[4] = maps_IDs[id];

            string start_end = map_import.Map_end(maps_IDs[id]);

            maps_IDs.Clear();

            switch (start_end)
            {
                case "up":
                    {
                        maps_IDs = map_import.Maps_IDs("down");
                        id = rand.Next(0, maps_IDs.Count - 1);

                        map_segments_id[1] = maps_IDs[id];
                        minimap_segments_id[1] = maps_IDs[id];

                        string map_end = map_import.Map_end(maps_IDs[id]);
                        maps_IDs.Clear();

                        switch (map_end)
                        {
                            case "up":
                                {
                                    last_map_location = 1;
                                    last_map_end = map_end;
                                    break;
                                }
                            case "left":
                                {
                                    maps_IDs = map_import.Maps_IDs("right");
                                    id = rand.Next(0, maps_IDs.Count - 1);

                                    map_segments_id[0] = maps_IDs[id];
                                    minimap_segments_id[0] = maps_IDs[id];

                                    map_end = map_import.Map_end(maps_IDs[id]);
                                    last_map_location = 0;
                                    last_map_end = map_end;
                                    maps_IDs.Clear();
                                    break;
                                }
                            case "right":
                                {
                                    maps_IDs = map_import.Maps_IDs("left");
                                    id = rand.Next(0, maps_IDs.Count - 1);

                                    map_segments_id[2] = maps_IDs[id];
                                    minimap_segments_id[2] = maps_IDs[id];

                                    map_end = map_import.Map_end(maps_IDs[id]);
                                    last_map_location = 2;
                                    last_map_end = map_end;
                                    maps_IDs.Clear();
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "left":
                    {
                        break;
                    }
                case "right":
                    {
                        break;
                    }
            }

            maps_IDs =  map_import.Maps_IDs("fill");
            id = rand.Next(0, maps_IDs.Count - 1);

            for (int i = 0; i < map_segments_id.Count(); i++)
            {
                if (map_segments_id[i] == 0)
                {
                    map_segments_id[i] = maps_IDs[id];
                }
                if (minimap_segments_id[i] == 0)
                {
                    minimap_segments_id[i] = maps_IDs[id];
                }
                checkpoints_count += map_import.Checkpoint_count(map_segments_id[i]);
            }

            maps_IDs.Clear();

            create_main_map(map_segments_id,0);
            create_minimap(minimap_segments_id,0);

            current_main_map_segments_ids = map_segments_id;
            current_minimap_segments_ids = minimap_segments_id;
        }

        //## TWORZENIE MAPY Z PROJEKTU

        private void create_main_map(int[] map_segments_id, byte map_number)
        {
            switch (map_number)
            {
                case 0:
                    {
                        using (Graphics main_map = Graphics.FromImage(main_map1))
                        {
                            main_map.DrawImage(map_import.Map(map_segments_id[0]), 0, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[1]), 3000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[2]), 6000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[3]), 0, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[4]), 3000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[5]), 6000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[6]), 0, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[7]), 3000, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[8]), 6000, 6000);
                        }
                        using (Graphics main_map = Graphics.FromImage(main_map2))
                        {
                            main_map.DrawImage(map_import.Map(map_segments_id[0]), 0, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[1]), 3000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[2]), 6000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[3]), 0, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[4]), 3000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[5]), 6000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[6]), 0, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[7]), 3000, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[8]), 6000, 6000);                            
                        }
                        break;
                    }
                case 1:
                    {
                        using (Graphics main_map = Graphics.FromImage(main_map1))
                        {
                            main_map.DrawImage(map_import.Map(map_segments_id[0]), 0, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[1]), 3000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[2]), 6000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[3]), 0, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[4]), 3000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[5]), 6000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[6]), 0, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[7]), 3000, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[8]), 6000, 6000);
                        }
                        break;
                    }
                case 2:
                    {
                        using (Graphics main_map = Graphics.FromImage(main_map2))
                        {
                            main_map.DrawImage(map_import.Map(map_segments_id[0]), 0, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[1]), 3000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[2]), 6000, 0);
                            main_map.DrawImage(map_import.Map(map_segments_id[3]), 0, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[4]), 3000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[5]), 6000, 3000);
                            main_map.DrawImage(map_import.Map(map_segments_id[6]), 0, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[7]), 3000, 6000);
                            main_map.DrawImage(map_import.Map(map_segments_id[8]), 6000, 6000);
                        }
                        break;
                    }
                default:
                    break;
            } 
        }

        //## TWORZENIE MINIMAPY Z PROJEKTU

        private void create_minimap(int[] minimap_segments_id, byte map_number)
        {
            switch (map_number)
            {
                case 0:
                    {
                        using (Graphics minimap = Graphics.FromImage(minimap1))
                        {
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[0]), 0, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[1]), 150, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[2]), 300, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[3]), 0, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[4]), 150, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[5]), 300, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[6]), 0, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[7]), 150, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[8]), 300, 300);
                        }
                        minimap1.MakeTransparent(Color.White);
                        using (Graphics minimap = Graphics.FromImage(minimap2))
                        {
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[0]), 0, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[1]), 150, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[2]), 300, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[3]), 0, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[4]), 150, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[5]), 300, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[6]), 0, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[7]), 150, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[8]), 300, 300);
                        }
                        minimap2.MakeTransparent(Color.White);
                        break;
                    }
                case 1:
                    {
                        using (Graphics minimap = Graphics.FromImage(minimap1))
                        {
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[0]), 0, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[1]), 150, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[2]), 300, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[3]), 0, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[4]), 150, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[5]), 300, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[6]), 0, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[7]), 150, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[8]), 300, 300);
                        }
                        minimap1.MakeTransparent(Color.White);
                        break;
                    }
                case 2:
                    {
                        using (Graphics minimap = Graphics.FromImage(minimap2))
                        {
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[0]), 0, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[1]), 150, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[2]), 300, 0);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[3]), 0, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[4]), 150, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[5]), 300, 150);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[6]), 0, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[7]), 150, 300);
                            minimap.DrawImage(map_import.Minimap(minimap_segments_id[8]), 300, 300);
                        }
                        minimap2.MakeTransparent(Color.White);
                        break;
                    }
                default:
                    break;
            }
        }

        //## PROEKTOWAINE MAPY

        public void Map_Genetator(string direction, string previous_map_end, int previous_map_location, bool end)
        {
            int[] map_segments_id = new int[3];
            int[] minimap_segments_id = new int[3];
            List<int> maps_IDs = new List<int>();
            int id;
            string map_end;
            load = true;

            switch (direction)
            {
                case "up":
                    {
                        switch (previous_map_location)
                        {
                            case 0:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "left":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "up":
                                            {
                                                try
                                                {
                                                    string map_start;                                       
                                                    if (end)
                                                    {
                                                        map_start = "down-end";
                                                    }
                                                    else
                                                    {
                                                        map_start = "down";
                                                    }
                                                    int i = 0;
                                                    do
                                                    {                                                       
                                                        maps_IDs = map_import.Maps_IDs(map_start);
                                                        id = rand.Next(0, maps_IDs.Count - 1);

                                                        map_segments_id[i] = maps_IDs[id];
                                                        minimap_segments_id[i] = maps_IDs[id];
                                                        checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                        map_end = map_import.Map_end(maps_IDs[id]);
                                                        maps_IDs.Clear();
                                                        id = 0;

                                                        if (map_end == "right" && i <= 2)
                                                        {
                                                            i++;
                                                            map_start = "left";

                                                            maps_IDs = map_import.Maps_IDs(map_start);
                                                            id = rand.Next(0, maps_IDs.Count - 1);

                                                            map_segments_id[i] = maps_IDs[id];
                                                            minimap_segments_id[i] = maps_IDs[id];
                                                            checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                            map_end = map_import.Map_end(maps_IDs[id]);
                                                            last_map_end = map_end;
                                                            last_map_location = i;
                                                            maps_IDs.Clear();
                                                        }
                                                        else
                                                        {
                                                            last_map_end = map_end;
                                                            last_map_location = i;
                                                            maps_IDs.Clear();
                                                            break;
                                                        }
                                                    } while (i<=2);
                                                }
                                                catch (Exception) { }

                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (end)
                                    {
                                        maps_IDs = map_import.Maps_IDs("down-end");
                                    }
                                    else
                                    {
                                        maps_IDs = map_import.Maps_IDs("down");
                                    }
                                    id = rand.Next(0, maps_IDs.Count - 1);

                                    map_segments_id[1] = maps_IDs[id];
                                    minimap_segments_id[1] = maps_IDs[id];
                                    checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                    map_end = map_import.Map_end(maps_IDs[id]);
                                    maps_IDs.Clear();

                                    switch (map_end)
                                    {
                                        case "up":
                                            {
                                                last_map_location = 1;
                                                last_map_end = map_end;
                                                break;
                                            }
                                        case "left":
                                            {
                                                maps_IDs = map_import.Maps_IDs("right");
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[0] = maps_IDs[id];
                                                minimap_segments_id[0] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_location = 0;
                                                last_map_end = map_end;
                                                maps_IDs.Clear();
                                                break;
                                            }
                                        case "right":
                                            {
                                                maps_IDs = map_import.Maps_IDs("left");
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[2] = maps_IDs[id];
                                                minimap_segments_id[2] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_location = 2;
                                                last_map_end = map_end;
                                                maps_IDs.Clear();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "right":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "up":
                                            {
                                                string map_start;
                                                if (end)
                                                {
                                                    map_start = "down-end";
                                                }
                                                else
                                                {
                                                    map_start = "down";
                                                }
                                                int i = 2;
                                                try
                                                {
                                                    do
                                                    {
                                                        maps_IDs = map_import.Maps_IDs(map_start);
                                                        id = rand.Next(0, maps_IDs.Count - 1);

                                                        map_segments_id[i] = maps_IDs[id];
                                                        minimap_segments_id[i] = maps_IDs[id];
                                                        checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                        map_end = map_import.Map_end(maps_IDs[id]);
                                                        maps_IDs.Clear();
                                                        id = 0;

                                                        if (map_end == "left" && i >= 0)
                                                        {
                                                            i--;
                                                            map_start = "right";

                                                            maps_IDs = map_import.Maps_IDs(map_start);
                                                            id = rand.Next(0, maps_IDs.Count - 1);

                                                            map_segments_id[i] = maps_IDs[id];
                                                            minimap_segments_id[i] = maps_IDs[id];
                                                            checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                            map_end = map_import.Map_end(maps_IDs[id]);
                                                            last_map_end = map_end;
                                                            last_map_location = i;
                                                            maps_IDs.Clear();
                                                        }
                                                        else
                                                        {
                                                            last_map_end = map_end;
                                                            last_map_location = i;
                                                            maps_IDs.Clear();
                                                            break;
                                                        }
                                                    } while (i>=0);
                                                }
                                                catch (Exception) { }

                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            default:
                                break;
                        }

                        if (load)
                        {
                            maps_IDs = map_import.Maps_IDs("fill");
                            id = rand.Next(0, maps_IDs.Count - 1);

                            for (int i = 0; i < map_segments_id.Count(); i++)
                            {
                                if (map_segments_id[i] == 0)
                                {
                                    map_segments_id[i] = maps_IDs[id];
                                }
                                if (minimap_segments_id[i] == 0)
                                {
                                    minimap_segments_id[i] = maps_IDs[id];
                                }
                            }

                            current_main_map_segments_ids[6] = current_main_map_segments_ids[3];
                            current_main_map_segments_ids[7] = current_main_map_segments_ids[4];
                            current_main_map_segments_ids[8] = current_main_map_segments_ids[5];
                            current_main_map_segments_ids[3] = current_main_map_segments_ids[0];
                            current_main_map_segments_ids[4] = current_main_map_segments_ids[1];
                            current_main_map_segments_ids[5] = current_main_map_segments_ids[2];

                            current_main_map_segments_ids[0] = map_segments_id[0];
                            current_main_map_segments_ids[1] = map_segments_id[1];
                            current_main_map_segments_ids[2] = map_segments_id[2];

                            current_minimap_segments_ids[6] = current_minimap_segments_ids[3];
                            current_minimap_segments_ids[7] = current_minimap_segments_ids[4];
                            current_minimap_segments_ids[8] = current_minimap_segments_ids[5];
                            current_minimap_segments_ids[3] = current_minimap_segments_ids[0];
                            current_minimap_segments_ids[4] = current_minimap_segments_ids[1];
                            current_minimap_segments_ids[5] = current_minimap_segments_ids[2];

                            current_minimap_segments_ids[0] = minimap_segments_id[0];
                            current_minimap_segments_ids[1] = minimap_segments_id[1];
                            current_minimap_segments_ids[2] = minimap_segments_id[2];

                            if (current_main_map == 2)
                            {
                                create_main_map(current_main_map_segments_ids, 1);
                                create_minimap(current_minimap_segments_ids, 1);
                            }
                            else
                            {
                                create_main_map(current_main_map_segments_ids, 2);
                                create_minimap(current_minimap_segments_ids, 2);
                            }                            
                        }

                        //## END OF CASE "UP" IN DIRECTION SWITCH ##
                        break;
                    }
                case "left":
                    {
                        switch (previous_map_location)
                        {
                            case 0:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "up":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "left":
                                            {
                                                if (end)
                                                {
                                                    maps_IDs = map_import.Maps_IDs("right-end");
                                                }
                                                else
                                                {
                                                    maps_IDs = map_import.Maps_IDs("right");
                                                }
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[0] = maps_IDs[id];
                                                minimap_segments_id[0] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_end = map_end;
                                                last_map_location = 0;
                                                maps_IDs.Clear();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "up":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "left":
                                            {
                                                if (end)
                                                {
                                                    maps_IDs = map_import.Maps_IDs("right-end");
                                                }
                                                else
                                                {
                                                    maps_IDs = map_import.Maps_IDs("right");
                                                }
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[1] = maps_IDs[id];
                                                minimap_segments_id[1] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_end = map_end;
                                                last_map_location = 3;
                                                maps_IDs.Clear();

                                                if (map_end == "up")
                                                {
                                                    maps_IDs = map_import.Maps_IDs("down");
                                                    id = rand.Next(0, maps_IDs.Count - 1);

                                                    map_segments_id[0] = maps_IDs[id];
                                                    minimap_segments_id[0] = maps_IDs[id];
                                                    checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                    map_end = map_import.Map_end(maps_IDs[id]);
                                                    last_map_location = 0;
                                                    maps_IDs.Clear();
                                                }
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            default:
                                break;
                        }

                        if (last_map_location >= 1)
                        {
                            load = false;
                        }

                        if (load)
                        {
                            maps_IDs = map_import.Maps_IDs("fill");
                            id = rand.Next(0, maps_IDs.Count - 1);

                            for (int i = 0; i < map_segments_id.Count(); i++)
                            {
                                if (map_segments_id[i] == 0)
                                {
                                    map_segments_id[i] = maps_IDs[id];
                                }
                                if (minimap_segments_id[i] == 0)
                                {
                                    minimap_segments_id[i] = maps_IDs[id];
                                }                                
                            }

                            current_main_map_segments_ids[2] = current_main_map_segments_ids[1];
                            current_main_map_segments_ids[5] = current_main_map_segments_ids[4];
                            current_main_map_segments_ids[8] = current_main_map_segments_ids[7];
                            current_main_map_segments_ids[1] = current_main_map_segments_ids[0];
                            current_main_map_segments_ids[4] = current_main_map_segments_ids[3];
                            current_main_map_segments_ids[7] = current_main_map_segments_ids[6];

                            current_main_map_segments_ids[0] = map_segments_id[0];
                            current_main_map_segments_ids[3] = map_segments_id[1];
                            current_main_map_segments_ids[6] = map_segments_id[2];

                            current_minimap_segments_ids[2] = current_minimap_segments_ids[1];
                            current_minimap_segments_ids[5] = current_minimap_segments_ids[4];
                            current_minimap_segments_ids[8] = current_minimap_segments_ids[7];
                            current_minimap_segments_ids[1] = current_minimap_segments_ids[0];
                            current_minimap_segments_ids[4] = current_minimap_segments_ids[3];
                            current_minimap_segments_ids[7] = current_minimap_segments_ids[6];

                            current_minimap_segments_ids[0] = minimap_segments_id[0];
                            current_minimap_segments_ids[3] = minimap_segments_id[1];
                            current_minimap_segments_ids[6] = minimap_segments_id[2];

                            if (current_main_map == 2)
                            {
                                create_main_map(current_main_map_segments_ids, 1);
                                create_minimap(current_minimap_segments_ids, 1);
                            }
                            else
                            {
                                create_main_map(current_main_map_segments_ids, 2);
                                create_minimap(current_minimap_segments_ids, 2);
                            }
                        }

                        //## END OF "LEFT" CASE IN DIRECTION SWITCH ##
                        break;
                    }
                case "right":
                    {
                        switch (previous_map_location)
                        {
                            case 2:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "up":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "right":
                                            {
                                                if (end)
                                                {
                                                    maps_IDs = map_import.Maps_IDs("left-end");
                                                }
                                                else
                                                {
                                                    maps_IDs = map_import.Maps_IDs("left");
                                                }
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[0] = maps_IDs[id];
                                                minimap_segments_id[0] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_end = map_end;
                                                last_map_location = 2;
                                                maps_IDs.Clear();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    switch (previous_map_end)
                                    {
                                        case "up":
                                            {
                                                load = false;
                                                break;
                                            }
                                        case "right":
                                            {
                                                if (end)
                                                {
                                                    maps_IDs = map_import.Maps_IDs("left-end");
                                                }
                                                else
                                                {
                                                    maps_IDs = map_import.Maps_IDs("left");
                                                }
                                                id = rand.Next(0, maps_IDs.Count - 1);

                                                map_segments_id[1] = maps_IDs[id];
                                                minimap_segments_id[1] = maps_IDs[id];
                                                checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                map_end = map_import.Map_end(maps_IDs[id]);
                                                last_map_end = map_end;
                                                last_map_location = 5;
                                                maps_IDs.Clear();

                                                if (map_end == "up")
                                                {
                                                    maps_IDs = map_import.Maps_IDs("down");
                                                    id = rand.Next(0, maps_IDs.Count - 1);

                                                    map_segments_id[0] = maps_IDs[id];
                                                    minimap_segments_id[0] = maps_IDs[id];
                                                    checkpoints_count += map_import.Checkpoint_count(maps_IDs[id]);

                                                    map_end = map_import.Map_end(maps_IDs[id]);
                                                    last_map_location = 2;
                                                    maps_IDs.Clear();
                                                }
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    break;
                                }
                            default:
                                break;
                        }

                        if (last_map_location <= 1)
                        {
                            load = false;
                        }

                        if (load)
                        {
                            maps_IDs = map_import.Maps_IDs("fill");
                            id = rand.Next(0, maps_IDs.Count - 1);

                            for (int i = 0; i < map_segments_id.Count(); i++)
                            {
                                if (map_segments_id[i] == 0)
                                {
                                    map_segments_id[i] = maps_IDs[id];
                                }
                                if (minimap_segments_id[i] == 0)
                                {
                                    minimap_segments_id[i] = maps_IDs[id];
                                }
                            }

                            current_main_map_segments_ids[0] = current_main_map_segments_ids[1];
                            current_main_map_segments_ids[3] = current_main_map_segments_ids[4];
                            current_main_map_segments_ids[6] = current_main_map_segments_ids[7];
                            current_main_map_segments_ids[1] = current_main_map_segments_ids[2];
                            current_main_map_segments_ids[4] = current_main_map_segments_ids[5];
                            current_main_map_segments_ids[7] = current_main_map_segments_ids[8];

                            current_main_map_segments_ids[2] = map_segments_id[0];
                            current_main_map_segments_ids[5] = map_segments_id[1];
                            current_main_map_segments_ids[8] = map_segments_id[2];

                            current_minimap_segments_ids[0] = current_minimap_segments_ids[1];
                            current_minimap_segments_ids[3] = current_minimap_segments_ids[4];
                            current_minimap_segments_ids[6] = current_minimap_segments_ids[7];
                            current_minimap_segments_ids[1] = current_minimap_segments_ids[2];
                            current_minimap_segments_ids[4] = current_minimap_segments_ids[5];
                            current_minimap_segments_ids[7] = current_minimap_segments_ids[8];

                            current_minimap_segments_ids[2] = minimap_segments_id[0];
                            current_minimap_segments_ids[5] = minimap_segments_id[1];
                            current_minimap_segments_ids[8] = minimap_segments_id[2];

                            if (current_main_map == 2)
                            {
                                create_main_map(current_main_map_segments_ids, 1);
                                create_minimap(current_minimap_segments_ids, 1);
                            }
                            else
                            {
                                create_main_map(current_main_map_segments_ids, 2);
                                create_minimap(current_minimap_segments_ids, 2);
                            }
                        }

                        //## END OF "RIGHT" CASE IN DIRECTION SWITCH ##
                        break;
                    }
                default:
                    break;
            }
        }
    }
}

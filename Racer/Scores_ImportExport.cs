using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Racer
{
    class ImportNicknamesEventArgs : EventArgs
    {
        public List<string> Nicknames { get; set; }
    }

    class ImportScoresEventArgs : EventArgs
    {
        public Highscores[] Highscores { get; set; }
    }

    class Scores_ImportExport
    {
        //## DEFINIOWANIE DELEGATÓW

        public delegate void NicknameImportHandler(object source, ImportNicknamesEventArgs Args);
        public delegate void ScoreImportHandler(object source, ImportScoresEventArgs Args);
        public delegate void ScoreSaverHandler(object source, EventArgs Args);

        public event NicknameImportHandler NicknamesImported;
        public event ScoreImportHandler ScoresImported;
        public event ScoreSaverHandler ScoreSaved;

        RacerEntities db = new RacerEntities();
        Highscores[] scores;

        //## POBIERANIE LISTY UNIKATOWYCH NICKÓW

        public async Task Import_nicknames()
        {
            List<string> nicknames = await (from b in db.Highscores select b.Nick).ToListAsync();

            List<string> nicknames_final = new List<string>();

            foreach (var nick in nicknames)
            {
                if (!nicknames_final.Contains(nick))
                {
                    nicknames_final.Add(nick);
                }
            }
            OnNicknamesImported(nicknames_final);
        }

        //## POBIERANIE NAZWY KOLUMN

        public List<string> Get_column_names()
        {
            var column_names = (from t in typeof(Highscores).GetProperties()select t.Name).ToList();

            return column_names;
        }

        //POBIERANIE DANYCH 

        public async Task Import_score(string nickname, string orderby)
        {
            switch (orderby)
            {
                case "Brak":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname select b).ToArrayAsync();
                        break;
                    }
                case "Nick":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname orderby b.Nick select b).ToArrayAsync();
                        break;
                    }
                case "Wynik":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname orderby b.Wynik select b).ToArrayAsync();
                        break;
                    }
                case "Czas":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname orderby b.Czas select b).ToArrayAsync();
                        break;
                    }
                case "Punkty_kontr":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname orderby b.Punkty_kontr select b).ToArrayAsync();
                        break;
                    }
                case "Data":
                    {
                        scores = await (from b in db.Highscores where b.Nick == nickname orderby b.Data select b).ToArrayAsync();
                        break;
                    }
                default:
                    break;
            }
            OnScoresImported(scores);
        }

        public async Task Import_score(string orderby)
        {
            switch (orderby)
            {
                case "Brak":
                    {
                        scores = await (from b in db.Highscores select b).ToArrayAsync();
                        break;
                    }
                case "Nick":
                    {
                        scores = await (from b in db.Highscores orderby b.Nick select b).ToArrayAsync();
                        break;
                    }
                case "Wynik":
                    {
                        scores = await (from b in db.Highscores orderby b.Wynik select b).ToArrayAsync();
                        break;
                    }
                case "Czas":
                    {
                        scores = await (from b in db.Highscores orderby b.Czas select b).ToArrayAsync();
                        break;
                    }
                case "Punkty_kontr":
                    {
                        scores = await (from b in db.Highscores orderby b.Punkty_kontr select b).ToArrayAsync();
                        break;
                    }
                case "Data":
                    {
                        scores = await (from b in db.Highscores orderby b.Data select b).ToArrayAsync();
                        break;
                    }
                default:
                    break;
            }
            OnScoresImported(scores);
        }

        //## DODAWANIE NOWEGO REKORU

        public async Task New_score(string[] data)
        {
            int id = (from b in db.Highscores orderby b.ID descending select b.ID).First();

            int wynik;
            int punkty_kontr;
            int.TryParse(data[2], out wynik);
            int.TryParse(data[3], out punkty_kontr);
            db.Highscores.Add(new Highscores
            {
                ID = id + 1,
                Nick = data[0],
                Czas = data[1],
                Wynik = wynik,
                Punkty_kontr = punkty_kontr,
                Data = data[4],               
            });

            await db.SaveChangesAsync();

            Thread.Sleep(1000);

            OnScoreSaved();
        }

        //DELEGATY

        protected virtual void OnNicknamesImported(List<string> nicknames)
        {
            if (NicknamesImported != null)
            {
                NicknamesImported(this, new ImportNicknamesEventArgs() { Nicknames = nicknames });
            }
        }

        protected virtual void OnScoresImported(Highscores[] highscores)
        {
            if (ScoresImported != null)
            {
                ScoresImported(this, new ImportScoresEventArgs() { Highscores = highscores});
            }
        }

        protected virtual void OnScoreSaved()
        {
            if (ScoreSaved != null)
            {
                ScoreSaved(this, EventArgs.Empty);
            }
        }
    }
}

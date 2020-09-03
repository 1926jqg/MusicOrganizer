using System;
using System.Linq;

namespace MusicOrganizer
{
    public class Mp3File
    {
        public const string UNKNOWN_ALBUM = "Unkown Album";
        public const string UNKNOWN_ARTIST = "Unkown Artist";

        public string Title { get; private set; }
        public string Album { get; private set; }
        public string Artist { get; private set; }
        public string Path { get; }

        public Mp3File(string path)
        {
            var file = TagLib.File.Create(path);
            Path = path;
            Title = file.Tag.Title;            
            Artist = file.Tag.AlbumArtists.FirstOrDefault() ?? file.Tag.FirstAlbumArtist ?? file.Tag.Artists.FirstOrDefault() ?? UNKNOWN_ARTIST;
            Album = file.Tag.Album ?? $"{Artist} {UNKNOWN_ALBUM}";

            CleanFields();
        }

        private void CleanFields()
        {
            var invalidFileChars = System.IO.Path.GetInvalidFileNameChars();
            var invalidPathChars = System.IO.Path.GetInvalidPathChars();

            Title = new string(Title.Where(t => !invalidFileChars.Contains(t)).ToArray()).Trim();
            Artist = new string(Artist.Where(t => !invalidPathChars.Contains(t) && !invalidFileChars.Contains(t) && t != '.').ToArray()).Trim();
            Album = new string(Album.Where(t => !invalidPathChars.Contains(t) && !invalidFileChars.Contains(t) && t != '.').ToArray()).Trim();
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(object obj)
        {
            return obj is Mp3File file &&
                   Title == file.Title &&
                   Album == file.Album &&
                   Artist == file.Artist;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Album, Artist);
        }
    }
}

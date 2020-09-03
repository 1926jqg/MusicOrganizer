using System;
using System.Collections;
using System.Collections.Generic;

namespace MusicOrganizer
{
    public class Artist : IEnumerable<Album>
    {
        private readonly Dictionary<string, Album> _albums = new Dictionary<string, Album>();

        public string Name { get; }

        public Artist(string name)
        {
            Name = name;
        }

        public void Add(Mp3File file)
        {
            if (file.Artist != Name)
                throw new ArgumentException($"{file} cannot be added to artist {this}. It is by {file.Artist}.");

            if(!_albums.TryGetValue(file.Album, out Album album))
            {
                album = new Album(file.Album);
                _albums.Add(file.Album, album);
            }
            album.Add(file);
        }

        public IEnumerator<Album> GetEnumerator()
        {
            return _albums.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }

        public string GetPath(string baseDirectory)
        {
            return $@"{baseDirectory}\Artists\{Name}";
        }
    }
}

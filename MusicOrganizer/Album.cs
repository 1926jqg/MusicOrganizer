using System;
using System.Collections;
using System.Collections.Generic;

namespace MusicOrganizer
{
    public class Album : IEnumerable<Mp3File>
    {
        private readonly HashSet<Mp3File> _files = new HashSet<Mp3File>();

        public string Title { get; }
        
        public Album(string title)
        {
            Title = title;
        }

        public void Add(Mp3File file)
        {
            if (file.Album != Title)
                throw new ArgumentException($"{file} cannot be added to album {this}. It is on {file.Album}.");
            _files.Add(file);
        }

        public IEnumerator<Mp3File> GetEnumerator()
        {
            return _files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Title;
        }

        public string GetPath(string baseDirectory)
        {
            return $@"{baseDirectory}\Albums\{Title}";
        }
    }
}

using System.Collections.Generic;

namespace MusicOrganizer
{
    public class MusicLibrary
    {

        private readonly Dictionary<string, Artist> _artists = new Dictionary<string, Artist>();
        private readonly Dictionary<string, Album> _albums = new Dictionary<string, Album>();

        public IEnumerable<Artist> Artists
        {
            get
            {
                return _artists.Values;
            }
        }

        public IEnumerable<Album> Albums
        {
            get
            {
                return _albums.Values;
            }
        }

        public void Add(Mp3File file)
        {
            if(!_albums.TryGetValue(file.Album, out Album album))
            {
                album = new Album(file.Album);
                _albums.Add(file.Album, album);
            }
            if (!_artists.TryGetValue(file.Artist, out Artist artist))
            {
                artist = new Artist(file.Artist);
                _artists.Add(file.Artist, artist);
            }

            album.Add(file);
            artist.Add(file);
        }
    }
}

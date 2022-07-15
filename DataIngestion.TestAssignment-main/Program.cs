using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Nest;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static async Task Main(string[] args)
		{

            var settings = new ConnectionSettings(new Uri("http://example.com:9200"))
                                        .DefaultIndex("result");

            var client = new ElasticClient(settings);

            var allArtistLines = File.ReadAllLines("/Users/skulshrestha/Documents/personal projects/input_files/artist.csv");
            var listOfArtists = new System.Collections.Generic.List<Artist>();
            foreach (var line in allArtistLines)
            {
                var splittedLines = line.Split( "," );
                 if (splittedLines != null && splittedLines.Any())
                {
                    listOfArtists.Add(new Artist
                    {
                        export_date =  int.Parse(splittedLines[0]),
                        artist_id = splittedLines.Length > 1 ? int.Parse(splittedLines[1]) : 0,
                        name = splittedLines.Length > 1 ? splittedLines[2] : null,
                        is_actual_artist = splittedLines.Length > 1 ? bool.Parse(splittedLines[3]) : false,
                        view_url = splittedLines.Length > 1 ? splittedLines[4] : null,
                        artist_type_id = splittedLines.Length > 1 ? int.Parse(splittedLines[5]) : 0,
                    });
                }

            }

            var allArtistCollectionLines = File.ReadAllLines("/Users/skulshrestha/Documents/personal projects/input_files/artist_collection.csv");
            var listOfArtistCollections = new System.Collections.Generic.List<ArtistCollection>();
            foreach (var line in allArtistCollectionLines)
            {
                var splittedLines = line.Split(",");
                if (splittedLines != null && splittedLines.Any())
                {
                    listOfArtistCollections.Add(new ArtistCollection
                    {
                        export_date = int.Parse(splittedLines[0]),
                        artist_id = splittedLines.Length > 1 ? int.Parse(splittedLines[1]) : 0,
                        collection_id = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        is_primary_artist = splittedLines.Length > 1 ? bool.Parse(splittedLines[3]) : false,
                        role_id = splittedLines.Length > 1 ? int.Parse(splittedLines[4]) : 0,
                    });
                }

            }

            var allCollectionMatchLines = File.ReadAllLines("/Users/skulshrestha/Documents/personal projects/input_files/collection_match.csv");
            var listOfCollectionMatch = new System.Collections.Generic.List<CollectionMatch>();
            foreach (var line in allCollectionMatchLines)
            {
                var splittedLines = line.Split(",");
                if (splittedLines != null && splittedLines.Any())
                {
                    listOfCollectionMatch.Add(new CollectionMatch
                    {
                        export_date = int.Parse(splittedLines[0]),
                        collection_id = splittedLines.Length > 1 ? int.Parse(splittedLines[1]) : 0,
                        upc = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        grid = splittedLines.Length > 1 ? int.Parse(splittedLines[3]) : 0,
                        amg_album_id = splittedLines.Length > 1 ? int.Parse(splittedLines[4]) : 0,
                    });
                }

            }

            var allCollectionLines = File.ReadAllLines("/Users/skulshrestha/Documents/personal projects/input_files/collection.csv");
            var listOfCollections = new System.Collections.Generic.List<Collection>();
            foreach (var line in allCollectionLines)
            {
                var splittedLines = line.Split(",");
                if (splittedLines != null && splittedLines.Any())
                {
                    listOfCollections.Add(new Collection
                    {
                        export_date = int.Parse(splittedLines[0]),
                        collection_id = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        name = splittedLines.Length > 1 ? splittedLines[2] : null,
                        title_version = splittedLines.Length > 1 ? splittedLines[2] : null,
                        search_terms = splittedLines.Length > 1 ? splittedLines[2] : null,
                        parental_advisory_id = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        artist_display_name = splittedLines.Length > 1 ? splittedLines[2] : null,
                        view_url = splittedLines.Length > 1 ? splittedLines[2] : null,
                        artwork_url = splittedLines.Length > 1 ? splittedLines[2] : null,
                        original_release_date = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        itunes_release_date = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        label_studio = splittedLines.Length > 1 ? splittedLines[2] : null,
                        content_provider_name = splittedLines.Length > 1 ? splittedLines[2] : null,
                        copyright = splittedLines.Length > 1 ? splittedLines[2] : null,
                        p_line = splittedLines.Length > 1 ? splittedLines[2] : null,
                        media_type_id = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                        is_compilation = splittedLines.Length > 1 ? bool.Parse(splittedLines[3]) : false,
                        collection_type_id = splittedLines.Length > 1 ? int.Parse(splittedLines[2]) : 0,
                    });
                }

            }

            foreach (Collection collection in listOfCollections)
            {
                resultObject result = new()
                {
                    Id = collection.collection_id,
                    ImageUrl = collection.artwork_url,
                    Name = collection.name,
                    Url = collection.view_url,
                    IsCompilation = collection.is_compilation,
                    Label = collection.label_studio
                };

                System.Collections.Generic.List<CollectionMatch> val = (System.Collections.Generic.List<CollectionMatch>)listOfCollectionMatch.Where(x => listOfCollectionMatch.Any(y => y.collection_id == collection.collection_id));             
                result.Upc = val.AsQueryable().FirstOrDefault().upc;
                result.ReleaseDate = new DateTime(collection.original_release_date);
                var artists = listOfArtistCollections.Where(x => listOfArtistCollections.Any(y => y.collection_id == collection.collection_id));
                result.Artists = (System.Collections.Generic.List<Artist>)artists;

                var indexResponse = client.IndexDocument(result);

                var asyncIndexResponse = await client.IndexDocumentAsync(result);

            }

        }
	}
}

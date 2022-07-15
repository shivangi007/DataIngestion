using System;
namespace Application
{
	public class ArtistCollection
	{

		public int export_date { get; set; }
		public int artist_id { get; set; }
		public int collection_id { get; set; }
		public Boolean is_primary_artist { get; set; } 
		public int role_id { get; set; }

		public ArtistCollection()
		{
		}
	}
}


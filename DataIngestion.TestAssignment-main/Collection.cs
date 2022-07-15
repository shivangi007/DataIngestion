using System;
namespace Application
{
	public class Collection
	{
        public int export_date { get; set; }
        public int collection_id { get; set; }
        public String name { get; set; }
        public String title_version { get; set; }
        public String search_terms { get; set; }
        public int parental_advisory_id { get; set; }
        public String artist_display_name { get; set; }
        public String view_url { get; set; }
        public String artwork_url { get; set; }
        public int original_release_date { get; set; }
        public int itunes_release_date { get; set; }
        public String label_studio { get; set; }
        public String content_provider_name { get; set; }
        public String copyright { get; set; }
        public String p_line { get; set; }
        public int media_type_id { get; set; }
        public Boolean is_compilation { get; set; }
        public int collection_type_id { get; set; }

        public Collection()
		{
		}
	}
}


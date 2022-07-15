using System;

namespace Application
{
	public class resultObject
    {
		
	  public int Id { get; set; }
	  public String Name { get; set; }
	  public String Url { get; set; }
	  public int Upc { get; set; }
	  public DateTime ReleaseDate { get; set; }
	  public Boolean IsCompilation { get; set; }
	  public String Label { get; set; }
	  public String ImageUrl { get; set; }
	  public System.Collections.Generic.List<Artist> Artists { get; set; }
  
public resultObject()
		{
		}
	}
}


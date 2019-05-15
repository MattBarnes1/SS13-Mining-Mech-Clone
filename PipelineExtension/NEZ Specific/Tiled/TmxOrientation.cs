
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;


namespace Nez.TiledMaps
{
	public enum TmxOrientation
	{
		[XmlEnum(Name = "orthogonal")]
		Orthogonal = 1,
		[XmlEnum( Name = "isometric" )] 
		Isometric = 2,
		[XmlEnum( Name = "staggered" )] 
		Staggered = 3
	}
}
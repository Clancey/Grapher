using System;

namespace Grapher
{
	public class Telemetry
	{
		public Telemetry ()
		{
		}
		public int SourceId {get;set;}
		public string Name{get;set;}
		public float Value{get;set;}
		public DateTime Timestamp {get;set;}
	}
}


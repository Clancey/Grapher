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

		public Type GetType() {
			switch (this.Name) {
			case "CO2":
				return typeof(CO2);
			case "O2":
				return typeof(Oxygen);
			default:
				return typeof(Telemetry);
			}
		}
	}
}


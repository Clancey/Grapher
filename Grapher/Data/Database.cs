using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grapher
{
	public class Database : SQLiteConnection
	{
		public Database () : base(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"database.db"),true)
		{
			this.CreateTable<CO2>();
			this.CreateTable<Oxygen>();
		}
		public static Database Main{
			get{
				if(main == null)
					main = new Database();
				return main;
			}
		}
		static Database main;

		public List<Oxygen> GetOxygen(int sourceId)
		{
			return this.Table<Oxygen>().Where(x=> x.SourceId == sourceId).ToList();
		}
		public Task<List<Oxygen>> GetOxygenAsync(int sourceId)
		{
			return Task.Factory.StartNew(delegate{
				return GetOxygen(sourceId);
			});
		}

		public bool InsertTelemetry(List<Telemetry> telemetry)
		{
			var oxygen = telemetry.Where(x=> x.Name == "O2").Cast<Oxygen>().ToList();
			var co2 = telemetry.Where(x=> x.Name == "CO2").Cast<CO2>().ToList();
			if(this.InsertAll(oxygen,true) < oxygen.Count)
				return false;
			return this.InsertAll(co2,true) == co2.Count;

		}

	}
}


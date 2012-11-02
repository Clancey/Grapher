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

		public List<Oxygen> GetOxygen(int sourceId, DateTime since)
		{
			return this.Table<Oxygen>().Where(x=> x.SourceId == sourceId && x.Timestamp >= since).OrderBy(x=> x.Timestamp).ToList();
		}
		public Task<List<Oxygen>> GetOxygenAsync(int sourceId, DateTime since)
		{
			return Task.Factory.StartNew(delegate{
				return GetOxygen(sourceId,since);
			});
		}

		public List<CO2> GetCo2(int sourceId, DateTime since)
		{
			return this.Table<CO2>().Where(x=> x.SourceId == sourceId).Take(100).OrderBy(x=> x.Timestamp).ToList();
		}
		public Task<List<CO2>> GetCo2Async(int sourceId, DateTime since)
		{
			return Task.Factory.StartNew(delegate{
				return GetCo2(sourceId,since);
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

		public bool InsertTelemetry(Telemetry tele) {
			return this.Insert(tele, tele.GetType()) == 1;
		}

		public Task<bool> InsertTelemetryAsync(Telemetry tele) {
			return Task.Factory.StartNew<bool>(() => {
				return this.InsertTelemetry(tele);
			});
		}

	}
}


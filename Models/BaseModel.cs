using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace Translucent.Models
{
	public abstract class BaseModel<dbContext> where dbContext : DbContext
	{
		/// <summary>A lsit of errors that accumulated throughout the process of running the model
		/// </summary>
		[ScriptIgnore]
		protected List<Exception> Exceptions { get; private set; }

		/// <summary>The list of error messages caught by the model
		/// </summary>
		public string[] Errors { get { return (Exceptions ?? new List<Exception>()).Select(e => e.Message).ToArray(); } }

		/// <summary>Was the model successful (i.e. no errors)?
		/// </summary>
		public bool Success { get { return Exceptions == null ? false : Exceptions.Count == 0; } }

		/// <summary>A persistent database context that will exist during the life of the http request
		/// </summary>
		protected abstract dbContext Database { get; }

		/// <summary>Initialize the model. This prepares and executes database queries
		/// </summary>
		public void Init()
		{
			Exceptions = new List<Exception>();
			try
			{
				// prepare the database queries
				BeforeQuery();

				using (var db = Database)
				{
					// execute the database queries
					Query(db);
				}
			}
			catch(Exception error)
			{
				Exceptions = Exceptions ?? new List<Exception>();
				Exceptions.Add(error);
			}
		}

		/// <summary>Load the model. This realizes any data and executes a majority of the business logic
		/// </summary>
		public void Load()
		{

			try
			{
				// execute post query logic
				AfterQuery();
			}
			catch(Exception error)
			{
				Exceptions = Exceptions ?? new List<Exception>();
				Exceptions.Add(error);
			}
		}

		/// <summary>Any logic that needs to be implemented prior to making database queries
		/// </summary>
		protected abstract void BeforeQuery();
		/// <summary>Make database requests
		/// </summary>
		/// <param name="db">the database context</param>
		protected abstract void Query(dbContext db);
		/// <summary>Realize the data retrieved from the database and execute post query logic
		/// </summary>
		protected abstract void AfterQuery();
	}
}

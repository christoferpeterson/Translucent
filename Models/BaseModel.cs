using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Script.Serialization;
using Translucent.Web;

namespace Translucent.Models
{
	public abstract class BaseModel<dbContext> where dbContext : DbContext
	{
		/// <summary>A lsit of errors that accumulated throughout the process of running the model
		/// </summary>
		[ScriptIgnore]
		public List<Exception> Exceptions { get; private set; }

		/// <summary>The list of error messages caught by the model
		/// </summary>
		public List<string> Errors { get { return (Exceptions ?? new List<Exception>()).Select(e => e.Message).ToList(); } }

		/// <summary>Was the model successful (i.e. no errors)?
		/// </summary>
		public bool Success { get { return Exceptions == null ? false : Exceptions.Count == 0; } }

		/// <summary>A persistent database context that will exist during the life of the http request
		/// </summary>
		protected abstract dbContext Database { get; }

		private bool _prepared = false;
		private bool _initialized = false;

		public void Prepare()
		{
			Exceptions = new List<Exception>();

			try
			{
				// prepare the database queries
				BeforeQuery();
			}
			catch(Exception error)
			{
				if (Params.Debug)
				{
					throw;
				}

				Exceptions = Exceptions ?? new List<Exception>();
				Exceptions.Add(error);
			}
			finally
			{
				_prepared = true;
			}
		}

		/// <summary>Initialize the model. This prepares and executes database queries
		/// </summary>
		public void Init()
		{
			using (var db = Database)
			{
				// execute the database queries
				Init(db);
			}
		}

		public void Init(dbContext db)
		{
			// make sure the prepare method has been called before initializing
			if (!_prepared)
			{
				throw new Exception(this.GetType().Name + " was not prepared before attempting to initialize. The model must be prepared before initialization can take place.");
			}

			try
			{
				Query(db);
			}
			catch (Exception error)
			{
				if (Params.Debug)
				{
					throw;
				}

				Exceptions = Exceptions ?? new List<Exception>();
				Exceptions.Add(error);
			}
			finally
			{
				_initialized = true;
			}
		}

		/// <summary>Load the model. This realizes any data and executes a majority of the business logic
		/// </summary>
		public void Load()
		{
			// make sure the init method has been called before initializing
			if (!_initialized)
			{
				throw new Exception(this.GetType().Name + " was not initialized before attempting to load. The model must be initialized before loading can take place.");
			}

			try
			{
				// execute post query logic
				AfterQuery();
			}
			catch(Exception error)
			{
				if (Params.Debug)
				{
					throw;
				}

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
